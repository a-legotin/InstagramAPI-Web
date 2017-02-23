using System.Threading.Tasks;
using InstagramApi.Classes;

namespace InstagramApi.API
{
    public interface IInstaApi
    {
        bool IsUserAuthenticated { get; }
        bool Login();

        InstaUser GetCurrentUser();
        InstaUser GetUser(string username);
        InstaPostList GetUserPosts(int pageCount = 0);
        InstaPostList GetUserPostsByUsername(string username, int pageCount = 0);
        InstaMedia GetMediaByCode(string postCode);
        InstaUserFeed GetUserFeed(int pageCount);


        Task<bool> LoginAsync();
        Task<InstaUser> GetCurrentUserAsync();
        Task<InstaUser> GetUserAsync(string username);
        Task<InstaPostList> GetUserPostsAsync(int pageCount = 0);

        Task<InstaPostList> GetUserPostsByUsernameAsync(string username, int pageCount = 0);

        Task<InstaMedia> GetMediaByCodeAsync(string postCode);
        Task<InstaUserFeed> GetUserFeedAsync(int pageCount);
    }
}