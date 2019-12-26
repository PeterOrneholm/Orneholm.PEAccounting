using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Orneholm.PEAccountingNet.Helpers;
using Orneholm.PEAccountingNet.Models;
using Orneholm.PEAccountingNet.Models.Native;

namespace Orneholm.PEAccountingNet
{
    public class PeaAuthenticationApi : IPeaAuthenticationApi
    {
        private readonly IPeaApiHttpClient _httpClient;

        public PeaAuthenticationApi(HttpClient httpClient)
        {
            _httpClient = new PeaApiHttpClient(httpClient);
        }

        public static PeaAuthenticationApi CreateClient()
        {
            return CreateClient(string.Empty);
        }

        public static PeaAuthenticationApi CreateClient(string accessToken)
        {
            var httpClient = new HttpClient
            {
                BaseAddress = PeaApiDefaults.ProductionApiBaseUrl
            };

            if (!string.IsNullOrEmpty(accessToken))
            {
                httpClient.DefaultRequestHeaders.Add(PeaApiDefaults.AccessTokenHeaderName, accessToken);
            }

            return new PeaAuthenticationApi(httpClient);
        }


        // Authenticate

        public async Task<IEnumerable<AccessibleCompany>> GetAccessibleCompaniesAsync(string email, string password)
        {
            var result = await _httpClient.PostAsync<userauthentication, accessiblecompanies>("/access/authenticate", new userauthentication()
            {
                email = email,
                password = password
            });

            return TransformLists.TransformListResult(result, x => x.accessiblecompany, AccessibleCompany.FromNative);
        }
    }
}
