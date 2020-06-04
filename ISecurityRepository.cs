using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VerificationDocs.Domain.Entities;

namespace VerificationDocs.Domain.Abstract
{
    public interface ISecurityRepository
    {
        IEnumerable<Security> UsersSec { get; }

        void SaveUserSecurity(Security security);
    }
}
