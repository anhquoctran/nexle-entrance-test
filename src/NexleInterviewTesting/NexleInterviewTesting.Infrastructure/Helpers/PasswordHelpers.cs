namespace NexleInterviewTesting.Infrastructure.Helpers
{
    public static class PasswordHelpers
    {
        /// <summary>
        /// Hash password by using Bcrypt
        /// </summary>
        /// <param name="password"></param>
        /// <returns></returns>
        public static string Hash(string password)
        {
            return BCrypt.Net.BCrypt.HashPassword(password, 10);
        }

        /// <summary>
        /// Hash password by using Bcrypt
        /// </summary>
        /// <param name="password"></param>
        /// <returns></returns>
        public static bool Verify(string password, string cipher)
        {
            return BCrypt.Net.BCrypt.Verify(password, cipher);
        }
    }
}
