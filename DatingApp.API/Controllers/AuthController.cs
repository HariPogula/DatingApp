using System.Threading.Tasks;
using DatingApp.API.DTO;
using DatingApp.API.Models;
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
        public async Task<IActionResult> Register(UserRegisterDTO userRegisterDTO)
        {
            // Since we are using [ApiController] It will take care of inputs and validation errors.
            // [FromBody]
            // if (!ModelState.IsValid)
            //     return BadRequest(ModelState);

            userRegisterDTO.Username = userRegisterDTO.Username.ToLower();
            if (await repo.UserExists(userRegisterDTO.Username))
            {
                return BadRequest("Username is Already Exists");
            }

            var userToCreate = new User
            {
                Username = userRegisterDTO.Username
            };
            var createdUser = await repo.Register(userToCreate, userRegisterDTO.Password);
            return StatusCode(201);
        }

    }
}