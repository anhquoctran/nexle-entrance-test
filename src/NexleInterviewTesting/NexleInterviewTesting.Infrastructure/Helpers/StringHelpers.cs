namespace NexleInterviewTesting.Infrastructure.Helpers
{
    public static class StringHelpers
    {

        /// <summary>
        /// This method help generate a refresh token by generate a random string
        /// </summary>
        /// <param name="length">Length of output random string</param>
        /// <returns></returns>
        public static string GenerateRandomString(int length)
        {
            var random = new Random();
            const string chars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length)
                .Select(s => s[random.Next(s.Length)]).ToArray());
        }
    }
}
