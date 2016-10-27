using System.Net.Http;
using InstagramApi.Logger;

namespace InstagramApi.API
{
    public class InstaApiBuilder
    {
        private HttpClient _httpClient = new HttpClient();
        private ILogger _logger;
        private string _username;

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