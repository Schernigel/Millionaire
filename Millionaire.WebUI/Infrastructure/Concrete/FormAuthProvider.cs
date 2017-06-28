using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using Millionaire.Domain.Abstract;
using Millionaire.Domain.Entities;
using Millionaire.WebUI.Infrastructure.Abstuct;

namespace Millionaire.WebUI.Infrastructure.Concrete
{
    public class FormAuthProvider : IAuthProvider
    {
        private IUserRepository repository;

        public FormAuthProvider(IUserRepository repo)
        {
            repository = repo;
        }
        public bool Authenticate(string email, string password)
        {
            bool result = repository.IsUserExist(email, password);
            if (result)
            {
                FormsAuthentication.SetAuthCookie(email, false);
            }
            return result;
        }
    }
}