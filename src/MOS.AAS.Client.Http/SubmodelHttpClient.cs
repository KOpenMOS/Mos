using System;
using System.Net.Http;
using System.Threading.Tasks;

using BaSyx.Models.Communication;
using BaSyx.Models.Core.AssetAdministrationShell.Generics;
using BaSyx.Models.Core.AssetAdministrationShell.Generics.SubmodelElementTypes;
using BaSyx.Models.Core.Common;
using BaSyx.Utils.ResultHandling;

namespace MOS.AAS.Client.Http
{
    /// <summary>
    /// HttpClient Submodel
    /// </summary>
    public class SubmodelHttpClient : MosHttpClient
    {
        private const string SUBMODEL = "submodel";
        private const string PROPERTIES = "properties";
        private const string OPERATIONS = "operations";
        private const string SUBMODELELEMENTS = "submodelElements";
        private const string EVENTS = "events";
        private const string VALUE = "value";

        private const string SEPERATOR = "/";
        private const int REQUEST_TIMEOUT = 30000;

        public SubmodelHttpClient(System.Net.Http.HttpClient httpClient) : base(httpClient)
        {
        }

        public void SetSubmodelIdShot(string aasId, string submodelId) => this.Endpoint = new Uri($"{this.HttpClient.BaseAddress}shells/{aasId}/aas/submodels/{submodelId}/{SUBMODEL}");

        public async Task<IResult<ISubmodel>> RetrieveSubmodel()
        {
            var request = base.CreateRequest(GetUri(), HttpMethod.Get);
            var response = await SendRequestAsync(request);
            return await EvaluateResponseAsync<ISubmodel>(response, response.Entity);
        }

        public async Task<IResult<IProperty>> CreateProperty(IProperty dataElement)
        {
            var request = base.CreateJsonContentRequest(GetUri(PROPERTIES), HttpMethod.Post, dataElement);
            var response = await SendRequestAsync(request);
            return await EvaluateResponseAsync<IProperty>(response, response.Entity);
        }

        public async Task<IResult<IEvent>> CreateEvent(IEvent eventable)
        {
            var request = base.CreateJsonContentRequest(GetUri(EVENTS), HttpMethod.Post, eventable);
            var response = await SendRequestAsync(request);
            return await EvaluateResponseAsync<IEvent>(response, response.Entity);
        }

        public async Task<IResult<IOperation>> CreateOperation(IOperation operation)
        {
            var request = base.CreateJsonContentRequest(GetUri(OPERATIONS), HttpMethod.Post, operation);
            var response = await SendRequestAsync(request);
            return await EvaluateResponseAsync<IOperation>(response, response.Entity);
        }

        public async Task<IResult> DeleteProperty(string propertyId)
        {
            var request = base.CreateRequest(GetUri(PROPERTIES, propertyId), HttpMethod.Delete);
            var response = await SendRequestAsync(request);
            return await base.EvaluateResponseAsync(response, response.Entity);
        }

        public async Task<IResult> DeleteEvent(string eventId)
        {
            var request = base.CreateRequest(GetUri(EVENTS, eventId), HttpMethod.Delete);
            var response = await SendRequestAsync(request);
            return await base.EvaluateResponseAsync(response, response.Entity);
        }

        public async Task<IResult> DeleteOperation(string operationId)
        {
            var request = base.CreateRequest(GetUri(OPERATIONS, operationId), HttpMethod.Delete);
            var response = await SendRequestAsync(request);
            return await base.EvaluateResponseAsync(response, response.Entity);
        }

        public async Task<IResult<InvocationResponse>> InvokeOperation(string operationId, InvocationRequest invocationRequest)
        {
            var request = base.CreateJsonContentRequest(GetUri(OPERATIONS, operationId), HttpMethod.Post, invocationRequest);
            var response = await SendRequestAsync(request);
            return await EvaluateResponseAsync<InvocationResponse>(response, response.Entity);
        }

        public async Task<IResult<IProperty>> RetrieveProperty(string propertyId)
        {
            var request = base.CreateRequest(GetUri(PROPERTIES, propertyId), HttpMethod.Get);
            var response = await SendRequestAsync(request);
            return await EvaluateResponseAsync<IProperty>(response, response.Entity);
        }

        public async Task<IResult<IElementContainer<IProperty>>> RetrieveProperties()
        {
            var request = base.CreateRequest(GetUri(PROPERTIES), HttpMethod.Get);
            var response = await SendRequestAsync(request);
            return await EvaluateResponseAsync<ElementContainer<IProperty>>(response, response.Entity);
        }

        public async Task<IResult<IValue>> RetrievePropertyValue(string propertyId)
        {
            var request = base.CreateRequest(GetUri(PROPERTIES, propertyId, VALUE), HttpMethod.Get);
            var response = await SendRequestAsync(request);
            return await EvaluateResponseAsync<IValue>(response, response.Entity);
        }

