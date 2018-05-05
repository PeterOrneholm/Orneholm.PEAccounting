using System.Threading.Tasks;

namespace Orneholm.PEAccountingNet
{
    internal class PeaApiCompanyHttpClient : IPeaApiHttpClient
    {
        private readonly IPeaApiHttpClient _httpClient;
        private readonly int _companyId;

        public PeaApiCompanyHttpClient(int companyId, IPeaApiHttpClient httpClient)
        {
            _companyId = companyId;
            _httpClient = httpClient;
        }


        public Task<T> GetAsync<T>(string url)
        {
            return _httpClient.GetAsync<T>(GetCompanyUrl(url));
        }


        public Task<TResult> PostAsync<TRequest, TResult>(string url, TRequest request)
        {
            return _httpClient.PostAsync<TRequest, TResult>(GetCompanyUrl(url), request);
        }

        public Task PostAsync<TRequest>(string url, TRequest request)
        {
            return _httpClient.PostAsync<TRequest>(GetCompanyUrl(url), request);
        }


        public Task<TResult> PutAsync<TRequest, TResult>(string url, TRequest request)
        {
            return _httpClient.PutAsync<TRequest, TResult>(GetCompanyUrl(url), request);
        }

        public Task PutAsync<TRequest>(string url, TRequest request)
        {
            return _httpClient.PutAsync<TRequest>(GetCompanyUrl(url), request);
        }


        public Task<T> DeleteAsync<T>(string url)
        {
            return _httpClient.DeleteAsync<T>(GetCompanyUrl(url));
        }


        private string GetCompanyUrl(string url)
        {
            return $"/company/{_companyId}/{url.TrimStart('/')}";
        }
    }
}