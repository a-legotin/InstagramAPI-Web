using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InstagramApi.API;

namespace InstagramApi.Tests
{
    public class TestHelpers
    {
        public static IInstaApi GetDefaultInstaApiInstance(string username)
        {
            var apiInstance = new InstaApiBuilder()
              .SetUserName(username)
              .UseLogger(new TestLogger())
              .UseHttpClient(new System.Net.Http.HttpClient())
              .Build();
            return apiInstance;
        }
    }
}
