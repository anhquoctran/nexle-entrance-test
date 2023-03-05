namespace NexleInterviewTesting.Domain.Intefaces
{
    public interface IEntity<TPrimaryKey> : IPrimaryKey<TPrimaryKey> { }

    public interface IEntity : IEntity<int> { }
}
