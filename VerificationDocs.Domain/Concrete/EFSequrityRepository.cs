using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VerificationDocs.Domain.Abstract;
using VerificationDocs.Domain.Entities;

namespace VerificationDocs.Domain.Concrete
{
    class EFSequrityRepository : ISecurityRepository
    {
        SecurityContext context = new SecurityContext();
        public IEnumerable<Security> UsersSec
        {
            get { return context.UsersSec; }
        }

        public void SaveUserSecurity(Security security)
        {
            if (security.ID == 0)
                context.UsersSec.Add(security);
            else
            {
                Security dbEntry = context.UsersSec.Find(security.ID);
                if (dbEntry != null)
                {
                    dbEntry.Login = security.Login;
                    dbEntry.Password = security.Password;
                    dbEntry.Role = security.Role;
                }
        
            }
            context.SaveChanges();
        }
    }
}
