using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InstagramApi.API;
using Xunit;

namespace InstagramApi.Tests
{
    public class InstaApiTest
    {
        [Fact]
        public void CreateApiInstanceWithBuilder()
        {
            //arrange
            string username = "alex_codegarage";
            var apiInstance = new InstaApiBuilder()
                .SetUserName(username)
                .UseLogger(new TestLogger())
                .UseHttpClient(new System.Net.Http.HttpClient())
                .Build();
            //act
            var user = apiInstance.GetUser();
            //assert
            Assert.NotNull(user);
            Assert.Equal(user.UserName, username);
        }
    }
}
