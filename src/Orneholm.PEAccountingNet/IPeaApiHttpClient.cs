using System.Threading.Tasks;

namespace Orneholm.PEAccountingNet
{
    internal interface IPeaApiHttpClient
    {
        Task<T> GetAsync<T>(string url);

        Task<TResult> PostAsync<TRequest, TResult>(string url, TRequest request);
        Task PostAsync<TRequest>(string url, TRequest request);

        Task<TResult> PutAsync<TRequest, TResult>(string url, TRequest request);
        Task PutAsync<TRequest>(string url, TRequest request);

        Task<T> DeleteAsync<T>(string url);
    }
}
