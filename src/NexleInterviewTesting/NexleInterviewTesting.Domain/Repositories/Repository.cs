using Microsoft.EntityFrameworkCore;

namespace NexleInterviewTesting.Domain.Repositories
{
    /// <summary>
    /// Basic implementation of IRepository
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    /// <typeparam name="TPrimaryKey"></typeparam>
    public class Repository<TEntity, TPrimaryKey> : IRepository<TEntity, TPrimaryKey>
        where TEntity : class, IEntity<TPrimaryKey>
    {
        private readonly DbContext _dbContext;

        public Repository(DbContext dbContext)
        {
            _dbContext = dbContext;
        }

        /// <inheritdoc/>
        public TEntity Add(TEntity entity)
        {
            var added = _dbContext.Set<TEntity>().Add(entity);
            return added.Entity;
        }

        /// <inheritdoc/>
        public void Delete(TPrimaryKey id)
        {
            var item = Get(id);
            _dbContext.Set<TEntity>().Remove(item);
        }

        /// <inheritdoc/>
        public TEntity Get(TPrimaryKey key)
        {
            return _dbContext.Set<TEntity>().First(x => x.Id.Equals(key));
        }

        /// <inheritdoc/>
        public IQueryable<TEntity> GetAll()
        {
            return _dbContext.Set<TEntity>().AsQueryable();
        }

        /// <inheritdoc/>
        public TEntity[] ToArray()
        {
            return _dbContext.Set<TEntity>().AsQueryable().ToArray();
        }

        /// <inheritdoc/>
        public List<TEntity> ToList()
        {
            return _dbContext.Set<TEntity>().AsQueryable().ToList();
        }

        /// <inheritdoc/>
        public TEntity Update(TEntity entity)
        {
            var set = _dbContext.Set<TEntity>();
            var item = set.FirstOrDefault(x => x.Id.Equals(entity.Id));

            if (item != null)
            {
                _dbContext.Entry(item).CurrentValues.SetValues(entity);
                return item;
            }

            return entity;
        }
    }

    public class Repository<TEntity> : Repository<TEntity, int>, IRepository<TEntity, int>
        where TEntity : class, IEntity<int>
    { 
        public Repository(DbContext dbContext) : base(dbContext) { }
    }
}
