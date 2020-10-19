using System;
using System.Net.Http;
using System.Threading.Tasks;


using BaSyx.Models.Communication;
using BaSyx.Models.Connectivity.Descriptors;
using BaSyx.Models.Core.AssetAdministrationShell.Generics;
using BaSyx.Models.Core.AssetAdministrationShell.Generics.SubmodelElementTypes;
using BaSyx.Models.Core.Common;
using BaSyx.Utils.ResultHandling;

using NLog;

namespace AIMonitor.AAS.Client.Http
{
    /// <summary>
    /// HttpClient AssetAdministraionShell
    /// </summary>
    public class TecalogAssetAdministrationShellHttpClient : TecalogHttpClient
    {
        private static readonly ILogger logger = LogManager.GetCurrentClassLogger();
        private const string AAS = "aas";
        private const string SUBMODELS = "submodels";
        private const string SUBMODEL = "submodel";
        private const string PROPERTIES = "properties";
        private const string OPERATIONS = "operations";
        private const string EVENTS = "events";
        private const string VALUE = "value";

        private const string BINDING = "binding";
        private const string BINDINGS = "bindings";

        private const string SEPERATOR = "/";
        private const int REQUEST_TIMEOUT = 30000;

        public TecalogAssetAdministrationShellHttpClient(HttpClient httpClient) : base(httpClient)
        {
        }

        public void SetAasIdShot(string aasid) => this.Endpoint = new Uri($"{this.HttpClient.BaseAddress}shells/{aasid}/aas");


        #region Public Methods

        public async Task<IResult<IAssetAdministrationShellDescriptor>> RetrieveAssetAdministrationShellDescriptorAsync()
        {
            var request = CreateRequest(GetUri(), HttpMethod.Get);
            var response = await SendRequestAsync(request);
            return await EvaluateResponseAsync<IAssetAdministrationShellDescriptor>(response, response.Entity);
        }

        public async Task<IResult<IAssetAdministrationShell>> RetrieveAssetAdministrationShellAsync()
        {
            var request = CreateRequest(GetUri(), HttpMethod.Get);
            var response = await SendRequestAsync(request);
            return await EvaluateResponseAsync<IAssetAdministrationShell>(response, response.Entity);
        }

        public async Task<IResult<ISubmodel>> CreateSubmodelAsync(ISubmodel submodel)
        {
            var request = CreateJsonContentRequest(GetUri(SUBMODELS), HttpMethod.Post, submodel);
            var response = await SendRequestAsync(request);
            return await EvaluateResponseAsync<ISubmodel>(response, response.Entity);
        }

        public async Task<IResult<IElementContainer<ISubmodel>>> RetrieveSubmodelsAsync()
        {
            var request = CreateRequest(GetUri(SUBMODELS), HttpMethod.Get);
            var response = await SendRequestAsync(request);
            return await EvaluateResponseAsync<ElementContainer<ISubmodel>>(response, response.Entity);
        }

        public async Task<IResult<ISubmodel>> RetrieveSubmodelAsync(string submodelId)
        {
            var request = CreateRequest(GetUri(SUBMODELS, submodelId), HttpMethod.Get);
            var response = await SendRequestAsync(request);
            return await EvaluateResponseAsync<ISubmodel>(response, response.Entity);
        }

        public async Task<IResult> DeleteSubmodelAsync(string submodelId)
        {
            var request = CreateRequest(GetUri(SUBMODELS, submodelId), HttpMethod.Delete);
            var response = await SendRequestAsync(request);
            return await EvaluateResponseAsync(response, response.Entity);
        }

        public async Task<IResult<IOperation>> CreateOperationAsync(string submodelId, IOperation operation)
        {
            var request = CreateJsonContentRequest(GetUri(SUBMODELS, submodelId, SUBMODEL, OPERATIONS), HttpMethod.Post, operation);
            var response = await SendRequestAsync(request);
            return await EvaluateResponseAsync<IOperation>(response, response.Entity);
        }

        public async Task<IResult<IElementContainer<IOperation>>> RetrieveOperationsAsync(string submodelId)
        {
            var request = CreateRequest(GetUri(SUBMODELS, submodelId, SUBMODEL, OPERATIONS), HttpMethod.Get);
            var response = await SendRequestAsync(request);
            return await EvaluateResponseAsync<ElementContainer<IOperation>>(response, response.Entity);
        }

        public async Task<IResult<IOperation>> RetrieveOperationAsync(string submodelId, string operationId)
        {
            var request = CreateRequest(GetUri(SUBMODELS, submodelId, SUBMODEL, OPERATIONS, operationId), HttpMethod.Get);
            var response = await SendRequestAsync(request);
            return await EvaluateResponseAsync<IOperation>(response, response.Entity);
        }

        public async Task<IResult> DeleteOperationAsync(string submodelId, string operationId)
        {
            var request = CreateRequest(GetUri(SUBMODELS, submodelId, SUBMODEL, OPERATIONS, operationId), HttpMethod.Delete);
            var response = await SendRequestAsync(request);
            return await EvaluateResponseAsync(response, response.Entity);
        }

