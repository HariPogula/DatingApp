using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using DatingApp.API.DTO;
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
        private readonly IMapper mapper;
        public UsersController(IDatingRepository repos, IMapper mapper)
        {
            this.mapper = mapper;
            this.repos = repos;

        }

        [HttpGet]
        public async Task<IActionResult> GetUsers()
        {
            var users = await repos.GetAllUsers();
            var usersToReturn = mapper.Map<IEnumerable<UserForListDTO>>(users);
            return Ok(usersToReturn);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetUserById(int id)
        {
            var user = await repos.GetUser(id);
            var userToReturn = mapper.Map<UserForDetailDTO>(user);
            return Ok(userToReturn);
        }

    }
}