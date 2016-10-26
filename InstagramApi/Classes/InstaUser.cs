using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InstagramApi.Classes
{
    public class InstaUser
    {
        public string UserName { get; set; }

        public string ProfilePicture { get; set; }

        public int Id { get; set; }

        public string FullName { get; set; }

        public long InstaIdentifier { get; set; }
        public static InstaUser Empty
        {
            get
            {
                return new InstaUser() { FullName = string.Empty, Id = 0, UserName = string.Empty };
            }
        }
    }

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
        public int UserId { get; set; }
        public string Url { get; set; }
        public string Code { get; set; }

        public string Link { get; set; }

        public bool CanViewComment { get; set; }

        public DateTime CreatedTime { get; set; }

        public static InstaPost Empty
        {
            get
            {
                return new InstaPost(0, 0, string.Empty);
            }
        }

        public bool Equals(InstaPost post)
        {
            if (Id != post.Id) return false;
            if (Code != post.Code) return false;
            if (UserId != post.UserId) return false;
            return true;
        }

    }

    public class InstaPostList : List<InstaPost>
    {
    }

    public class InstaUserList : List<InstaUser>
    {
    }
}
