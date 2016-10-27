using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using InstagramApi.API;
using InstagramApi.Classes;
using InstagramApi.Converters;
using InstagramApi.Logger;
using InstagramApi.ResponseWrappers;
using Newtonsoft.Json;

namespace InstagramApi
{
    public class InstaApi : IInstaApi
    {
        private ILogger _logger;
        private string _username;
        private HttpClient _httpClient;


        public InstaApi()
        {
        }

        public InstaApi(string _username, ILogger _logger, HttpClient _httpClient)
        {
            this._username = _username;
            this._logger = _logger;
        }

        public InstaUser GetUser()
        {
            var userTask = GetUserAsync();
            return userTask.Result;

        }

        public async Task<InstaUser> GetUserAsync()
        {
            var user = new InstaUser();
            string userUrl = $"{InstaApiConstants.INSTAGRAM_URL}{_username}{InstaApiConstants.USER_PROFILE_POSTFIX}";
            var stream = await _httpClient.GetStreamAsync(userUrl);
            using (var reader = new StreamReader(stream))
            {
                var json = await reader.ReadToEndAsync();
                var root = Newtonsoft.Json.Linq.JObject.Parse(json);
                var userObject = root["user"];
                var instaresponse = JsonConvert.DeserializeObject<InstaResponseUser>(userObject.ToString());
                var converter = ConvertersFabric.GetUserConverter(instaresponse);
                return converter.Convert();
            }
        }

    }
}
