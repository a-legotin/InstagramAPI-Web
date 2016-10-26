using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InstagramApi.Logger;

namespace InstagramApi
{
    public class InstaApi : IInstaApi
    {
        private ILogger _logger;
        private string _username;

        public InstaApi()
        {
        }

        public InstaApi(string _username, ILogger _logger)
        {
            this._username = _username;
            this._logger = _logger;
        }
    }
}
