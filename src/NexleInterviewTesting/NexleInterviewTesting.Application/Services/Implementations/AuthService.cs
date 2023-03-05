namespace NexleInterviewTesting.Application.Services.Implementations
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<User> _userManager;

        public AuthService(UserManager<User> userManager)
        {
            _userManager = userManager;
        }

        /// <inheritdoc/>
        public async Task<(SignUpResultDto, string)> SignUp(SignUpDto input)
        {
            var result = await _userManager.CreateAsync(new User
            {
                Email = input.Email.Trim(),
                FirstName = input.FirstName,
                LastName = input.LastName,
                PasswordHash = PasswordHelpers.Hash(input.Password),
                UserName = input.Email.Trim()
            });

            if (result.Succeeded)
            {
                var newUser = await _userManager.Users.FirstOrDefaultAsync(x => x.Email == input.Email.Trim());

                var resultUser = new SignUpResultDto
                {
                    Email = input.Email,
                    FirstName = input.FirstName,
                    LastName = input.LastName,
                    Id = newUser.Id
                };

                return (resultUser, null);
            }

            return (null, string.Join(',', result.Errors.Select(x => x.Description)));
        }

        /// <inheritdoc/>
        public async Task<bool> IsEmailAlreadyUsed(string email)
        {
            return await _userManager.Users.AnyAsync(x => x.Email == email);
        }
    }
}
