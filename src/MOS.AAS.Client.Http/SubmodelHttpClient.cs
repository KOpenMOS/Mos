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
        public void SetSubmodelIdShot(string aasId, string submodelId, DateTime changedTime) => this.Endpoint = new Uri($"{this.HttpClient.BaseAddress}time{changedTime.ToString("yyyyMMddHHmmssfff")}/{aasId}/aas/submodels/{submodelId}/{SUBMODEL}");

        public async Task<IResult<ISubmodel>> RetrieveSubmodelAsync()
        {
            var request = base.CreateRequest(GetUri(), HttpMethod.Get);
            var response = await SendRequestAsync(request);
            return await EvaluateResponseAsync<ISubmodel>(response, response.Entity);
        }

        public async Task<IResult<IProperty>> CreatePropertyAsync(IProperty dataElement)
        {
            var request = base.CreateJsonContentRequest(GetUri(PROPERTIES), HttpMethod.Post, dataElement);
            var response = await SendRequestAsync(request);
            return await EvaluateResponseAsync<IProperty>(response, response.Entity);
        }

        public async Task<IResult<IEvent>> CreateEventAsync(IEvent eventable)
        {
            var request = base.CreateJsonContentRequest(GetUri(EVENTS), HttpMethod.Post, eventable);
            var response = await SendRequestAsync(request);
            return await EvaluateResponseAsync<IEvent>(response, response.Entity);
        }

        public async Task<IResult<IOperation>> CreateOperationAsync(IOperation operation)
        {
            var request = base.CreateJsonContentRequest(GetUri(OPERATIONS), HttpMethod.Post, operation);
            var response = await SendRequestAsync(request);
            return await EvaluateResponseAsync<IOperation>(response, response.Entity);
        }

        public async Task<IResult> DeletePropertyAsync(string propertyId)
        {
            var request = base.CreateRequest(GetUri(PROPERTIES, propertyId), HttpMethod.Delete);
            var response = await SendRequestAsync(request);
            return await base.EvaluateResponseAsync(response, response.Entity);
        }

        public async Task<IResult> DeleteEventAsync(string eventId)
        {
            var request = base.CreateRequest(GetUri(EVENTS, eventId), HttpMethod.Delete);
            var response = await SendRequestAsync(request);
            return await base.EvaluateResponseAsync(response, response.Entity);
        }

        public async Task<IResult> DeleteOperationAsync(string operationId)
        {
            var request = base.CreateRequest(GetUri(OPERATIONS, operationId), HttpMethod.Delete);
            var response = await SendRequestAsync(request);
            return await base.EvaluateResponseAsync(response, response.Entity);
        }

        public async Task<IResult<InvocationResponse>> InvokeOperationAsync(string operationId, InvocationRequest invocationRequest)
        {
            var request = base.CreateJsonContentRequest(GetUri(OPERATIONS, operationId), HttpMethod.Post, invocationRequest);
            var response = await SendRequestAsync(request);
            return await EvaluateResponseAsync<InvocationResponse>(response, response.Entity);
        }

        public async Task<IResult<IProperty>> RetrievePropertyAsync(string propertyId)
        {
            var request = base.CreateRequest(GetUri(PROPERTIES, propertyId), HttpMethod.Get);
            var response = await SendRequestAsync(request);
            return await EvaluateResponseAsync<IProperty>(response, response.Entity);
        }

        public async Task<IResult<IElementContainer<IProperty>>> RetrievePropertiesAsync()
        {
            var request = base.CreateRequest(GetUri(PROPERTIES), HttpMethod.Get);
            var response = await SendRequestAsync(request);
            return await EvaluateResponseAsync<ElementContainer<IProperty>>(response, response.Entity);
        }

        public async Task<IResult<IValue>> RetrievePropertyValueAsync(string propertyId)
        {
            var request = base.CreateRequest(GetUri(PROPERTIES, propertyId, VALUE), HttpMethod.Get);
            var response = await SendRequestAsync(request);
            return await EvaluateResponseAsync<IValue>(response, response.Entity);
        }

        public async Task<IResult<IEvent>> RetrieveEventAsync(string eventId)
        {
            var request = base.CreateRequest(GetUri(EVENTS, eventId), HttpMethod.Get);
            var response = await SendRequestAsync(request);
            return await EvaluateResponseAsync<IEvent>(response, response.Entity);
        }

        public async Task<IResult<IElementContainer<IEvent>>> RetrieveEventsAsync()
        {
            var request = base.CreateRequest(GetUri(EVENTS), HttpMethod.Get);
            var response = await SendRequestAsync(request);
            return await EvaluateResponseAsync<IElementContainer<IEvent>>(response, response.Entity);
        }

        public async Task<IResult<IOperation>> RetrieveOperationAsync(string operationId)
        {
            var request = base.CreateRequest(GetUri(OPERATIONS, operationId), HttpMethod.Get);
            var response = await SendRequestAsync(request);
            return await EvaluateResponseAsync<IOperation>(response, response.Entity);
        }

        public async Task<IResult<IElementContainer<IOperation>>> RetrieveOperationsAsync()
        {
            var request = base.CreateRequest(GetUri(OPERATIONS), HttpMethod.Get);
            var response = await SendRequestAsync(request);
            return await EvaluateResponseAsync<ElementContainer<IOperation>>(response, response.Entity);
        }

        public async Task<IResult> UpdatePropertyValueAsync(string propertyId, IValue value)
        {
            var request = base.CreateJsonContentRequest(GetUri(PROPERTIES, propertyId, VALUE), HttpMethod.Put, value);
            var response = await SendRequestAsync(request);
            return await base.EvaluateResponseAsync(response, response.Entity);
        }

        public async Task<IResult<ISubmodelElement>> CreateSubmodelElementAsync(ISubmodelElement submodelElement)
        {
            var request = base.CreateJsonContentRequest(GetUri(SUBMODELELEMENTS), HttpMethod.Post, submodelElement);
            var response = await SendRequestAsync(request);
            return await EvaluateResponseAsync<ISubmodelElement>(response, response.Entity);
        }

        public async Task<IResult<IElementContainer<ISubmodelElement>>> RetrieveSubmodelElementsAsync()
        {
            var request = base.CreateRequest(GetUri(SUBMODELELEMENTS), HttpMethod.Get);
            var response = await SendRequestAsync(request);
            return await EvaluateResponseAsync<IElementContainer<ISubmodelElement>>(response, response.Entity);
        }

        public async Task<IResult<ISubmodelElement>> RetrieveSubmodelElementAsync(string submodelElementId)
        {
            var request = base.CreateRequest(GetUri(SUBMODELELEMENTS, submodelElementId), HttpMethod.Get);
            var response = await SendRequestAsync(request);
            return await EvaluateResponseAsync<ISubmodelElement>(response, response.Entity);
        }

        public async Task<IResult<IValue>> RetrieveSubmodelElementValueAsync(string submodelElementId)
        {
            var request = base.CreateRequest(GetUri(SUBMODELELEMENTS, submodelElementId, VALUE), HttpMethod.Get);
            var response = await SendRequestAsync(request);
            return await EvaluateResponseAsync<IValue>(response, response.Entity);
        }

        public async Task<IResult> UpdateSubmodelElementAsync(string submodelElementId, ISubmodelElement submodelElement)
        {
            var request = base.CreateJsonContentRequest(GetUri(SUBMODELELEMENTS, submodelElementId), HttpMethod.Put, submodelElement);
            var response = await SendRequestAsync(request);
            return await base.EvaluateResponseAsync(response, response.Entity);
        }

        public async Task<IResult> DeleteSubmodelElementAsync(string submodelElementId)
        {
            var request = base.CreateRequest(GetUri(SUBMODELELEMENTS, submodelElementId), HttpMethod.Delete);
            var response = await SendRequestAsync(request);
            return await base.EvaluateResponseAsync(response, response.Entity);
        }

        public async Task<IResult<InvocationResponse>> GetInvocationResultAsync(string operationId, string requestId)
        {
            var request = base.CreateRequest(GetUri(OPERATIONS, operationId, "invocationList", requestId), HttpMethod.Get);
            var response = await SendRequestAsync(request);
            return await EvaluateResponseAsync<InvocationResponse>(response, response.Entity);
        }

        public async Task<IResult> EventFireAsync(string eventId)
        {
            var request = base.CreateRequest(GetUri(EVENTS, eventId), HttpMethod.Post);
            var response = await SendRequestAsync(request);
            return await EvaluateResponseAsync(response, response.Entity);
        }
    }
}
