using ApiAiSDK;
using System;
using Core.Contracts;
using System.Net.Http;
using System.Threading.Tasks;

namespace TripAssistantSearchEngineApi
{
    public class ContextGenerator: IContextGenerator
    {
        static private ApiAi _apiAi;
        public string GetContextResponse(string contextInput)
        {
            try
            {
                var config = new AIConfiguration("ada088ff76ae462fb2a17e5ee0df4c9b", SupportedLanguage.English);
                _apiAi = new ApiAi(config);
                var response = _apiAi.TextRequest(contextInput);
                string context = response.Result.Fulfillment.Speech;
                return context;
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
                return "Exception occured!";
            }
        }
    }
    
}
