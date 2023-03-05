namespace NexleInterviewTesting.Api.Controllers
{
    [Route("auth")]
    [ApiController]
    public class AuthController : BaseController
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        /// <summary>
        /// Sign Up action
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost("signup")]
        public async Task<IActionResult> SignUp([FromBody] SignUpInputModel model)
        {
            var existsEmail = await _authService.IsEmailAlreadyUsed(model.Email);

            if (existsEmail)
            {
                return BadRequest($"Email address '{model.Email}' is already taken.");
            }

            var (user, message) = await _authService.SignUp(new SignUpDto
            {
                Email = model.Email,
                FirstName = model.FirstName,
                LastName = model.LastName,
                Password = model.Password,
            });

            if (user != null)
            {
                return Succeed(user, 201);
            }

            return ServerError(message);
        }
    }
}
