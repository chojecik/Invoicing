using Invoicing.Core.Database.Entities;
using Invoicing.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Invoicing.BusinessLogic.Interfaces
{
    /// <summary>
    /// Interface containing methods for User class
    /// </summary>
    public interface IUserService : IService<User>
    {
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
        /// Gets user's invoices
        /// </summary>
        /// <param name="userId">Id of the user</param>
        /// <param name="type">Type of invoice (cost/sale)</param>
        /// <returns>Returns collection of user's invoices</returns>
        IEnumerable<Invoice> GerUsersInvoicesByType(int userId, InvoiceType type);
    }
}
