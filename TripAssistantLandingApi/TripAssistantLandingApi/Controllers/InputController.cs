using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace TripAssistantLandingApi.Controllers
{
    [Produces("application/json")]
    [Route("api/Input")]
    public class InputController : Controller
    {
        [HttpGet]
        public async Task<string> GetInputAsync([FromQuery] string input, [FromQuery] string location)
        {
            HttpClient client = new HttpClient();
            string query = "http://localhost:64030/api/FetchContext/" + input;
            var result = await client.GetAsync(query);
            string res = await result.Content.ReadAsStringAsync();
            res = res.Replace("\"", "");
            string[] response = res.Split(" ");
            if (response[0].Equals("yes"))
            {
                string queryString = "";
                if (response[response.Length - 1].Equals("current"))
                {
                    queryString = location + " " + response[1];
                }
                else
                {
                    queryString = response[1] + " " + response[2];
                }
                string url = "http://localhost:50021/api/TripResults?context=" + queryString;
                result = await client.GetAsync(url);
                res = await result.Content.ReadAsStringAsync();
                res = res.Replace("\"", "");
            }
            else if (response[0].Equals("no"))
            {
                res = "";
                for(int index = 1; index < response.Length; index++)
                {
                    res += response[index]+" ";
                }
            }
            else
            {
                res = "This Request was beyond my power!!!";
            }
            return res;
        }
    }
}