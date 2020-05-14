using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using IdentityModel;
using IdentityModel.Client;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace Mvc4.Controllers
{
    [ApiController]
    public class TestController : ControllerBase
    {
        private readonly ILogger<TestController> _logger;
        private readonly IHttpClientFactory _httpClientFactory;

        public TestController(ILogger<TestController> logger,
            IHttpClientFactory httpClientFactory)
        {
            _logger = logger;
            _httpClientFactory = httpClientFactory;
        }

        [HttpGet("api/getToken")]
        public async Task<IActionResult> GetToken()
        {
            var client = _httpClientFactory.CreateClient();

            var request = new ClientCredentialsTokenRequest
            {
                RequestUri = new Uri("https://localhost:5000/connect/token"),
                ClientId = "mvc1",
                ClientSecret = "secret"
            };

            var response = await client.RequestClientCredentialsTokenAsync(request);

            var result = new
            {
                response.AccessToken,
                response.Error,
                response.ErrorDescription,
                response.ErrorType,
                response.ExpiresIn,
                response.HttpErrorReason,
                response.IdentityToken,
                response.IssuedTokenType,
                response.RefreshToken,
                response.Scope,
                response.TokenType
            };

            return Ok(result);
        }
    }
}
