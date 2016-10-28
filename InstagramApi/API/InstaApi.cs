using System.IO;
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
        private readonly string _username;
        private ILogger _logger;


        public InstaApi()
        {
        }

        public InstaApi(string _username, ILogger _logger, HttpClient _httpClient)
        {
            this._username = _username;
            this._logger = _logger;
            this._httpClient = _httpClient;
        }

        public async Task<InstaUser> GetUserAsync()
        {
            string userUrl = $"{InstaApiConstants.INSTAGRAM_URL}{_username}{InstaApiConstants.GET_ALL_POSTFIX}";
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
            string mediaUrl = $"{InstaApiConstants.INSTAGRAM_URL}{_username}{InstaApiConstants.MEDIA}";
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

        private InstaResponse _getUserPostsResponseWithMaxId(string Id)
        {
            string mediaUrl = $"{InstaApiConstants.INSTAGRAM_URL}{_username}{InstaApiConstants.MAX_MEDIA_ID_POSTFIX}{Id}";
            string json;
            var task = _httpClient.GetStreamAsync(mediaUrl);
            using (var reader = new StreamReader(task.Result))
            {
                json = reader.ReadToEnd();
            }
            return JsonConvert.DeserializeObject<InstaResponse>(json);
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

        #endregion
    }
}