        public async Task<IResult<InvocationResponse>> InvokeOperationAsync(string submodelId, string operationId, InvocationRequest invocationRequest)
        {
            var request = CreateJsonContentRequest(GetUri(SUBMODELS, submodelId, SUBMODEL, OPERATIONS, operationId), HttpMethod.Post, invocationRequest);
            var response = await SendRequestAsync(request);
            return await EvaluateResponseAsync<InvocationResponse>(response, response.Entity);
        }

        public async Task<IResult<IProperty>> CreatePropertyAsync(string submodelId, IProperty property)
        {
            var request = CreateJsonContentRequest(GetUri(SUBMODELS, submodelId, SUBMODEL, PROPERTIES), HttpMethod.Post, property);
            var response = await SendRequestAsync(request);
            return await EvaluateResponseAsync<IProperty>(response, response.Entity);
        }

        public async Task<IResult<IElementContainer<IProperty>>> RetrievePropertiesAsync(string submodelId)
        {
            var request = CreateRequest(GetUri(SUBMODELS, submodelId, SUBMODEL, PROPERTIES), HttpMethod.Get);
            var response = await SendRequestAsync(request);
            return await EvaluateResponseAsync<ElementContainer<IProperty>>(response, response.Entity);
        }

        public async Task<IResult<IProperty>> RetrievePropertyAsync(string submodelId, string propertyId)
        {
            var request = CreateRequest(GetUri(SUBMODELS, submodelId, SUBMODEL, PROPERTIES, propertyId), HttpMethod.Get);
            var response = await SendRequestAsync(request);
            return await EvaluateResponseAsync<IProperty>(response, response.Entity);
        }

        public async Task<IResult> UpdatePropertyValueAsync(string submodelId, string propertyId, IValue value)
        {
            var request = CreateJsonContentRequest(GetUri(SUBMODELS, submodelId, SUBMODEL, PROPERTIES, propertyId, VALUE), HttpMethod.Put, value);
            var response = await SendRequestAsync(request);
            return await EvaluateResponseAsync(response, response.Entity);
        }

        public async Task<IResult<IValue>> RetrievePropertyValueAsync(string submodelId, string propertyId)
        {
            var request = CreateRequest(GetUri(SUBMODELS, submodelId, SUBMODEL, PROPERTIES, propertyId, VALUE), HttpMethod.Get);
            var response = await SendRequestAsync(request);
            return await EvaluateResponseAsync<IValue>(response, response.Entity);
        }

        public async Task<IResult> DeletePropertyAsync(string submodelId, string propertyId)
        {
            var request = CreateRequest(GetUri(SUBMODELS, submodelId, SUBMODEL, PROPERTIES, propertyId), HttpMethod.Delete);
            var response = await SendRequestAsync(request);
            return await EvaluateResponseAsync(response, response.Entity);
        }

        public async Task<IResult<IEvent>> CreateEventAsync(string submodelId, IEvent eventable)
        {
            var request = CreateJsonContentRequest(GetUri(SUBMODELS, submodelId, SUBMODEL, EVENTS), HttpMethod.Post, eventable);
            var response = await SendRequestAsync(request);
            return await EvaluateResponseAsync<IEvent>(response, response.Entity);
        }

        public async Task<IResult<IElementContainer<IEvent>>> RetrieveEventsAsync(string submodelId)
        {
            var request = CreateRequest(GetUri(SUBMODELS, submodelId, SUBMODEL, EVENTS), HttpMethod.Get);
            var response = await SendRequestAsync(request);
            return await EvaluateResponseAsync<ElementContainer<IEvent>>(response, response.Entity);
        }

        public async Task<IResult<IEvent>> RetrieveEventAsync(string submodelId, string eventId)
        {
            var request = CreateRequest(GetUri(SUBMODELS, submodelId, SUBMODEL, EVENTS, eventId), HttpMethod.Get);
            var response = await SendRequestAsync(request);
            return await EvaluateResponseAsync<IEvent>(response, response.Entity);
        }

        public async Task<IResult> DeleteEventAsync(string submodelId, string eventId)
        {
            var request = CreateRequest(GetUri(SUBMODELS, submodelId, SUBMODEL, EVENTS, eventId), HttpMethod.Delete);
            var response = await SendRequestAsync(request);
            return await EvaluateResponseAsync(response, response.Entity);
        }

        public async Task<IResult<InvocationResponse>> GetInvocationResultAsync(string submodelId, string operationId, string requestId)
        {
            var request = CreateRequest(GetUri(SUBMODELS, submodelId, SUBMODEL, OPERATIONS, operationId, "invocationList", requestId), HttpMethod.Get);
            var response = await SendRequestAsync(request);
            return await EvaluateResponseAsync<InvocationResponse>(response, response.Entity);
        }

        #endregion
    }
}
