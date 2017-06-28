using Millionaire.Domain.Entities;

namespace Millionaire.Domain.Abstract
{
    public interface IUserRepository
    {
        User GetUser(string email, string password);

        void InsertUser(User user);

        bool IsUserExist(string email, string password);
        void Save();
    }
}
