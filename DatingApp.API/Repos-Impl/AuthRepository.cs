using System.Threading.Tasks;
using DatingApp.API.Data;
using DatingApp.API.Models;
using DatingApp.API.Repos;
using Microsoft.EntityFrameworkCore;

namespace DatingApp.API.Repos_Impl
{
    public class AuthRepository : IAuthRepository
    {
        private readonly DataContext _context;

        public AuthRepository(DataContext context)
        {
            this._context = context;

        }
        public async Task<User> Login(string username, string password)
        {
            var user = await _context.User.FirstOrDefaultAsync(u => u.Username == username);
            if (user == null) return null;
            // If password is not correct after decrypting using Hash and Salt.
            if (!VerifyPasswordHash(password, user.PasswordHash, user.PasswordSalt))
            {
                return null;
            }
            return user;
        }

        private bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512(passwordSalt))
            {

                var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                for (int i = 0; i < computedHash.Length; i++)
                {
                    if (computedHash[i] != passwordHash[i]) return false;
                }
            }
            return true;
        }

        public async Task<User> Register(User user, string password)
        {
            byte[] passwordHash, passwordSalt;
            CreatePasswordHash(password, out passwordHash, out passwordSalt);
            user.PasswordHash = passwordHash;
            user.PasswordSalt = passwordSalt;
            await _context.User.AddAsync(user);
            await _context.SaveChangesAsync();
            return user;
        }

        private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));

            }

        }

        public async Task<bool> UserExists(string username)
        {
            if (await _context.User.AnyAsync(x => x.Username == username))
            {
                return true;
            }
            return false;
        }

        public Task<User> Register(User user)
        {
            throw new System.NotImplementedException();
        }
    }
}