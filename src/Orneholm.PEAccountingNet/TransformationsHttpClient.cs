using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Orneholm.PEAccountingNet
{
    public static class PeaApiHttpClientExtensions
    {
        public static async Task<TItem> GetSingleAsync<TNativeItem, TItem>(this IPeaApiHttpClient httpClient, string url, Func<TNativeItem, TItem> transformItem)
        {
            var result = await httpClient.GetAsync<TNativeItem>(url);
            if (result == null)
            {
                return default(TItem);
            }

            return transformItem(result);
        }

        public static async Task<IEnumerable<TItem>> GetListAsync<TResult, TNativeItem, TItem>(this IPeaApiHttpClient httpClient, string url, Func<TResult, IEnumerable<TNativeItem>> getValue, Func<TNativeItem, TItem> transformItem)
        {
            var result = await httpClient.GetAsync<TResult>(url);
            return PeaApiHelpers.TransformListResult(result, getValue, transformItem);
        }

        public static async Task<TItem> PutAsync<TRequest, TNativeItem, TItem>(this IPeaApiHttpClient httpClient, string url, TRequest request, Func<TNativeItem, TItem> transformItem)
        {
            var result = await httpClient.PutAsync<TRequest, TNativeItem>(url, request);
            if (result == null)
            {
                return default(TItem);
            }

            return transformItem(result);
        }

        public static async Task<TItem> PostAsync<TRequest, TNativeItem, TItem>(this IPeaApiHttpClient httpClient, string url, TRequest request, Func<TNativeItem, TItem> transformItem)
        {
            var result = await httpClient.PostAsync<TRequest, TNativeItem>(url, request);
            if (result == null)
            {
                return default(TItem);
            }

            return transformItem(result);
        }

        public static async Task<TItem> DeleteAsync<TNativeItem, TItem>(this IPeaApiHttpClient httpClient, string url, Func<TNativeItem, TItem> transformItem)
        {
            var result = await httpClient.DeleteAsync<TNativeItem>(url);
            if (result == null)
            {
                return default(TItem);
            }

            return transformItem(result);
        }
    }
}