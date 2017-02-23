using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using InstagramApi.Classes;
using InstagramApi.Converters;
using InstagramApi.Helpers;
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


        public InstaApi(UserCredentials user, ILogger logger, HttpClient httpClient, HttpClientHandler httpHandler)
        {
            this._user = user;
            this._logger = logger;
            this._httpClient = httpClient;
            this._httpHandler = httpHandler;
        }

        public bool IsUserAuthenticated { get; private set; }

        #region Private methods

        private InstaResponse _getUserPostsResponseWithMaxId(string Id)
        {
            string mediaUrl =
                $"{InstaApiConstants.INSTAGRAM_URL}{_user.UserName}{InstaApiConstants.MAX_MEDIA_ID_POSTFIX}{Id}";
            string json;
            var task = _httpClient.GetStreamAsync(mediaUrl);
            using (var reader = new StreamReader(task.Result))
            {
                json = reader.ReadToEnd();
            }
            return JsonConvert.DeserializeObject<InstaResponse>(json);
        }

        #endregion

        #region sync methods

        public InstaUser GetUser(string username)
        {
            var user = GetUserAsync(username).Result;
            return user;
        }

        public InstaPostList GetUserPosts(int pageCount)
        {
            var posts = GetUserPostsAsync(pageCount).Result;
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

        public InstaUserFeed GetUserFeed(int pageCount)
        {
            return GetUserFeedAsync(pageCount).Result;
        }

        public InstaUser GetCurrentUser()
        {
            return GetCurrentUserAsync().Result;
        }

        #endregion

        #region async methods

        public async Task<InstaUser> GetUserAsync(string username)
        {
            string userUrl = $"{InstaApiConstants.INSTAGRAM_URL}{username}{InstaApiConstants.GET_ALL_POSTFIX}";
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

        public InstaPostList GetUserPostsByUsername(string username, int pageCount = 0)
        {
            return GetUserPostsByUsernameAsync(username, pageCount).Result;
        }

        public async Task<InstaPostList> GetUserPostsAsync(int pageCount = 0)
        {
            return await GetUserPostsByUsernameAsync(_user.UserName, pageCount);
        }

        public async Task<InstaPostList> GetUserPostsByUsernameAsync(string username, int pageCount = 0)
        {
            var posts = new InstaPostList();
            string mediaUrl = $"{InstaApiConstants.INSTAGRAM_URL}{username}{InstaApiConstants.MEDIA}";
            string json;

            var stream = await _httpClient.GetStreamAsync(mediaUrl);
            using (var reader = new StreamReader(stream))
            {
                json = reader.ReadToEnd();
            }

            var instaresponse = JsonConvert.DeserializeObject<InstaResponse>(json);
            var converter = ConvertersFabric.GetPostsConverter(instaresponse);
            posts.AddRange(converter.Convert());
            var pages = 1;
            while (instaresponse.MoreAvailable && pages <= pageCount)
            {
                pages++;
                instaresponse = _getUserPostsResponseWithMaxId(instaresponse.GetLastId());
                converter = ConvertersFabric.GetPostsConverter(instaresponse);
                posts.AddRange(converter.Convert());
            }
            return posts;
        }

        public Task<InstaUser> GetCurrentUserAsync()
        {
            return GetUserAsync(_user.UserName);
        }

        public async Task<InstaMedia> GetMediaByCodeAsync(string postCode)
        {
            string mediaUrl =
                $"{InstaApiConstants.INSTAGRAM_URL}{InstaApiConstants.P_SUFFIX}{postCode}{InstaApiConstants.GET_ALL_POSTFIX}";
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
            var cookies = _httpHandler.CookieContainer.GetCookies(_httpClient.BaseAddress);
            foreach (Cookie cookie in cookies)
                if (cookie.Name == InstaApiConstants.CSRFTOKEN) _user.Token = cookie.Value;
            var fields = new Dictionary<string, string>
            {
                {"username", _user.UserName},
                {"password", _user.Password}
            };
            var request = new HttpRequestMessage(HttpMethod.Post, InstaApiConstants.ACCOUNTS_LOGIN_AJAX)
            {
                Content = new FormUrlEncodedContent(fields)
            };

            request.Headers.Referrer = new Uri(_httpClient.BaseAddress, InstaApiConstants.ACCOUNTS_LOGIN);
            request.Headers.Add(InstaApiConstants.HEADER_XCSRFToken, _user.Token);
            request.Headers.Add(InstaApiConstants.HEADER_XInstagramAJAX, "1");
            request.Headers.Add(InstaApiConstants.HEADER_XRequestedWith, InstaApiConstants.HEADER_XMLHttpRequest);

            var response = await _httpClient.SendAsync(request);
            var loginInfo = JsonConvert.DeserializeObject<LoginInfo>(await response.Content.ReadAsStringAsync());
            IsUserAuthenticated = loginInfo.authenticated;
            return loginInfo.authenticated;
        }

        public async Task<InstaUserFeed> GetUserFeedAsync(int pageCount)
        {
            if (!IsUserAuthenticated) throw new Exception("user must be authenticated");
            var feedUrl = $"{InstaApiConstants.INSTAGRAM_URL.TrimEnd('/')}{InstaApiConstants.GET_ALL_POSTFIX}";
            var request = HttpHelper.GetDefaultRequest(HttpMethod.Get, new Uri(feedUrl));
            request.Headers.Add(InstaApiConstants.HEADER_XCSRFToken, _user.Token);
            request.Headers.Add(InstaApiConstants.HEADER_XInstagramAJAX, "1");
            request.Headers.Add(InstaApiConstants.HEADER_XRequestedWith, InstaApiConstants.HEADER_XMLHttpRequest);
            var response = await _httpClient.SendAsync(request);
            var json = await response.Content.ReadAsStringAsync();
            var feedResponse = JsonConvert.DeserializeObject<InstaFeedResponse>(json);
            var converter = ConvertersFabric.GetFeedConverter(feedResponse);
            return converter.Convert();
        }

        #endregion
    }
}