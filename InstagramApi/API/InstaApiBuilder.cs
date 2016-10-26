using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InstagramApi.Logger;

namespace InstagramApi.API
{
    public class InstaApiBuilder
    {
        ILogger _logger;
        string _username;

        public IInstaApi Build()
        {
            var instaApi = new InstaApi(_username, _logger);
            return instaApi;
        }

        public InstaApiBuilder UseLogger(ILogger logger)
        {
            _logger = logger;
            return this;
        }
    }
}
