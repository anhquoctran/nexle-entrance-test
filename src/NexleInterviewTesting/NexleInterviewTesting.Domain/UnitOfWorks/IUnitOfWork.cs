namespace NexleInterviewTesting.Domain.UnitOfWorks
{
    /// <summary>
    /// Manage and control database transaction
    /// </summary>
    public interface IUnitOfWork
    {
        /// <summary>
        /// Save any changes of current UoW transaction to the database asynchronously
        /// </summary>
        /// <returns></returns>
        Task<int> SaveChangesAsync();

        /// <summary>
        /// Save any changes of current UoW transaction to the database
        /// </summary>
        /// <returns></returns>
        int SaveChanges();
    }
}
