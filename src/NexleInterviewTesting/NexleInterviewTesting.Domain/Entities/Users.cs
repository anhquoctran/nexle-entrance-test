using Microsoft.AspNetCore.Identity;

namespace NexleInterviewTesting.Domain.Entities
{
    [Table("users")]
    public class User: IdentityUser<int>, IEntity, IHasCreationTime, IHasModificationTime
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime? UpdatedAt { get; set; }

        [InverseProperty(nameof(Token.User))]
        public virtual List<Token> Tokens { get; set; }

        public override string SecurityStamp { get; set; } = Guid.NewGuid().ToString();
    }
}
