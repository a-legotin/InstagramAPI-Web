using System;

namespace InstagramApi.Classes
{
    public class InstaPost
    {
        public InstaPost(int id, int userId, string code)
        {
            Id = id;
            UserId = userId;
            Code = code;
        }

        public InstaPost()
        {
        }

        public int Id { get; set; }
        public long UserId { get; set; }
        public string Url { get; set; }
        public string Code { get; set; }

        public string Link { get; set; }

        public bool CanViewComment { get; set; }

        public DateTime CreatedTime { get; set; }

        public static InstaPost Empty => new InstaPost(0, 0, string.Empty);

        public bool Equals(InstaPost post)
        {
            if (Id != post.Id) return false;
            if (Code != post.Code) return false;
            if (UserId != post.UserId) return false;
            return true;
        }
    }
}