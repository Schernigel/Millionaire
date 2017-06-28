using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Millionaire.WebUI.Infrastructure.Abstuct
{
    public interface IAuthProvider
    {
        bool Authenticate(string email, string password);
    }
}
