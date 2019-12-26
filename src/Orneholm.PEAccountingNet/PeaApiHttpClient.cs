using System;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Reflection;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;

namespace Orneholm.PEAccountingNet
{
    public class PeaApiDefaults
    {
        public static readonly Uri ProductionApiBaseUrl = new Uri("https://my.accounting.pe/api/v1/");
        public const string AccessTokenHeaderName = "X-Token";
    }

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

    internal class PeaApiHttpClient : IPeaApiHttpClient
    {
        private readonly HttpClient _httpClient;

        public static PeaApiHttpClient CreateClient()
        {
            return CreateClient(string.Empty);
        }

        public static PeaApiHttpClient CreateClient(string accessToken)
        {
            var httpClient = new HttpClient
            {
                BaseAddress = PeaApiDefaults.ProductionApiBaseUrl
            };

            if (!string.IsNullOrEmpty(accessToken))
            {
                httpClient.DefaultRequestHeaders.Add(PeaApiDefaults.AccessTokenHeaderName, accessToken);
            }

            return new PeaApiHttpClient(httpClient);
        }

        public PeaApiHttpClient(HttpClient httpClient)
        {
            UserAgentHelper.ApplyUserAgent(httpClient);
            _httpClient = httpClient;
        }


        public async Task<T> GetAsync<T>(string url)
        {
            var httpResponseMessage = await _httpClient.GetAsync(CleanUrl(url));
            EnsureSuccess(httpResponseMessage);
            return await GetDeserializedResponseAsync<T>(httpResponseMessage.Content);
        }


        public Task<TResult> PostAsync<TRequest, TResult>(string url, TRequest request)
        {
            return PostPutAsync<TRequest, TResult>(url, request,
                (cleanUrl, httpContent) => _httpClient.PostAsync(cleanUrl, httpContent)
            );
        }

        public async Task PostAsync<TRequest>(string url, TRequest request)
        {
            await PostPutAsync<TRequest>(url, request,
                (cleanUrl, httpContent) => _httpClient.PostAsync(cleanUrl, httpContent)
            );
        }


        public Task<TResult> PutAsync<TRequest, TResult>(string url, TRequest request)
        {
            return PostPutAsync<TRequest, TResult>(url, request,
                (cleanUrl, httpContent) => _httpClient.PutAsync(cleanUrl, httpContent)
            );
        }

        public async Task PutAsync<TRequest>(string url, TRequest request)
        {
            await PostPutAsync<TRequest>(url, request,
                (cleanUrl, httpContent) => _httpClient.PutAsync(cleanUrl, httpContent)
            );
        }


        public async Task<T> DeleteAsync<T>(string url)
        {
            var httpResponseMessage = await _httpClient.DeleteAsync(CleanUrl(url));
            EnsureSuccess(httpResponseMessage);
            return await GetDeserializedResponseAsync<T>(httpResponseMessage.Content);
        }



        public async Task<TResult> PostPutAsync<TRequest, TResult>(string url, TRequest request, Func<string, HttpContent, Task<HttpResponseMessage>> httpRequest)
        {
            var httpResponseMessage = await GetHttpResponse<TRequest>(url, request, httpRequest);
            return await GetDeserializedResponseAsync<TResult>(httpResponseMessage.Content);
        }

        public async Task PostPutAsync<TRequest>(string url, TRequest request, Func<string, HttpContent, Task<HttpResponseMessage>> httpRequest)
        {
            await GetHttpResponse(url, request, httpRequest);
        }

        private async Task<HttpResponseMessage> GetHttpResponse<TRequest>(string url, TRequest request, Func<string, HttpContent, Task<HttpResponseMessage>> httpRequest)
        {
            var requestXml = SerializeXml(request);
            var httpContent = new StringContent(requestXml);

            var httpResponseMessage = await httpRequest(CleanUrl(url), httpContent);
            EnsureSuccess(httpResponseMessage);
            return httpResponseMessage;
        }

        private void EnsureSuccess(HttpResponseMessage httpResponseMessage)
        {
            httpResponseMessage.EnsureSuccessStatusCode();
        }

        private string CleanUrl(string url)
        {
            return url.TrimStart('/');
        }

        private async Task<T> GetDeserializedResponseAsync<T>(HttpContent content)
        {
            using (var stream = await content.ReadAsStreamAsync())
            {
                return DeserializeXmlStream<T>(stream);
            }
        }

        private static T DeserializeXmlStream<T>(Stream xmlStream)
        {
            var serializer = new XmlSerializer(typeof(T));
            return (T)serializer.Deserialize(xmlStream);
        }

        private static string SerializeXml<T>(T value)
        {
            if (value == null)
            {
                return string.Empty;
            }

            var settings = new XmlWriterSettings
            {
                OmitXmlDeclaration = true
            };

            var xmlSerializer = new XmlSerializer(typeof(T));
            var stringWriter = new StringWriter();
            using (var writer = XmlWriter.Create(stringWriter, settings))
            {
                var emptyNamespaces = new XmlSerializerNamespaces(new[]
                {
                    XmlQualifiedName.Empty
                });
                xmlSerializer.Serialize(writer, value, emptyNamespaces);
                return stringWriter.ToString();
            }
        }
    }
}
