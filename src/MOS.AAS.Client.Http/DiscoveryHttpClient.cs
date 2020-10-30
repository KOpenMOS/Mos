using System.Net.Http;
using System.Threading.Tasks;

using BaSyx.Models.Core.AssetAdministrationShell.Enums;
using BaSyx.Models.Core.AssetAdministrationShell.Generics;
using BaSyx.Models.Core.AssetAdministrationShell.References;
using BaSyx.Models.Core.Common;
using BaSyx.Utils.ResultHandling;

namespace MOS.AAS.Client.Http
{
    /// <summary>
    /// HttpClient For Search
    /// </summary>
    public class DiscoveryHttpClient : MosHttpClient
    {
        private const string SHELL = "shells";
        private const string AAS = "aas";
        private const string DISCOVERY = "discovery";

        public DiscoveryHttpClient(System.Net.Http.HttpClient httpClient) : base(httpClient)
        {
        }

        /// <summary>
        /// 전체 AAS
        /// </summary>
        /// <returns></returns>
        public async Task<IResult<IElementContainer<IAssetAdministrationShell>>> RetrieveAssetAdministrationShellsAsync()
        {
            var request = this.CreateRequest(GetUri(DISCOVERY, "RetrieveAssetAdministrationShells"), HttpMethod.Get);
            var response = await this.SendRequestAsync(request);
            return await this.EvaluateResponseAsync<ElementContainer<IAssetAdministrationShell>>(response, response.Entity);
        }

        /// <summary>
        /// AAS 조회(by aasId)
        /// </summary>
        /// <param name="aasId"></param>
        /// <returns></returns>
        public async Task<IResult<IAssetAdministrationShell>> RetrieveAssetAdministrationShellAsync(string aasId)
        {
            var request = this.CreateRequest(GetUri(DISCOVERY, "RetrieveAssetAdministrationShellByIdShort", aasId.ToString()), HttpMethod.Get);
            var response = await SendRequestAsync(request);
            return await EvaluateResponseAsync<IAssetAdministrationShell>(response, response.Entity);
        }

        /// <summary>
        /// AAS 조회(by assetKind)
        /// </summary>
        /// <param name="assetKind"></param>
        /// <returns></returns>
        public async Task<IResult<IElementContainer<IAssetAdministrationShell>>> GetAssetAdministrationShellsByAssetKindAsync(AssetKind assetKind)
        {
            var request = this.CreateRequest(GetUri(DISCOVERY, "GetAssetAdministrationShellsByAssetKind", assetKind.ToString()), HttpMethod.Get);
            var response = await SendRequestAsync(request);
            return await EvaluateResponseAsync<ElementContainer<IAssetAdministrationShell>>(response, response.Entity);
        }

        /// <summary>
        /// AAS 조회(by Category)
        /// </summary>
        /// <param name="assetKind"></param>
        /// <returns></returns>
        public async Task<IResult<IElementContainer<IAssetAdministrationShell>>> GetAssetAdministrationShellsByCategoryAsync(AssetKind assetKind, string category)
        {
            var request = this.CreateRequest(GetUri(DISCOVERY, "GetAssetAdministrationShellsByCategory", assetKind.ToString(), category), HttpMethod.Get);
            var response = await SendRequestAsync(request);
            return await EvaluateResponseAsync<ElementContainer<IAssetAdministrationShell>>(response, response.Entity);
        }

        /// <summary>
        /// AAS 조회(by IsMonitoring)
        /// </summary>
        /// <param name="assetKind"></param>
        /// <returns></returns>
        public async Task<IResult<IElementContainer<IAssetAdministrationShell>>> GetAssetAdministrationShellsByMonitorAsync(AssetKind assetKind, bool isMonitoring)
        {
            var request = this.CreateRequest(GetUri(DISCOVERY, "GetAssetAdministrationShellsByMonitor", assetKind.ToString(), isMonitoring.ToString()), HttpMethod.Get);
            var response = await SendRequestAsync(request);
            return await EvaluateResponseAsync<ElementContainer<IAssetAdministrationShell>>(response, response.Entity);
        }

