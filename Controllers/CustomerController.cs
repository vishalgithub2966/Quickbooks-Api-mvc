using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Threading.Tasks;
using System;
using System.Text;
using Intuit.Ipp.Data;
using OAuth2_CoreMVC_Sample.Models;
using Microsoft.IdentityModel.Tokens;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System.Collections.Generic;


namespace OAuth2_CoreMVC_Sample.Controllers
{
    public class CustomerController : Controller
    {
        private readonly TokensContext _tokens;
        private readonly IConfiguration _configuration;
        public CustomerController(TokensContext tokens, IConfiguration configuration)
        {
            _tokens = tokens;
            _configuration = configuration;
        }
        public async Task<ActionResult> Index()
        {
            return View();
        }

        public async Task<ActionResult> CustomerList()
        {
            string companyId = _configuration["CompanyId"];
            string minorVersion = _configuration["MinorVersion"];
            string connectUrl = _configuration["BaseUrl"];
            string baseUrl = $"{connectUrl}/v3/company/{companyId}/query?minorversion={minorVersion}";
            string accessToken = _tokens.Token.FirstOrDefaultAsync(t=>t.RealmId== companyId).Result.AccessToken;

            var client = new HttpClient();
            var request = new HttpRequestMessage(HttpMethod.Post, baseUrl);
            request.Headers.Add("Accept", "application/json");
            //request.Headers.Add("Authorization", "Bearer access");
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken); 

            var content = new StringContent("Select * from Customer ", Encoding.UTF8, "application/text");
            request.Content = content;
            var response = await client.SendAsync(request);
            if (response.IsSuccessStatusCode)
            {
                string responseContent = await response.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<RootObject>(responseContent);
                List<Intuit.Ipp.Data.Customer> customers = result.QueryResponse.Customer;
                return View(customers);
            }
            else
            {
                throw new Exception($"Failed to fetch customers: {response.ReasonPhrase}");
            }
        }


    }
}
