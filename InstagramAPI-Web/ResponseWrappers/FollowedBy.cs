using Newtonsoft.Json;

namespace InstagramApi.ResponseWrappers
{
    public class FollowedBy
    {
        [JsonProperty("count")]
        public int Count { get; set; }
    }
}