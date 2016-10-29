using InstagramApi.API;
using InstagramApi.Logger;
using Xunit;

namespace InstagramApi.Tests
{
    public class ApiInstanceBuilderTest
    {
        [Fact]
        public void CreateApiInstanceWithBuilder()
        {
            var result = new InstaApiBuilder()
                .UseLogger(new TestLogger())
                .Build();
            Assert.NotNull(result);
        }
    }

    internal class TestLogger : ILogger
    {
    }
}