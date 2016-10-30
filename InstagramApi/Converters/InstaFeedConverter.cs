using System;
using InstagramApi.Classes;
using InstagramApi.ResponseWrappers;

namespace InstagramApi.Converters
{
    class InstaFeedConverter : IObjectConverter<InstaUserFeed, InstaFeedResponse>
    {
        public InstaFeedResponse SourceObject { get; set; }

        public InstaUserFeed Convert()
        {
            InstaUserFeed feed = new InstaUserFeed();
            foreach (var user in SourceObject.SuggestedUsers.Users)
            {
                var userConverter = ConvertersFabric.GetUserConverter(user);
                feed.SuggestedUsers.Add(userConverter.Convert());

            }
            foreach (var media in SourceObject.Feed.Media.Nodes)
            {
                var mediaConverter = ConvertersFabric.GetSingleMediaConverter(media);
                feed.FeedMedia.Add(mediaConverter.Convert());
            }
            feed.FeedPageInfo = new InstaFeedPageInfo()
            {
                EndCursor = SourceObject.Feed.Media.PageInfo.EndCursor,
                StartCursor = SourceObject.Feed.Media.PageInfo.StartCursor,
                HasNextPage = SourceObject.Feed.Media.PageInfo.HasNextPage,
                HasPrevPage = SourceObject.Feed.Media.PageInfo.HasPrevPage
            };
            return feed;
        }
    }
}