        /// <summary>
        /// AAS 조회(by SubmodelId)
        /// </summary>
        /// <param name="submodelId"></param>
        /// <returns></returns>
        public async Task<IResult<IElementContainer<IAssetAdministrationShell>>> GetAssetAdministrationShellsBySubmodelIdAsync(string submodelId)
        {
            var request = this.CreateRequest(GetUri(DISCOVERY, "GetAssetAdministrationShellsBySubmodelId", submodelId), HttpMethod.Get);
            var response = await SendRequestAsync(request);
            return await EvaluateResponseAsync<ElementContainer<IAssetAdministrationShell>>(response, response.Entity);
        }

        /// <summary>
        /// AAS(by aasId) 내 Submodel 조회 
        /// </summary>
        /// <param name="aasId"></param>
        /// <returns></returns>
        public async Task<IResult<IElementContainer<ISubmodel>>> GetSubmodelsInAssetAdministrationShellAsync(string aasId)
        {
            var request = this.CreateRequest(GetUri(DISCOVERY, "GetSubmodelsInAssetAdministrationShell", aasId), HttpMethod.Get);
            var response = await SendRequestAsync(request);
            return await EvaluateResponseAsync<ElementContainer<ISubmodel>>(response, response.Entity);
        }

        /// <summary>
        /// AAS(by aasId) 내 Submodel 조회(by category)
        /// </summary>
        /// <param name="aasId"></param>
        /// <param name="category"></param>
        /// <returns></returns>
        public async Task<IResult<IElementContainer<ISubmodel>>> GetSubmodelsInAssetAdministrationShellByCategoryAsync(string aasId, string category)
        {
            var request = this.CreateRequest(GetUri(DISCOVERY, "GetSubmodelsInAssetAdministrationShellByCategory", aasId, category), HttpMethod.Get);
            var response = await SendRequestAsync(request);
            return await EvaluateResponseAsync<ElementContainer<ISubmodel>>(response, response.Entity);
        }

        /// <summary>
        /// Submodel 조회(by Key)
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public async Task<IResult<IAssetAdministrationShell>> GetSubmodelsInAssetAdministrationShellByKeyAsync(IKey key)
        {
            var request = this.CreateJsonContentRequest(GetUri(DISCOVERY, "GetSubmodelsInAssetAdministrationShellByKey"), HttpMethod.Post, key);
            var response = await SendRequestAsync(request);
            return await EvaluateResponseAsync<IAssetAdministrationShell>(response, response.Entity);
        }

        /// <summary>
        /// AAS 조회(by Key)
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public async Task<IResult<IElementContainer<IAssetAdministrationShell>>> GetInheritAssetAdministrationShellsByKeyAsync(IKey key)
        {
            var request = this.CreateJsonContentRequest(GetUri(DISCOVERY, "GetInheritAssetAdministrationShellsByKey"), HttpMethod.Post, key);
            var response = await SendRequestAsync(request);
            return await EvaluateResponseAsync<ElementContainer<IAssetAdministrationShell>>(response, response.Entity);
        }

        /// <summary>
        /// AAS 조회(by Type AssetAdministrationShell IdShort)
        /// </summary>
        /// <param name="aasId"></param>
        /// <returns></returns>
        public async Task<IResult<IElementContainer<IAssetAdministrationShell>>> GetDescendantAssetAdministrationShellsAsync(string aasId)
        {
            var request = this.CreateRequest(GetUri(DISCOVERY, "GetDescendantAssetAdministrationShellsByIdShort", aasId), HttpMethod.Get);
            var response = await SendRequestAsync(request);
            return await EvaluateResponseAsync<ElementContainer<IAssetAdministrationShell>>(response, response.Entity);
        }

        /// <summary>
        /// Submodel 조회(특정 Aas 내 Key 이용 조회)
        /// </summary>
        /// <param name="aasId"></param>
        /// <returns></returns>
        public async Task<IResult<ISubmodel>> GetSubmodelBySubmodelKeyAsync(string aasId, IKey key)
        {
            var request = this.CreateJsonContentRequest(GetUri(DISCOVERY, "GetSubmodelBySubmodelKey", aasId), HttpMethod.Post, key);
            var response = await SendRequestAsync(request);
            return await EvaluateResponseAsync<ISubmodel>(response, response.Entity);
        }
    }
}
