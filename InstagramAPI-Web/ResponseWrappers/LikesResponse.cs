using Newtonsoft.Json;

namespace InstagramApi.ResponseWrappers
{
    public class LikesResponse
    {
        [JsonProperty("count")]
        public int Count { get; set; }

        [JsonProperty("data")]
        public InstaResponseUser[] Users { get; set; }
    }
}