        public async Task<IResult<IEvent>> RetrieveEvent(string eventId)
        {
            var request = base.CreateRequest(GetUri(EVENTS, eventId), HttpMethod.Get);
            var response = await SendRequestAsync(request);
            return await EvaluateResponseAsync<IEvent>(response, response.Entity);
        }

        public async Task<IResult<IElementContainer<IEvent>>> RetrieveEvents()
        {
            var request = base.CreateRequest(GetUri(EVENTS), HttpMethod.Get);
            var response = await SendRequestAsync(request);
            return await EvaluateResponseAsync<IElementContainer<IEvent>>(response, response.Entity);
        }

        public async Task<IResult<IOperation>> RetrieveOperation(string operationId)
        {
            var request = base.CreateRequest(GetUri(OPERATIONS, operationId), HttpMethod.Get);
            var response = await SendRequestAsync(request);
            return await EvaluateResponseAsync<IOperation>(response, response.Entity);
        }

        public async Task<IResult<IElementContainer<IOperation>>> RetrieveOperations()
        {
            var request = base.CreateRequest(GetUri(OPERATIONS), HttpMethod.Get);
            var response = await SendRequestAsync(request);
            return await EvaluateResponseAsync<ElementContainer<IOperation>>(response, response.Entity);
        }

        public async Task<IResult> UpdatePropertyValue(string propertyId, IValue value)
        {
            var request = base.CreateJsonContentRequest(GetUri(PROPERTIES, propertyId, VALUE), HttpMethod.Put, value);
            var response = await SendRequestAsync(request);
            return await base.EvaluateResponseAsync(response, response.Entity);
        }

        public async Task<IResult<ISubmodelElement>> CreateSubmodelElement(ISubmodelElement submodelElement)
        {
            var request = base.CreateJsonContentRequest(GetUri(SUBMODELELEMENTS), HttpMethod.Post, submodelElement);
            var response = await SendRequestAsync(request);
            return await EvaluateResponseAsync<ISubmodelElement>(response, response.Entity);
        }

        public async Task<IResult<IElementContainer<ISubmodelElement>>> RetrieveSubmodelElements()
        {
            var request = base.CreateRequest(GetUri(SUBMODELELEMENTS), HttpMethod.Get);
            var response = await SendRequestAsync(request);
            return await EvaluateResponseAsync<IElementContainer<ISubmodelElement>>(response, response.Entity);
        }

        public async Task<IResult<ISubmodelElement>> RetrieveSubmodelElement(string submodelElementId)
        {
            var request = base.CreateRequest(GetUri(SUBMODELELEMENTS, submodelElementId), HttpMethod.Get);
            var response = await SendRequestAsync(request);
            return await EvaluateResponseAsync<ISubmodelElement>(response, response.Entity);
        }

        public async Task<IResult<IValue>> RetrieveSubmodelElementValue(string submodelElementId)
        {
            var request = base.CreateRequest(GetUri(SUBMODELELEMENTS, submodelElementId, VALUE), HttpMethod.Get);
            var response = await SendRequestAsync(request);
            return await EvaluateResponseAsync<IValue>(response, response.Entity);
        }

        public async Task<IResult> UpdateSubmodelElement(string submodelElementId, ISubmodelElement submodelElement)
        {
            var request = base.CreateJsonContentRequest(GetUri(SUBMODELELEMENTS, submodelElementId), HttpMethod.Put, submodelElement);
            var response = await SendRequestAsync(request);
            return await base.EvaluateResponseAsync(response, response.Entity);
        }

        public async Task<IResult> DeleteSubmodelElement(string submodelElementId)
        {
            var request = base.CreateRequest(GetUri(SUBMODELELEMENTS, submodelElementId), HttpMethod.Delete);
            var response = await SendRequestAsync(request);
            return await base.EvaluateResponseAsync(response, response.Entity);
        }

        public async Task<IResult<CallbackResponse>> InvokeOperationAsync(string operationId, InvocationRequest invocationRequest)
        {
            var request = base.CreateJsonContentRequest(GetUri(OPERATIONS, operationId, "async"), HttpMethod.Post, invocationRequest);
            var response = await SendRequestAsync(request);
            return await EvaluateResponseAsync<CallbackResponse>(response, response.Entity);
        }

        public async Task<IResult<InvocationResponse>> GetInvocationResult(string operationId, string requestId)
        {
            var request = base.CreateRequest(GetUri(OPERATIONS, operationId, "invocationList", requestId), HttpMethod.Get);
            var response = await SendRequestAsync(request);
            return await EvaluateResponseAsync<InvocationResponse>(response, response.Entity);
        }
    }
}
