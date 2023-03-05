using Microsoft.Extensions.Configuration;
using NexleInterviewTesting.Domain;
using NexleInterviewTesting.Domain.Repositories;
using NexleInterviewTesting.Domain.UnitOfWorks;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace NexleInterviewTesting.Application.Services.Implementations
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<User> _userManager;
        private readonly IRepository<Token, int> _tokenRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly SignInManager<User> _signInManager;
        private readonly IConfiguration _config;

        public AuthService(
            UserManager<User> userManager,
            IRepository<Token, int> tokenRepository,
            IUnitOfWork unitOfWork,
            SignInManager<User> signInManager,
            IConfiguration config
            )
        {
            _userManager = userManager;
            _tokenRepository = tokenRepository;
            _unitOfWork = unitOfWork;
            _config = config;
            _signInManager = signInManager;
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

        /// <inheritdoc/>
        public async Task<SignInResultDto> SignIn(SiginInInputDto input)
        {
            var user = await _userManager.Users.FirstOrDefaultAsync(e => e.Email == input.Email);

            if (user != null && await _userManager.CheckPasswordAsync(user, input.Password))
            {
                var (token, refreshToken) = GenerateToken(user);

                var tokenItem = new Token
                {
                    RefreshToken = refreshToken,
                    UserId = user.Id,
                    ExpiresIn = DateTime.Now.AddDays(30).ToString(Constants.DATE_TIME_FORMAT)
                };

                _tokenRepository.Add(tokenItem);
                await _unitOfWork.SaveChangesAsync();

                return new SignInResultDto
                {
                    User = new AuthUserDto
                    {
                        Email = user.Email,
                        FirstName = user.FirstName,
                        LastName = user.LastName,
                    },
                    Success = true,
                    Token = token,
                    RefreshToken = refreshToken,
                };
            }

            return new SignInResultDto { Success = false, Message = "Invalid username or password!" };
        }

        private (string, string) GenerateToken(User user)
        {
            var claims = new List<Claim>
            {
                new Claim("id", user?.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.Sub, user?.Id.ToString() ?? "0"),
                new Claim(JwtRegisteredClaimNames.Email, user?.UserName ?? string.Empty),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            var token = TokenHelpers.GenerateJwtToken(
                claims,
                _config["Authentication:JwtBearer:SecurityKey"],
                _config["Authentication:JwtBearer:Issuer"],
                _config["Authentication:JwtBearer:Audience"],
                TimeSpan.FromHours(1)
            );

            var refreshToken = StringHelpers.GenerateRandomString(250);

            return (token, refreshToken);
        }
    }
}
