using Xunit;


namespace InstagramApi.Tests
{
    public class ApiInstanceBuilderTest
    {

        [Fact]
        public void CreateApiInstance()
        {
            var result = new InstaApi();
            Assert.NotNull(result);
        }
    }
}
