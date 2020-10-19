
using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Http;
using System.Runtime.InteropServices.ComTypes;
using System.Threading.Tasks;

using BaSyx.Models.Core.AssetAdministrationShell.Generics;
using BaSyx.Models.Core.Common;
using BaSyx.Utils.ResultHandling;

namespace AIMonitor.AAS.Client.Http
{
    /// <summary>
    /// HttpClient AssetAdministrationShellRepository
    /// </summary>
    public class TecalogAssetAdministrationShellRepositoryHttpClient : TecalogHttpClient
    {
        private const string SHELL = "shells";

        public TecalogAssetAdministrationShellRepositoryHttpClient(HttpClient httpClient) : base(httpClient)
        {
        }

        public async Task<IResult<IAssetAdministrationShell>> CreateAssetAdministrationShellAsync(IAssetAdministrationShell aas)
        {
            var request = CreateJsonContentRequest(GetUri(SHELL), HttpMethod.Post, aas);
            var response = await SendRequestAsync(request);
            return await EvaluateResponseAsync<IAssetAdministrationShell>(response, response.Entity);
        }

        public async Task<IResult> DeleteAssetAdministrationShellAsync(string aasId)
        {
            var request = CreateRequest(GetUri(SHELL, aasId), HttpMethod.Delete);
            var response = await SendRequestAsync(request);
            return await EvaluateResponseAsync(response, response.Entity);
        }

        public async Task<IResult<IAssetAdministrationShell>> RetrieveAssetAdministrationShellAsync(string aasId)
        {
            var request = CreateRequest(GetUri(SHELL, aasId), HttpMethod.Get);
            var response = await SendRequestAsync(request);
            return await EvaluateResponseAsync<IAssetAdministrationShell>(response, response.Entity);
        }

        public async Task<IResult<IElementContainer<IAssetAdministrationShell>>> RetrieveAssetAdministrationShellsAsync()
        {
            var request = CreateRequest(GetUri(SHELL), HttpMethod.Get);
            var response = await SendRequestAsync(request);
            return await EvaluateResponseAsync<ElementContainer<IAssetAdministrationShell>>(response, response.Entity);
        }

        public async Task<IResult> UpdateAssetAdministrationShell(string aasId, IAssetAdministrationShell aas)
        {
            var request = CreateJsonContentRequest(GetUri(SHELL, aasId), HttpMethod.Put, aas);
            var response = await SendRequestAsync(request);
            return await EvaluateResponseAsync(response, response.Entity);
        }

        public async Task<IResult<IEnumerable<string>>> UploadAssetAdministrationShell(IEnumerable<string> filePaths)
        {
            var request = CreateFileContentRequest(GetUri("uploads"), HttpMethod.Post, filePaths);
            var response = await SendRequestAsync(request);
            return await EvaluateResponseAsync<IEnumerable<string>>(response, response.Entity);
        }
    }
}
