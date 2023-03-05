namespace NexleInterviewTesting.Domain.UnitOfWorks
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DbContext _context;

        public UnitOfWork(DbContext context)
        {
            _context = context;
        }

        /// <inheritdoc/>
        public int SaveChanges()
        {
            return _context.SaveChanges();
        }

        /// <inheritdoc/>
        public Task<int> SaveChangesAsync()
        {
            return _context.SaveChangesAsync();
        }
    }
}
