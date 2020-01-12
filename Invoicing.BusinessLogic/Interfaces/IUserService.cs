using Invoicing.Core.Entities;

namespace Invoicing.BusinessLogic.Interfaces
{
    public interface IUserService : IService<User>
    {
        User CreateUser(User user, string password);
        User Authenticate(string email, string password);
    }
}
