using System;
using InstagramApi.Helpers;
using Newtonsoft.Json;

namespace InstagramApi.ResponseWrappers
{
    public class InstaResponseItem
    {
        public string Code { get; set; }
        public InstagramLocation Location { get; set; }

        public string Link { get; set; }

        public InstagramPostType Type { get; set; }

        [JsonProperty("created_time")]
        public string CreatedTime { get; set; }

        [JsonProperty("can_view_comments")]
        public bool CanViewComment { get; set; }

        public string Id { get; set; }

        public DateTime CreatedTimeConverted => DateTimeHelper.UnixTimestampToDateTime(double.Parse(CreatedTime));
    }
}