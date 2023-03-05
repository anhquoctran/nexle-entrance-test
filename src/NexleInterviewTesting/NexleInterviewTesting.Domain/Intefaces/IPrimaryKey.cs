namespace NexleInterviewTesting.Domain.Intefaces
{
    public interface IPrimaryKey<TPrimaryKey>
    {
        TPrimaryKey Id { get; set; }
    }
}
