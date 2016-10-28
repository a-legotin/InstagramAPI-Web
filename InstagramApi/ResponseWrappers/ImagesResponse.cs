using Newtonsoft.Json;

namespace InstagramApi.ResponseWrappers
{
    public class ImagesResponse
    {
        [JsonProperty("low_resolution")]
        public Image LowResolution { get; set; }

        [JsonProperty("thumbnail")]
        public Image Thumbnail { get; set; }

        [JsonProperty("standard_resolution")]
        public Image StandartResolution { get; set; }
    }
}