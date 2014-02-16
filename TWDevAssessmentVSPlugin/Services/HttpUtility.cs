using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;

namespace TWDevAssessmentVSPlugin.Services
{
    public class HttpUtility
    {
        private readonly Dictionary<RequestType, string> urlDictionary;
        public HttpUtility()
        {
            const string serverUrl = "http://54.84.178.223/";
            const string createUserUrl = serverUrl + "candidates/{0}";
            const string codeFreezeUrl = serverUrl + "candidates/{0}/freeze";
            const string filePostUrl = serverUrl + "candidates/{0}/code";
            const string questionsUrl = serverUrl + "candidates/{0}/start";
            urlDictionary = new Dictionary<RequestType, string>
                                {
                                    { RequestType.CreateUser, createUserUrl}, 
                                    { RequestType.FreezeCode, codeFreezeUrl},
                                    { RequestType.FilePost, filePostUrl},
                                    { RequestType.GetSelectedQuestion, questionsUrl}
                                };
        }

        public HttpResponseMessage GetJson(RequestType requestType, string id = null)
        {
            var url = string.Format(urlDictionary[requestType], id);
            using (var httpClient = new HttpClient())
            {
                var httpResponseMessage = httpClient.GetAsync(url).Result;
                var result = httpResponseMessage.Content.ReadAsStringAsync();
                httpResponseMessage.EnsureSuccessStatusCode();
                return httpResponseMessage;
            }
        }

        public HttpResponseMessage PostJson(string jsonContent, string id, RequestType requestType)
        {
            var stringContent = new StringContent(jsonContent);
            stringContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            var url = string.Format(urlDictionary[requestType], id);
            using (var httpClient = new HttpClient())
            {
                var httpResponseMessage = httpClient.PostAsync(url, stringContent).Result;
                return httpResponseMessage;
            }
        }

        public HttpResponseMessage PostFile(string zipFileName, string id, RequestType requestType)
        {
            var solutionInBytes = File.ReadAllBytes(zipFileName);
            var zipFileContent = new ByteArrayContent(solutionInBytes);
            var formContent = new MultipartFormDataContent { { zipFileContent, "\"file\"", "\"solution.zip\"" } };
            var url = string.Format(urlDictionary[requestType], id);
            HttpResponseMessage httpResponseMessage;
            using (var httpClient = new HttpClient())
            {
                httpResponseMessage = httpClient.PostAsync(url, formContent).Result;
            }
            return httpResponseMessage;
        }
    }

    public enum RequestType
    {
        CreateUser,
        FreezeCode,
        FilePost,
        GetSelectedQuestion
    }
}