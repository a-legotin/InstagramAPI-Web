using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace InstagramApi.ResponseWrappers
{
    public class InstaResponseSuggestedUsers
    {
        [JsonProperty("nodes")]
        public IList<InstaResponseUser> Users { get; set; }
    }
}
