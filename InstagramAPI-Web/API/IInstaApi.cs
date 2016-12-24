using System.Threading.Tasks;
using InstagramApi.Classes;

namespace InstagramApi.API
{
    public interface IInstaApi
    {
        bool IsUserAuthenticated { get; }
        InstaUser GetUser();
        Task<InstaUser> GetUserAsync();
        InstaPostList GetUserPosts(int pageCount = 0);
        InstaPostList GetUserPostsByUsername(string username, int pageCount = 0);

        Task<InstaPostList> GetUserPostsByUsernameAsync(string username, int pageCount = 0);

        Task<InstaPostList> GetUserPostsAsync(int pageCount = 0);
        InstaMedia GetMediaByCode(string postCode);
        Task<InstaMedia> GetMediaByCodeAsync(string postCode);
        bool Login();
        Task<bool> LoginAsync();
        InstaUserFeed GetUserFeed(int pageCount);
        Task<InstaUserFeed> GetUserFeedAsync(int pageCount);
    }
}