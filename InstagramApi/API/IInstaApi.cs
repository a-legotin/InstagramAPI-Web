using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InstagramApi.Classes;

namespace InstagramApi
{
    public interface IInstaApi
    {
        InstaUser GetUser();
        Task<InstaUser> GetUserAsync();
    }
}
