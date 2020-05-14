using System.Linq;
using System.Threading.Tasks;
using DatingApp.API.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DatingApp.API.Controllers
{

    [Authorize]
    //[ApiVersion("1")]
    [ApiController]
    [Route("api/[controller]")]

    public class ValueController : ControllerBase
    {
        private readonly DataContext context;
        public ValueController(DataContext context)
        {
            this.context = context;

        }
        [HttpGet("GetSamples")]
        //[MapToApiVersion("1")]
        public async Task<IActionResult> GetSamples()
        {
            var values = await context.Sample.ToListAsync();

            return Ok(values);
        }

        [AllowAnonymous]

        [HttpGet("GetValues")]
        //   [MapToApiVersion("1")]
        public async Task<IActionResult> GetValues()
        {
            var values = await context.Value.ToListAsync();

            return Ok(values);
        }

    }
}