using System;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;

namespace Orneholm.PEAccountingNet
{
    internal class PeaApiHttpClient : IPeaApiHttpClient
    {
        private const string AccessTokenHeaderName = "X-Token";
        private readonly Uri _baseUrl = new Uri("https://my.accounting.pe/api/v1/");
        private readonly HttpClient _httpClient;

        public PeaApiHttpClient() : this(string.Empty)
        {
        }

        public PeaApiHttpClient(string accessToken)
        {
            _httpClient = new HttpClient
            {
                BaseAddress = _baseUrl
            };

            if (!string.IsNullOrEmpty(accessToken))
            {
                _httpClient.DefaultRequestHeaders.Add(AccessTokenHeaderName, accessToken);
            }
        }

        public async Task<T> GetAsync<T>(string url)
        {
            var httpResponseMessage = await _httpClient.GetAsync(CleanUrl(url));
            EnsureSuccess(httpResponseMessage);
            return await GetDeserializedResponseAsync<T>(httpResponseMessage.Content);
        }

        public async Task<TResult> PostAsync<TRequest, TResult>(string url, TRequest request)
        {
            return await PostPutAsync<TRequest, TResult>(url, request,
                (cleanUrl, httpContent) => _httpClient.PostAsync(cleanUrl, httpContent)
            );
        }

        public async Task PostAsync<TRequest>(string url, TRequest request)
        {
            await PostPutAsync<TRequest>(url, request,
                (cleanUrl, httpContent) => _httpClient.PostAsync(cleanUrl, httpContent)
            );
        }

        public async Task<TResult> PutAsync<TRequest, TResult>(string url, TRequest request)
        {
            return await PostPutAsync<TRequest, TResult>(url, request,
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
            var requestXml = SerializeXml(request);
            var httpContent = new StringContent(requestXml);

            var httpResponseMessage = await httpRequest(CleanUrl(url), httpContent);
            EnsureSuccess(httpResponseMessage);
            return await GetDeserializedResponseAsync<TResult>(httpResponseMessage.Content);
        }

        public async Task PostPutAsync<TRequest>(string url, TRequest request, Func<string, HttpContent, Task<HttpResponseMessage>> httpRequest)
        {
            var requestXml = SerializeXml(request);
            var httpContent = new StringContent(requestXml);

            var httpResponseMessage = await httpRequest(CleanUrl(url), httpContent);
            EnsureSuccess(httpResponseMessage);
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

            var xmlserializer = new XmlSerializer(typeof(T));
            var stringWriter = new StringWriter();
            using (var writer = XmlWriter.Create(stringWriter, settings))
            {
                var emptyNamepsaces = new XmlSerializerNamespaces(new[]
                {
                    XmlQualifiedName.Empty
                });
                xmlserializer.Serialize(writer, value, emptyNamepsaces);
                return stringWriter.ToString();
            }
        }
    }
}