using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using DatingApp.API.DTO;
using DatingApp.API.Models;
using DatingApp.API.Repos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace DatingApp.API.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthRepository repo;
        private readonly IConfiguration config;

        public AuthController(IAuthRepository repo, IConfiguration config)
        {
            this.config = config;
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

        [HttpPost("Login")]

        public async Task<IActionResult> Login(UserLoginDTO userLoginDTO)
        {
            var userFromRepo = await repo.Login(userLoginDTO.Username.ToLower(), userLoginDTO.Password);

            if (userFromRepo == null)
                return Unauthorized();

            var claims = new[]
            {
            new Claim(ClaimTypes.NameIdentifier,userFromRepo.Id.ToString()),
            new Claim(ClaimTypes.Name, userFromRepo.Username)
            };

            var key = new SymmetricSecurityKey
            (Encoding.UTF8.GetBytes(config.GetSection("AppSettings:Token").Value));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddDays(1),
                SigningCredentials = creds
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return Ok(new
            {
                token = tokenHandler.WriteToken(token)
            });
        }

    }

}

