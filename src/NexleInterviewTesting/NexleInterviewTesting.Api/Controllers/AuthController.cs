using Microsoft.AspNetCore.Mvc;

namespace NexleInterviewTesting.Api.Controllers
{
    [Route("auth")]
    [ApiController]
    public class AuthController : BaseController
    {
        public IActionResult Index()
        {
            return NoContent();
        }
    }
}
