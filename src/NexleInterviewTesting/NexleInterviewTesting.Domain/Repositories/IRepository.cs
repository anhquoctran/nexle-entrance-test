namespace NexleInterviewTesting.Domain.Repositories
{
    /// <summary>
    /// Represents the common database operations for the entity (Cread, Read, Update, Delete)
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    /// <typeparam name="TPrimaryKey"></typeparam>
    public interface IRepository<TEntity, TPrimaryKey> where TEntity : class, IEntity<TPrimaryKey>
    {
        /// <summary>
        /// Adding entity to database sets
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        TEntity Add(TEntity entity);

        /// <summary>
        /// Update existing entity to database sets
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        TEntity Update(TEntity entity);

        /// <summary>
        /// Get entity by primary key
        /// </summary>
        /// <param name="key">Primary key to get</param>
        /// <returns></returns>
        TEntity Get(TPrimaryKey key);

        /// <summary>
        /// Used to get a IQueryable that is used to query entities from entire table.
        /// </summary>
        /// <returns></returns>
        IQueryable<TEntity> GetAll();

        /// <summary>
        /// Get all entites from table and convert to an Array
        /// </summary>
        /// <returns></returns>
        TEntity[] ToArray();

        /// <summary>
        /// Get all entites from table and convert to a List
        /// </summary>
        /// <returns></returns>
        List<TEntity> ToList();

        /// <summary>
        /// Delete the entity by primary key
        /// </summary>
        /// <param name="id"></param>
        void Delete(TPrimaryKey id);
    }
}
