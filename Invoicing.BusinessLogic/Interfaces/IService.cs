using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Invoicing.BusinessLogic.Interfaces
{
    /// <summary>
    /// Interface containing basic methods for CRUD operations
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IService<T> where T : class
    {
        /// <summary>
        /// Gets all records of T type from the database
        /// </summary>
        /// <returns>Returns the IEmumerable collection of T type</returns>
        IEnumerable<T> GetAll();

        /// <summary>
        /// Gets the record from the database by it's ID
        /// </summary>
        /// <param name="id">ID of the record</param>
        /// <returns>Returns single object of T type</returns>
        T GetById(int id);

        /// <summary>
        /// Gets records of type T from the database by provided condition
        /// </summary>
        /// <param name="predicate">Condition</param>
        /// <returns>Returns IEnumerabl collection of T type</returns>
        IEnumerable<T> Find(Expression<Func<T, bool>> predicate);

        /// <summary>
        /// Inserts the entity to the database
        /// </summary>
        /// <param name="entity">Entity to insert</param>
        /// <returns>Returns created entity of T type</returns>
        T Add(T entity);

        /// <summary>
        /// Updates the record in the database
        /// </summary>
        /// <param name="entity">Record to update</param>
        /// <returns>Returns updated entity of T type</returns>
        T Update(T entity);

        /// <summary>
        /// Deletes the record from the database
        /// </summary>
        /// <param name="entity">Entity to delete</param>
        void Delete(T entity);
    }
}
