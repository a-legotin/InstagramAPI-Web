using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using InstagramApi.Logger;

namespace InstagramApi.API
{
    public class InstaApiBuilder
    {
        ILogger _logger;
        string _username;
        HttpClient _httpClient;

        public IInstaApi Build()
        {
            var instaApi = new InstaApi(_username, _logger, _httpClient);
            return instaApi;
        }

        public InstaApiBuilder UseLogger(ILogger logger)
        {
            _logger = logger;
            return this;
        }

        public InstaApiBuilder UseHttpClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
            return this;
        }

        public InstaApiBuilder SetUserName(string username)
        {
            _username = username;
            return this;
        }
    }
}
