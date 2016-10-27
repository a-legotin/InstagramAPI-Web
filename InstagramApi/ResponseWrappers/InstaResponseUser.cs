using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InstagramApi.Helpers;
using Newtonsoft.Json;

namespace InstagramApi.ResponseWrappers
{
    public class InstaResponseUser
    {
        [JsonProperty("username")]
        public string UserName { get; set; }
        [JsonProperty("profile_pic_url")]

        public string ProfilePicture { get; set; }
        [JsonProperty("id")]

        public int Id { get; set; }
        [JsonProperty("full_name")]

        public string FullName { get; set; }
    }

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

    public class InstagramLocation
    {
        public string Name { get; set; }
    }

    public enum InstagramPostType

    {
        Image = 0,
        Video = 1
    }

    public class InstaResponse
    {
        public bool IsFirstResponse { get; set; }
        public string Status { get; set; }
        [JsonProperty("more_available")]
        public bool MoreAvailable { get; set; }

        public List<InstaResponseItem> Items { get; set; }

        public string GetLastId()
        {
            var id = Items.OrderByDescending(post => post.CreatedTimeConverted).LastOrDefault()?.Id;
            if (string.IsNullOrEmpty(id)) throw new ArgumentNullException("id");
            return id;
        }
    }

}
