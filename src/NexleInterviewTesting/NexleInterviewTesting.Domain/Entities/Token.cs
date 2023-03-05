namespace NexleInterviewTesting.Domain.Entities
{
    [Table("tokens")]
    public class Token: IEntity, IHasCreationTime, IHasModificationTime
    {
        [Key, Required]
        public int Id { get; set; }

        [Required]
        public int UserId { get; set; }

        [Required]
        public string RefreshToken { get; set; }

        [Required]
        public string ExpiresIn { get; set; }

        [Required]
        public long ExpireInMs { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;

        public DateTime? UpdatedAt { get; set; }

        [ForeignKey("userId")]
        public virtual User User { get; set; }
    }
}
