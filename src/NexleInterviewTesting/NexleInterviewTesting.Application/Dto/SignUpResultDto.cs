namespace NexleInterviewTesting.Application.Dto
{
    public class SignUpResultDto
    {
        public int Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public string DisplayName
        {
            get
            {
                if (string.IsNullOrEmpty(FirstName) || string.IsNullOrEmpty(LastName)) return string.Empty;

                return $"{FirstName} {LastName}";
            }
        }
    }
}
