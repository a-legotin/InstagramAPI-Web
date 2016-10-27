using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InstagramApi.Classes;
using InstagramApi.ResponseWrappers;

namespace InstagramApi.Converters
{
    internal class ConvertersFabric
    {
        internal static IObjectConverter<InstaPostList, InstaResponse> GetPostsConverter(InstaResponse instaresponse)
        {
            return new InstaPostsConverter() { SourceObject = instaresponse };
        }

        internal static IObjectConverter<InstaUser, InstaResponseUser> GetUserConverter(InstaResponseUser instaresponse)
        {
            return new InstaUsersConverter() { SourceObject = instaresponse };
        }
    }
    public class InstaPostsConverter : IObjectConverter<InstaPostList, InstaResponse>
    {
        public InstaResponse SourceObject { get; set; }

        public InstaPostList Convert()
        {
            if (SourceObject == null) throw new ArgumentNullException("Source object");
            var instaPosts = new InstaPostList();
            foreach (var post in SourceObject.Items)
            {
                instaPosts.Add(new InstaPost()
                {
                    Url = post.Link,
                    CanViewComment = post.CanViewComment,
                    Code = post.Code,
                    CreatedTime = post.CreatedTimeConverted
                });
            };
            return instaPosts;
        }
    }

    public class InstaUsersConverter : IObjectConverter<InstaUser, InstaResponseUser>
    {
        public InstaResponseUser SourceObject
        {
            get; set;
        }

        public InstaUser Convert()
        {
            if (SourceObject == null) throw new ArgumentNullException("Source object");
            var user = new InstaUser();
            user.FullName = SourceObject.FullName;
            user.Id = SourceObject.Id;
            user.ProfilePicture = SourceObject.ProfilePicture;
            user.UserName = SourceObject.UserName;
            return user;
        }
    }

    interface IObjectConverter<T, TT>
    {
        TT SourceObject { get; set; }
        T Convert();
    }
}
