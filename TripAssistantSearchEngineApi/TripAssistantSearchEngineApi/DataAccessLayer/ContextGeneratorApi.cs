using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace TripAssistantSearchEngineApi
{
    public class ContextGeneratorApi
    {
        public async Task<string[]> GetContextResponse(string input)
        {
            string[] response = null;
            using (HttpClient client = new HttpClient())
            {
                string query = "http://localhost:64030/api/FetchContext/" + input;
                var result = await client.GetAsync(query);
                string resultantResponse = await result.Content.ReadAsStringAsync();
                resultantResponse = resultantResponse.Replace("\"", "");
                response = resultantResponse.Split(' ');
            }

            return response;
        }
    }
}
