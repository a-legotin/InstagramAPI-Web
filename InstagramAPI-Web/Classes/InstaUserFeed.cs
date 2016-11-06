using System.Collections.Generic;

namespace InstagramApi.Classes
{
    public class InstaUserFeed
    {
        public InstaUserList SuggestedUsers { get; set; } = new InstaUserList();
        public IList<InstaMedia> FeedMedia { get; set; } = new List<InstaMedia>();
        public InstaFeedPageInfo FeedPageInfo { get; set; }
    }
}