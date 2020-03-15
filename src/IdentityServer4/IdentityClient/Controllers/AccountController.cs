using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using IdentityModel.Client;

namespace api
{
    [Route("account")]
    public class AccountController : ControllerBase
    {
        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login()
        {
            var client = new HttpClient();
            var disco = await client.GetDiscoveryDocumentAsync("http://localhost:5000");
            if (disco.IsError)
            {
                Console.WriteLine(disco.Error);
            }

            var tokenResponse = await client.RequestClientCredentialsTokenAsync(new ClientCredentialsTokenRequest
            {
                Address = disco.TokenEndpoint,

                ClientId = "client",
                ClientSecret = "secret",
                Scope = "api1"
            });

            if (tokenResponse.IsError)
            {
                Console.WriteLine(tokenResponse.Error);
            }
            //return new JsonResult(from c in User.Claims select new { c.Type, c.Value });
            return new JsonResult(tokenResponse.Json);
        }

        //[HttpPost]
        //[Route("logintest")]
        //public async Task<IActionResult> LoginTest()
        //{
        //    var client = new HttpClient();
        //    var disco = await client.GetDiscoveryDocumentAsync("http://localhost:5000");
        //    if (disco.IsError)
        //    {
        //        Console.WriteLine(disco.Error);
        //    }
        //    return new JsonResult(from c in User.Claims select new { c.Type, c.Value });
        //}
    }
}
