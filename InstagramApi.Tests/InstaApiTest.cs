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
        [Theory]
        [InlineData("alex_codegarage")]
        [InlineData("instagram")]
        public void GetUserTest(string username)
        {
            //arrange
            var apiInstance = TestHelpers.GetDefaultInstaApiInstance(username);
            //act
            var user = apiInstance.GetUser();
            //assert
            Assert.NotNull(user);
            Assert.Equal(user.UserName, username);
        }

        [Theory]
        [InlineData("alex_codegarage")]
        [InlineData("instagram")]
        public void GetUserPostsTest(string username)
        {
            //arrange
            var apiInstance = TestHelpers.GetDefaultInstaApiInstance(username);
            //act
            var user = apiInstance.GetUser();
            var posts = apiInstance.GetUserPosts();
            //assert
            Assert.NotNull(posts);
            if(posts.Count > 0)
                Assert.Equal(posts.FirstOrDefault().UserId, user.InstaIdentifier);
        }
    }
}
