﻿namespace DreamFactory.Api.Implementation
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using DreamFactory.Http;
    using DreamFactory.Model;
    using DreamFactory.Model.Database;
    using DreamFactory.Model.System.Config;
    using DreamFactory.Model.System.Environment;
    using DreamFactory.Model.System.Script;
    using DreamFactory.Serialization;

    internal partial class SystemApi : BaseApi, ISystemApi
    {
        public SystemApi(IHttpAddress baseAddress, IHttpFacade httpFacade, IContentSerializer contentSerializer, HttpHeaders baseHeaders)
            : base(baseAddress, httpFacade, contentSerializer, baseHeaders, "system")
        {
        }

        public async Task<EnvironmentResponse> GetEnvironmentAsync()
        {
            return await RequestAsync<EnvironmentResponse>(HttpMethod.Get, "environment", new SqlQuery());
        }

        public async Task<IEnumerable<string>> GetConstantsAsync()
        {
            Dictionary<string, object> result = await RequestAsync<Dictionary<string, object>>(HttpMethod.Get, "constant", new SqlQuery());
            return result.Keys;
        }

        public async Task<Dictionary<string, string>> GetConstantAsync(string constant)
        {
            var result = await RequestAsync<Dictionary<string, Dictionary<string, string>>>(HttpMethod.Get, "constant", constant, new SqlQuery());
            return result[constant];
        }

        public async Task<ConfigResponse> GetConfigAsync()
        {
            return await RequestAsync<ConfigResponse>(HttpMethod.Get, "config", new SqlQuery());
        }

        public async Task<ConfigResponse> SetConfigAsync(ConfigRequest config)
        {
            return await RequestWithPayloadAsync<ConfigRequest, ConfigResponse>(HttpMethod.Post, "config", new SqlQuery(), config);
        }

        public async Task<IEnumerable<ScriptTypeResponse>> GetScriptTypesAsync(SqlQuery query)
        {
            ResourceWrapper<ScriptTypeResponse> response = await base.RequestAsync<ResourceWrapper<ScriptTypeResponse>>(
                method: HttpMethod.Get,
                resource: "script_type", 
                query: query
                );

            return response.Records;
        }
    }
}
