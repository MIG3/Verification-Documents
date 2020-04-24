using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using VerificationDocs.WebUI.Infrastructure.Abstract;

namespace VerificationDocs.WebUI.Infrastructure.Concrete
{
    public class FormAuthProvider : IAuthProvider
    {
        public bool Authenticate(string login, string password)
        {
            bool result = FormsAuthentication.Authenticate(login, password);
            if (result)
                FormsAuthentication.SetAuthCookie(login, false);
            return result;
        }
    }
}