using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

using BaSyx.Utils.DependencyInjection;
using BaSyx.Utils.PathHandling;
using BaSyx.Utils.ResultHandling;

using Microsoft.Extensions.DependencyInjection;

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace MOS.AAS.Client.Http
{
    public class MosHttpClient
    {
        public System.Net.Http.HttpClient HttpClient { get; }
        public Uri Endpoint { get; protected set; }
        public JsonSerializerSettings JsonSerializerSettings { get; set; }

        public MosHttpClient(System.Net.Http.HttpClient httpClient)
        {
            this.HttpClient = httpClient;
            this.Endpoint = httpClient.BaseAddress;

            JsonSerializerSettings = new JsonSerializerSettings
            {
                ContractResolver = new DependencyInjectionContractResolver(new DependencyInjectionExtension(new ServiceCollection().AddStandardImplementation())),
                Formatting = Formatting.Indented,
                NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore
            };
            JsonSerializerSettings.Converters.Add(new Newtonsoft.Json.Converters.StringEnumConverter());
        }

        public Uri GetUri(params string[] pathElements)
        {
            if (pathElements == null)
            {
                return Endpoint;
            }

            return Endpoint.Append(pathElements);
        }

        protected HttpRequestMessage CreateRequest(Uri uri, HttpMethod method) => new HttpRequestMessage(method, uri);
        protected HttpRequestMessage CreateRequest(Uri uri, HttpMethod method, HttpContent content)
        {
            var message = CreateRequest(uri, method);
            if (content != null)
            {
                message.Content = content;
            }

            return message;
        }
        protected HttpRequestMessage CreateRequest(Uri uri, HttpMethod method, Func<HttpContent> content)
        {
            var message = CreateRequest(uri, method);
            if (content != null)
            {
                message.Content = content.Invoke();
            }

            return message;
        }

        protected HttpRequestMessage CreateFileContentRequest(Uri uri, HttpMethod method, IEnumerable<string> filePaths)
        {
            var message = CreateRequest(uri, method, () =>
            {
                var formData = new MultipartFormDataContent();
                foreach (var filePath in filePaths)
                {
                    var fileInfo = new System.IO.FileInfo(filePath);
                    var file = System.IO.File.OpenRead(fileInfo.FullName);
                    var content = new StreamContent(file);
                    formData.Add(content, "files", fileInfo.Name);
                }
                return formData;
            });
            return message;
        }

        protected HttpRequestMessage CreateJsonContentRequest(Uri uri, HttpMethod method, object content)
        {
            var message = CreateRequest(uri, method, () =>
            {
                var serialized = JsonConvert.SerializeObject(content, JsonSerializerSettings);
                return new StringContent(serialized, Encoding.UTF8, "application/json");
            });
            return message;
        }

        protected async Task<IResult<HttpResponseMessage>> SendRequestAsync(HttpRequestMessage message)
        {
            try
            {
                HttpResponseMessage response = await HttpClient.SendAsync(message);
                return new Result<HttpResponseMessage>(true, response);
            }
            catch (Exception e)
            {
                return new Result<HttpResponseMessage>(e);
            }
        }

        protected async Task<IResult<T>> EvaluateResponseAsync<T>(IResult result, HttpResponseMessage response)
        {
            var messageList = new List<IMessage>();
            messageList.AddRange(result.Messages);

            if (response != null)
            {
                try
                {
                    var responseString = await response.Content.ReadAsStringAsync();
                    if (response.IsSuccessStatusCode)
                    {
                        try
                        {
                            responseString = CheckAndExtractResultContruct(responseString);
                            var requestResult = JsonConvert.DeserializeObject<T>(responseString, JsonSerializerSettings);

                            messageList.Add(new Message(MessageType.Information, response.ReasonPhrase, ((int)response.StatusCode).ToString()));
                            return new Result<T>(true, requestResult, messageList);
                        }
                        catch (Exception e)
                        {
                            messageList.Add(new Message(MessageType.Error, e.Message, e.HelpLink));
                            return new Result<T>(false, messageList);
                        }
                    }
                    else
                    {
                        messageList.Add(new Message(MessageType.Error, response.ReasonPhrase + " | " + responseString, ((int)response.StatusCode).ToString()));
                        return new Result<T>(false, messageList);
                    }
                }
                catch (Exception ex)
                {
                    new Exception(ex.Message, ex);
                }

            }
            messageList.Add(new Message(MessageType.Error, "Evaluation of response failed - Response from host is null", null));
            return new Result<T>(false, messageList);
        }
        protected async Task<IResult> EvaluateResponseAsync(IResult result, HttpResponseMessage response)
        {
            var messageList = new List<IMessage>();
            messageList.AddRange(result.Messages);

            if (response != null)
            {
                var responseString = await response.Content.ReadAsStringAsync();
                if (response.IsSuccessStatusCode)
                {
                    messageList.Add(new Message(MessageType.Information, response.ReasonPhrase, ((int)response.StatusCode).ToString()));
                    return new Result(true, messageList);
                }
                else
                {
                    messageList.Add(new Message(MessageType.Error, response.ReasonPhrase + " | " + responseString, ((int)response.StatusCode).ToString()));
                    return new Result(false, messageList);
                }
            }
            messageList.Add(new Message(MessageType.Error, "Evaluation of response failed - Response from host is null", null));
            return new Result(false, messageList);
        }
        protected string CheckAndExtractResultContruct(string responseString)
        {
            if (responseString == null)
            {
                return null;
            }

            try
            {
                JToken jToken = JToken.Parse(responseString);
                var jEntity = jToken.SelectToken("entity");
                if (jEntity != null)
                {
                    return jEntity.ToString();
                }
                else
                {
                    return responseString;
                }
            }
            catch
            {
                return responseString;
            }
        }
    }
}
