using System.ComponentModel.DataAnnotations;

namespace NexleInterviewTesting.Api.Models
{
    public class SignInInputModel
    {
        [Required, EmailAddress]
        public string Email { get; set; }

        [Required, MinLength(8), MaxLength(20)]
        public string Password { get; set; }
    }
}
