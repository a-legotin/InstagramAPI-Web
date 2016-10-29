using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using InstagramApi.Classes;
using InstagramApi.Converters;
using InstagramApi.Logger;
using InstagramApi.ResponseWrappers;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace InstagramApi.API
{
    public class InstaApi : IInstaApi
    {
        private readonly HttpClient _httpClient;
        private readonly HttpClientHandler _httpHandler;
        private readonly UserCredentials _user;
        private ILogger _logger;


        public InstaApi(UserCredentials _user, ILogger _logger, HttpClient _httpClient, HttpClientHandler _httpHandler)
        {
            this._user = _user;
            this._logger = _logger;
            this._httpClient = _httpClient;
            this._httpHandler = _httpHandler;
        }

        public bool IsUserAuthenticated { get; private set; }


        public async Task<InstaUser> GetUserAsync()
        {
            string userUrl = $"{InstaApiConstants.INSTAGRAM_URL}{_user.UserName}{InstaApiConstants.GET_ALL_POSTFIX}";
            IObjectConverter<InstaUser, InstaResponseUser> converter = null;
            var stream = await _httpClient.GetStreamAsync(userUrl);
            using (var reader = new StreamReader(stream))
            {
                var json = reader.ReadToEnd();
                var root = JObject.Parse(json);
                var userObject = root["user"];
                var instaresponse = JsonConvert.DeserializeObject<InstaResponseUser>(userObject.ToString());
                converter = ConvertersFabric.GetUserConverter(instaresponse);
            }
            return converter.Convert();
        }

        public async Task<InstaPostList> GetUserPostsAsync()
        {
            var posts = new InstaPostList();
            string mediaUrl = $"{InstaApiConstants.INSTAGRAM_URL}{_user.UserName}{InstaApiConstants.MEDIA}";
            string json;

            var stream = await _httpClient.GetStreamAsync(mediaUrl);
            using (var reader = new StreamReader(stream))
            {
                json = reader.ReadToEnd();
            }

            var instaresponse = JsonConvert.DeserializeObject<InstaResponse>(json);
            var converter = ConvertersFabric.GetPostsConverter(instaresponse);
            posts.AddRange(converter.Convert());
            while (instaresponse.MoreAvailable)
            {
                instaresponse = _getUserPostsResponseWithMaxId(instaresponse.GetLastId());
                converter = ConvertersFabric.GetPostsConverter(instaresponse);
                posts.AddRange(converter.Convert());
            }
            return posts;
        }


        public async Task<InstaMedia> GetMediaByCodeAsync(string postCode)
        {
            string mediaUrl = $"{InstaApiConstants.INSTAGRAM_URL}{InstaApiConstants.P_SUFFIX}{postCode}{InstaApiConstants.GET_ALL_POSTFIX}";
            var stream = await _httpClient.GetStreamAsync(mediaUrl);
            InstaResponseMedia mediaResponse;
            using (var reader = new StreamReader(stream))
            {
                var json = reader.ReadToEnd();
                var root = JObject.Parse(json);
                var mediaObject = root["media"];
                mediaResponse = JsonConvert.DeserializeObject<InstaResponseMedia>(mediaObject.ToString());
            }
            var converter = ConvertersFabric.GetSingleMediaConverter(mediaResponse);
            return converter.Convert();
        }

        public async Task<bool> LoginAsync()
        {
            if (string.IsNullOrEmpty(_user.UserName) || string.IsNullOrEmpty(_user.Password))
                throw new ArgumentException("user name and password must be specified");
            var firstResponse = await _httpClient.GetAsync(_httpClient.BaseAddress);
            var csrftoken = string.Empty;
            var cookies = _httpHandler.CookieContainer.GetCookies(_httpClient.BaseAddress);
            foreach (Cookie cookie in cookies)
            {
                if (cookie.Name == InstaApiConstants.CSRFTOKEN)
                    csrftoken = cookie.Value;
            }
            var fields = new Dictionary<string, string>
            {
                {"username", _user.UserName},
                {"password", _user.Password}
            };
            var request = new HttpRequestMessage(HttpMethod.Post, InstaApiConstants.ACCOUNTS_LOGIN_AJAX);
            request.Content = new FormUrlEncodedContent(fields);

            request.Headers.Referrer = new Uri(_httpClient.BaseAddress, InstaApiConstants.ACCOUNTS_LOGIN);
            request.Headers.Add(InstaApiConstants.HEADER_XCSRFToken, csrftoken);
            request.Headers.Add(InstaApiConstants.HEADER_XInstagramAJAX, "1");
            request.Headers.Add(InstaApiConstants.HEADER_XRequestedWith, InstaApiConstants.HEADER_XMLHttpRequest);

            var response = await _httpClient.SendAsync(request);
            var loginInfo = JsonConvert.DeserializeObject<LoginInfo>(await response.Content.ReadAsStringAsync());
            IsUserAuthenticated = loginInfo.authenticated;
            return loginInfo.authenticated;
        }

        #region sync methods

        public InstaUser GetUser()
        {
            var user = GetUserAsync().Result;
            return user;
        }

        public InstaPostList GetUserPosts()
        {
            var posts = GetUserPostsAsync().Result;
            return posts;
        }

        public InstaMedia GetMediaByCode(string postCode)
        {
            var media = GetMediaByCodeAsync(postCode).Result;
            return media;
        }
        public bool Login()
        {
            return LoginAsync().Result;
        }
        #endregion
        private InstaResponse _getUserPostsResponseWithMaxId(string Id)
        {
            string mediaUrl = $"{InstaApiConstants.INSTAGRAM_URL}{_user.UserName}{InstaApiConstants.MAX_MEDIA_ID_POSTFIX}{Id}";
            string json;
            var task = _httpClient.GetStreamAsync(mediaUrl);
            using (var reader = new StreamReader(task.Result))
            {
                json = reader.ReadToEnd();
            }
            return JsonConvert.DeserializeObject<InstaResponse>(json);
        }
    }
}