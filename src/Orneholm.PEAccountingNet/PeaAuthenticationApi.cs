using System.Collections.Generic;
using System.Threading.Tasks;
using Orneholm.PEAccountingNet.Models;

namespace Orneholm.PEAccountingNet
{
    public class PeaAuthenticationApi
    {
        private readonly IPeaApiHttpClient _httpClient;

        public PeaAuthenticationApi() : this(new PeaApiHttpClient())
        {
        }

        public PeaAuthenticationApi(IPeaApiHttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        // Authenticate

        public async Task<IEnumerable<accessiblecompany>> GetAccessibleCompaniesAsync(string email, string password)
        {
            var result = await _httpClient.PostAsync<userauthentication, accessiblecompanies>("/access/authenticate", new userauthentication()
            {
                email = email,
                password = password
            });

            return PeaApiHelpers.TransformListResult(result, x => x.accessiblecompany);
        }
    }
}