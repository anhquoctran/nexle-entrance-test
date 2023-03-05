using System.ComponentModel.DataAnnotations;

namespace NexleInterviewTesting.Api.Models
{
    public class RefreshTokenInputModel
    {
        [Required]
        public string RefreshToken { get; set; }
    }
}
