using Common;
using Common.Models;
using IdentityModel.Client;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace Reporting.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Authorize]
    public class SubscribersReportController : ControllerBase
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public SubscribersReportController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }
        
        [HttpGet]
        public async Task<SubscriberReport> GenerateSubsribersReportAsync()
        {
            //retrieve access token
            var serverClient = _httpClientFactory.CreateClient();

            var discoveryDocument = await serverClient.GetDiscoveryDocumentAsync(MicroServices.IdentitySuite.Url);
            var tokenResponse = await serverClient.RequestClientCredentialsTokenAsync(
                new ClientCredentialsTokenRequest
                {
                    Address = discoveryDocument.TokenEndpoint,

                    ClientId = MicroServices.Reporting.Id,
                    ClientSecret = "reporting_strong_password(!)",

                    Scope = new GetstringClass().GetScope(),
                    //Scope = "subscribers.read",//



                });
            //match client regesitered id, secret and allowed scopes

            //retrieve secret data
            var apiClient = _httpClientFactory.CreateClient();

            apiClient.SetBearerToken(tokenResponse.AccessToken);

            var response = await apiClient.GetAsync($"{MicroServices.Subscribtions.Url}/subscribers");

            var content = await response.Content.ReadAsStringAsync();
            var data = JsonSerializer.Deserialize<List<SubscriberBase>>(content);
            var report = new SubscriberReport() 
            { 
                SubscribersCount = data.Count    
            };

            return report;
        }
    }
    public class GetstringClass
    {
        private static string s = "subscribers.read";
        public string ScopeString()
        {
            return s;
        }
    }
}