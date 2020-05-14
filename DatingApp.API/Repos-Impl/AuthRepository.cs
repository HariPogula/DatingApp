using System.Threading.Tasks;
using DatingApp.API.Models;
using DatingApp.API.Repos;

namespace DatingApp.API.Repos_Impl
{
    public class AuthRepository : IAuthRepository
    {
        public Task<User> Login(string username, string password)
        {
            throw new System.NotImplementedException();
        }

        public Task<User> Register(User user)
        {
            throw new System.NotImplementedException();
        }

        public Task<bool> UserExists(string username)
        {
            throw new System.NotImplementedException();
        }
    }
}