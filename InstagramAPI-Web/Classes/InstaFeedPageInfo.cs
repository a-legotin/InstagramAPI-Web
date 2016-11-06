namespace InstagramApi.Classes
{
    public class InstaFeedPageInfo
    {
        public bool HasNextPage { get; set; }
        public bool HasPrevPage { get; set; }
        public string StartCursor { get; set; }
        public string EndCursor { get; set; }
    }
}