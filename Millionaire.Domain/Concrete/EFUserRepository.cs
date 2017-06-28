using System;
using System.Linq;
using Millionaire.Domain.Abstract;
using Millionaire.Domain.Entities;

namespace Millionaire.Domain.Concrete
{
    public class EfUserRepository : IUserRepository,IDisposable
    {
        private EFDbContext context;

        public EfUserRepository(EFDbContext context)
        {
            this.context = context;
        } 

        public void InsertUser(User user)
        {
            context.Users.Add(user);
        }

        public User GetUser(string email, string password)
        {

            return context.Users.FirstOrDefault(u=>u.Email == email && u.Password == password);
        }

        public bool IsUserExist(string email, string password)
        {
            if (GetUser(email, password) != null)
            {
                return true;
            }
            return false;
        }

        public void Save()
        {
            context.SaveChanges();
        }

        private bool _disposed = false;
        protected virtual void Dispose(bool disposing)
        {
            if (!this._disposed)
            {
                if (disposing)
                {
                    context.Dispose();
                }
            }
            this._disposed = true;
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
