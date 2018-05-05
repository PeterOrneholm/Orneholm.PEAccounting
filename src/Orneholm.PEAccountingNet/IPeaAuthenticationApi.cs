using System.Collections.Generic;
using System.Threading.Tasks;
using Orneholm.PEAccountingNet.Models;

namespace Orneholm.PEAccountingNet
{
    public interface IPeaAuthenticationApi
    {
        Task<IEnumerable<accessiblecompany>> GetAccessibleCompaniesAsync(string email, string password);
    }
}