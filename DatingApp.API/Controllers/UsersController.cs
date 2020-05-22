using System.Threading.Tasks;
using DatingApp.API.Repos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DatingApp.API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly IDatingRepository repos;
        public UsersController(IDatingRepository repos)
        {
            this.repos = repos;

        }

        [HttpGet]
        public async Task<IActionResult> GetUsers()
        {
            var users = await repos.GetAllUsers();
            return Ok(users);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetUserById(int id)
        {
            var user = await repos.GetUser(id);
            return Ok(user);
        }

    }
}