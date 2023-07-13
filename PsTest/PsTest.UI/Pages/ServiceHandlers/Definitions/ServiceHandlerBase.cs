﻿using PsTest.UI.Helpers;
using System.Net;
using System.Net.Http.Json;
using System.Text.Json;

namespace PsTest.UI.Pages.ServiceHandlers.Definitions
{
    public abstract class ServiceHandlerBase
    {
        private readonly IConfiguration configuration;
        private readonly HttpClient http;

        public ServiceHandlerBase(IConfiguration configuration, HttpClient http)
        {
            this.configuration = configuration;
            this.http = http;
        }

        protected string GetServiceUri(string uriKey)
        {
            var uri = configuration[uriKey];
            return uri;
        }
        protected string GetServiceUri(string uriKey, Dictionary<string, string> queryStringsToBeApended)
        {
            var uri = configuration[uriKey];

            if (queryStringsToBeApended != null && queryStringsToBeApended.Count > 0)
            {
                uri = $"{uri}?";
                int i = 0;
                foreach (var queryString in queryStringsToBeApended)
                {
                    i++;
                    var encodedVal = WebUtility.UrlEncode(queryString.Value);
                    if (i <= 1)
                        uri = $"{uri}{queryString.Key}={encodedVal}";
                    else
                        uri = $"{uri}&{queryString.Key}={encodedVal}";
                }
            }
            return uri;
        }
        protected async Task<T> ReadApiResponseAsync<T>(HttpResponseMessage httpResponse)
        {
            if (httpResponse.IsSuccessStatusCode && httpResponse.Content != null)
            {
                return await httpResponse.Content.ReadFromJsonAsync<T>();
            }
            return default(T);
        }
        protected async Task<string> ReadApiStringResponseAsync(HttpResponseMessage httpResponse)
        {
            string res = "";
            if (httpResponse.IsSuccessStatusCode && httpResponse.Content != null)
            {
                res = await httpResponse.Content.ReadAsStringAsync();
            }
            return res;
        }
        protected async Task<TResult> Get<TResult>(string uriConfigKey)
        {
            var uri = GetServiceUri(uriConfigKey);
            var response = await http.GetAsync(uri);
            TResult vm = await ReadApiResponseAsync<TResult>(response);
            return vm;
        }
        protected async Task<string> GetString(string uriConfigKey)
        {
            var uri = GetServiceUri(uriConfigKey);
            var response = await http.GetStringAsync(uri);
            return response;
        }
        protected async Task<string> PostAndGetString<TInput>(TInput vm, string uriConfigKey)
        {
            if (vm == null)
                throw new ArgumentNullException(ErrorMessages.VIEW_MODEL_IS_NULL);

            var uri = GetServiceUri(uriConfigKey);

            var response = await http.PostAsJsonAsync(uri, vm);
            string result = await ReadApiStringResponseAsync(response);
            return result;
        }
        protected async Task<string> PostFile(MultipartFormDataContent fileContent, string uriConfigKey)
        {
            if (fileContent == null)
                throw new ArgumentNullException(ErrorMessages.VIEW_MODEL_IS_NULL);

            var uri = GetServiceUri(uriConfigKey);

            var response = await http.PostAsync(uri, fileContent);
            string result = await ReadApiStringResponseAsync(response);
            return result;
        }
        protected async Task<string> PostFile(MultipartFormDataContent fileContent, string uriConfigKey, Dictionary<string, string> queryStringsToBeApended)
        {
            if (fileContent == null)
                throw new ArgumentNullException(ErrorMessages.VIEW_MODEL_IS_NULL);

            var uri = GetServiceUri(uriConfigKey, queryStringsToBeApended);

            var response = await http.PostAsync(uri, fileContent);
            string result = await ReadApiStringResponseAsync(response);
            return result;
        }
        protected async Task<TReturn> PostFile<TReturn>(MultipartFormDataContent fileContent, string uriConfigKey)
        {
            if (fileContent == null)
                throw new ArgumentNullException(ErrorMessages.VIEW_MODEL_IS_NULL);

            var uri = GetServiceUri(uriConfigKey);

            var response = await http.PostAsync(uri, fileContent);
            TReturn result = await ReadApiResponseAsync<TReturn>(response);
            return result;
        }
        protected async Task<TResult> Post<TInput, TResult>(TInput vm, string uriConfigKey)
        {
            if (vm == null)
                throw new ArgumentNullException(ErrorMessages.VIEW_MODEL_IS_NULL);

            var uri = GetServiceUri(uriConfigKey);

            var response = await http.PostAsJsonAsync(uri, vm);
            TResult result = await ReadApiResponseAsync<TResult>(response);
            return result;
        }
        protected async Task<TResult> Post<TResult>(string uriConfigKey)
        {
            var uri = GetServiceUri(uriConfigKey);
            var response = await http.PostAsync(uri, null);
            TResult result = await ReadApiResponseAsync<TResult>(response);
            return result;
        }
        protected async Task<TResult> Post<TResult>(string stringContent, string uriConfigKey)
        {
            var uri = GetServiceUri(uriConfigKey);
            var requestMsg = new HttpRequestMessage(HttpMethod.Post, uri);
            var content = JsonSerializer.Serialize(stringContent);
            requestMsg.Content = new StringContent(content);
            requestMsg.Content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");
            var response = await http.SendAsync(requestMsg);
            TResult result = await ReadApiResponseAsync<TResult>(response);
            return result;
        }
    }
}
