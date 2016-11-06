using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace InstagramApi.ResponseWrappers
{
    public class InstaFeedResponseItem
    {
        [JsonProperty("media")]

        public InstaFeedResponseMediaItem Media { get; set; }
    }
}
