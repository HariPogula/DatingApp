using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace DatingApp.API.Controllers
{
    public class AuthController : ControllerBase
    {
        public AuthController()
        {

        }
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            //TODO: Implement Realistic Implementation
            await Task.Yield();
            return Ok();
        }

    }
}