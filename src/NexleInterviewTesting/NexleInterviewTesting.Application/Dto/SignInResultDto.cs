using System.Text.Json.Serialization;

namespace NexleInterviewTesting.Application.Dto
{
    public class SignInResultDto
    {
        public AuthUserDto User { get; set; }

        public string Token { get; set; }

        public string RefreshToken { get; set; }

        [JsonIgnore]
        public bool Success { get; set; }

        [JsonIgnore]
        public string Message { get; set; }
    }
}
