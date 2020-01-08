using Invoicing.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Invoicing.BusinessLogic.Interfaces
{
    public interface IUserService : IService<User>
    {
        User CreateUser(User user, string password);
    }
}
