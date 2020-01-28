using Invoicing.Core.Database.Entities;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Invoicing.BusinessLogic.Interfaces
{
    /// <summary>
    /// Interface containing methods for User class
    /// </summary>
    public interface IUserService
    {
        /// <summary>
        /// Gets all records of User type from the database
        /// </summary>
        /// <returns>Returns the IEmumerable collection of T type</returns>
        IEnumerable<User> GetAll();

        /// <summary>
        /// Gets the record from the database by it's ID
        /// </summary>
        /// <param name="id">ID of the record</param>
        /// <returns>Returns single object of T type</returns>
        User GetById(int id);

        /// <summary>
        /// Gets records of type User from the database by provided condition
        /// </summary>
        /// <param name="predicate">Condition</param>
        /// <returns>Returns IEnumerabl collection of T type</returns>
        IEnumerable<User> Find(Expression<Func<User, bool>> predicate);

        /// <summary>
        /// Inserts new user to the database
        /// </summary>
        /// <param name="user">Entity to insert</param>
        /// <param name="password">User's password</param>
        /// <returns>Returns created user</returns>
        User CreateUser(User user, string password);

        /// <summary>
        /// Authenticates the user
        /// </summary>
        /// <param name="email">User's email</param>
        /// <param name="password">User's password</param>
        /// <returns>Returns authenticated user</returns>
        User Authenticate(string email, string password);

        /// <summary>
        /// Deletes the record from the database
        /// </summary>
        /// <param name="entity">Entity to delete</param>
        void Delete(User entity);
    }
}
