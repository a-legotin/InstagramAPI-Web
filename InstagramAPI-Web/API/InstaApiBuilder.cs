using System;
using System.Net.Http;
using InstagramApi.Classes;
using InstagramApi.Logger;

namespace InstagramApi.API
{
    public class InstaApiBuilder
    {
        private HttpClient _httpClient;
        private HttpClientHandler _httpHandler = new HttpClientHandler();
        private ILogger _logger;
        private UserCredentials _user;

        public IInstaApi Build()
        {
            if (_httpClient == null)
            {
                _httpClient = new HttpClient(_httpHandler);
                _httpClient.BaseAddress = new Uri(InstaApiConstants.INSTAGRAM_URL);
            }
            var instaApi = new InstaApi(_user, _logger, _httpClient, _httpHandler);
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

        public InstaApiBuilder UseHttpClientHandler(HttpClientHandler handler)
        {
            _httpHandler = handler;
            return this;
        }

        public InstaApiBuilder SetUserName(string username)
        {
            _user = new UserCredentials {UserName = username};
            return this;
        }

        public InstaApiBuilder SetUser(UserCredentials user)
        {
            _user = user;
            return this;
        }
    }
}