using System.Net.Http;
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
                .UseHttpClient(new HttpClient())
                .Build();
            return apiInstance;
        }
    }
}