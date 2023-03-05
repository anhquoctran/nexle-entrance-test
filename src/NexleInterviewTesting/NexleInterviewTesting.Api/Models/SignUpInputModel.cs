using System.ComponentModel.DataAnnotations;

namespace NexleInterviewTesting.Api.Models
{
    public class SignUpInputModel
    {
        [EmailAddress, Required]
        public string Email { get; set; }

        [Required, MinLength(8), MaxLength(20)]
        public string Password { get; set; }

        [MaxLength(30)]
        public string FirstName { get; set; }

        [MaxLength(30)]
        public string LastName { get; set; }
    }
}
