namespace InstagramApi.Classes
{
    public class InstaUser
    {
        public string UserName { get; set; }

        public string ProfilePicture { get; set; }

        public long Id { get; set; }

        public string FullName { get; set; }

        public long InstaIdentifier { get; set; }

        public static InstaUser Empty
        {
            get { return new InstaUser {FullName = string.Empty, Id = 0, UserName = string.Empty}; }
        }
    }
}