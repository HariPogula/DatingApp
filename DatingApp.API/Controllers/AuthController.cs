using System.Threading.Tasks;
using DatingApp.API.DTO;
using DatingApp.API.Repos;
using Microsoft.AspNetCore.Mvc;

namespace DatingApp.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthRepository repo;

        public AuthController(IAuthRepository repo)
        {
            this.repo = repo;

        }
        [HttpPost("Register")]
        public async Task<IActionResult> Register([FromBody] UserRegisterDTO userRegisterDTO)
        {
            //TODO: Implement Realistic Implementation
            await Task.Yield();
            return Ok();
        }

    }
}