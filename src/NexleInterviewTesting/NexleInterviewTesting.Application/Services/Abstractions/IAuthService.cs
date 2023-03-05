using NexleInterviewTesting.Application.Dto;

namespace NexleInterviewTesting.Application.Services.Abstractions
{
    /// <summary>
    /// Authentication service to handle every auth operations
    /// </summary>
    public interface IAuthService
    {
        /// <summary>
        /// Handle user registeration action
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<(SignUpResultDto, string)> SignUp(SignUpDto input);

        /// <summary>
        /// Check email already used or not
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        Task<bool> IsEmailAlreadyUsed(string email);

        /// <summary>
        /// Handle login action
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<SignInResultDto> SignIn(SiginInInputDto input);
    }
}
