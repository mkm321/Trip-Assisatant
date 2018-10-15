using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using ApiAiSDK;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace TripAssistantDialogFlowApi.Controllers
{
    [Produces("application/json")]
    [Route("api/FetchContext")]
    public class FetchContextController : Controller
    {
        static private ApiAi apiAi;
        [HttpGet("{contextInput}")]
        public string GetResponseFromDialogFlow(string contextInput)
        {
            string res = FetchDataFromDialogFlow(contextInput);
            string result = "";
            string[] response = res.Split(" ");
            if(response[0].Equals("yes") || response[0].Equals("no"))
            {
                result += response[0];
                if (response[0].Equals("yes"))
                {
                    if (response[response.Length - 1].Equals("complete"))
                    {
                        result += " "+ response[1] + " " + response[2];
                    }
                    else if(response[response.Length - 1].Equals("only"))
                    {
                        result += " " + response[1] + " 1";
                    }
                    else if(response[response.Length - 1].Equals("remove"))
                    {
                        if (response[1].Equals("not"))
                        {
                            string remove = response[1] + " " + response[2].ToLower();
                            res = contextInput.Replace(remove, "");
                            res = GetResponseFromDialogFlow(res);
                            result = "";
                            result += res;
                        }
                        else
                        {
                            result = "no I cannot process this request!";
                        }
                    }
                    else if(response[response.Length - 1].Equals("duration"))
                    {
                        result += " " + response[1] + " current";
                    }
                }
                else
                {
                    if (response[response.Length - 1].Equals("send"))
                    {
                        for(int index = 2; index <= 7; index++)
                        {
                            result += " " + response[index];
                        }
                    }
                    else if(response[response.Length - 1].Equals("trip"))
                    {
                       int index= Array.IndexOf(response,"activity");
                        for(int iterator = index+1; iterator < response.Length - 1; iterator++)
                        {
                            result += " " + response[iterator];
                        }
                    }
                    else
                    {
                        result +=" I can not process this request!!";
                    }
                }
            }
            else
            {
                result = "Other";
            }

            return result;
        }
        public string FetchDataFromDialogFlow(string context)
        {
            var config = new AIConfiguration("ada088ff76ae462fb2a17e5ee0df4c9b", SupportedLanguage.English);
            apiAi = new ApiAi(config);
            var response = apiAi.TextRequest(context);
            string result = response.Result.Fulfillment.Speech;
            return result;
        }
    }
}