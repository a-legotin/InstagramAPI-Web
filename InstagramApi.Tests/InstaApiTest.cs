using System;
using System.Linq;
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
            if (posts.Count > 0)
                Assert.Equal(posts.FirstOrDefault().UserId, user.InstaIdentifier);
        }

        [Theory]
        [InlineData("BGBgSw0tpHQ")]
        [InlineData("BL0fnggBAsU")]
        [InlineData("BMDFkBND-6k")]
        [InlineData("BMEiO2kj8Je")]

        public void GetMediaTest(string mediaCode)
        {
            //arrange
            var apiInstance = TestHelpers.GetDefaultInstaApiInstance("just some random string");
            //act
            var media = apiInstance.GetMediaByCode(mediaCode);
            //assert
            Assert.NotNull(media);
            Assert.Equal(media.Code, mediaCode);
        }

        [Fact]
        public void UserLoginSuccessTest()
        {
            //arrange
            var username = "alex_codegarage";
            var password = Environment.GetEnvironmentVariable("instaapiuserpassword");
            var apiInstance =
                TestHelpers.GetDefaultInstaApiInstance(new UserCredentials { UserName = username, Password = password });
            //act
            bool success = apiInstance.Login();

            //assert
            Assert.True(success);
            Assert.True(apiInstance.IsUserAuthenticated);
        }

        [Fact]
        public void UserLoginEmptyPasswordTest()
        {
            //arrange
            var username = "alex_codegarage";
            var apiInstance =
                TestHelpers.GetDefaultInstaApiInstance(new UserCredentials { UserName = username });
            //act
            Action loginAction = () => apiInstance.Login();
            //assert
            Assert.Throws<AggregateException>(loginAction);
        }

        [Fact]
        public void UserLoginFailTest()
        {
            //arrange
            var username = "alex_codegarage";
            var password = "sometestpassword";
            var apiInstance =
                TestHelpers.GetDefaultInstaApiInstance(new UserCredentials { UserName = username, Password = password });

            //act
            bool success = apiInstance.Login();
            //assert
            Assert.False(success);
            Assert.False(apiInstance.IsUserAuthenticated);
        }
    }
}