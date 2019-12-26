using System.Net.Http;
using System.Net.Http.Headers;
using System.Reflection;

namespace Orneholm.PEAccountingNet.Helpers
{
    internal class UserAgentHelper
    {
        public static void ApplyUserAgent(HttpClient httpClient)
        {
            var product = GetProduct();
            var version = GetAssemblyVersion();
            var productInfo = new ProductInfoHeaderValue(product, version);

            httpClient.DefaultRequestHeaders.UserAgent.Add(productInfo);
        }

        private static string GetProduct()
        {
            return GetAssembly().GetCustomAttribute<AssemblyProductAttribute>()
                .Product;
        }

        private static string GetAssemblyVersion()
        {
            return GetAssembly().GetCustomAttribute<AssemblyFileVersionAttribute>()
                .Version;
        }

        private static Assembly GetAssembly()
        {
            return typeof(IPeaApi)
                .GetTypeInfo()
                .Assembly;
        }
    }
}