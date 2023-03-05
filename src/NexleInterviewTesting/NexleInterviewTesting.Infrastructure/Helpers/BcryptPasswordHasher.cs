using Microsoft.AspNetCore.Identity;

namespace NexleInterviewTesting.Infrastructure.Helpers
{
    /// <summary>
    /// Custom password handler definition for ASP.NET Core Identity using Bcrypt
    /// </summary>
    public class BcryptPasswordHasher : IPasswordHasher<User>
    {
        public string HashPassword(User user, string password)
        {
            return PasswordHelpers.Hash(password);
        }

        public PasswordVerificationResult VerifyHashedPassword(User user, string hashedPassword, string providedPassword)
        {
            return PasswordHelpers.Verify(providedPassword, hashedPassword) ? PasswordVerificationResult.Success : PasswordVerificationResult.Failed;
        }
    }
}
