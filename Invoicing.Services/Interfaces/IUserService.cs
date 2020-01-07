using Invoicing.Data.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Invoicing.Services.Interfaces
{
    public interface IUserService
    {
        Task<User> GetUserById(int userId);
        Task<IEnumerable<User>> GetAllUsers();
        Task<User> CreateUser(User user, string password);
        Task<User> UpdateUser(User user);
        Task DeleteUser(User user);
    }
}
