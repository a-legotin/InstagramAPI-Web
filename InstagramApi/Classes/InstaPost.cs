using System;

namespace InstagramApi.Classes
{
    public class InstaPost
    {
        public InstaPost(int id, InstaUser user, string code)
        {
            Id = id;
            User = user;
            Code = code;
        }

        public InstaPost()
        {
        }

        public int Id { get; set; }
        public long UserId => User.Id;
        public InstaUser User { get; set; }
        public string Code { get; set; }

        public string Link { get; set; }

        public bool CanViewComment { get; set; }

        public DateTime CreatedTime { get; set; }

        public Images Images { get; set; }

        public Likes Likes { get; set; }

        public int LikesCount => Likes?.Count ?? 0;

        public static InstaPost Empty => new InstaPost(0, InstaUser.Empty, string.Empty);

        public InstaPostType Type { get; set; }

        public InstaLocation Localtion { get; set; }

        public bool Equals(InstaPost post)
        {
            if (Id != post.Id) return false;
            if (Code != post.Code) return false;
            if (UserId != post.UserId) return false;
            return true;
        }
    }
}