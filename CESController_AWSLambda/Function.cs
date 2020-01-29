using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Amazon.Lambda.Core;
using Alexa.NET.Request;
using Alexa.NET.Request.Type;
using Alexa.NET.Response;
using Alexa.NET.Response.Directive;
using Alexa.NET.Helpers;
using Newtonsoft.Json;

// Assembly attribute to enable the Lambda function's JSON input to be converted into a .NET class.
[assembly: LambdaSerializer(typeof(Amazon.Lambda.Serialization.Json.JsonSerializer))]

namespace CESController_AWSLambda
{
    public class Function
    {

        const string HelpMessage = "Collins cabin system offers you to \n" +
            "Watch movies on a monitor..... \n" +
            "track your flight information with high definition 3D maps...... \n" +
            "And control cabin lighting systems....... You can say something like... \n" +
            "Push HDMI movie on forward monitor...... \n" +
            "or Shut off cabin reading lights.";
        const string HelpReprompt = "You can say push HDMI movie on forward monitor or shut off wash lights!";
        const string FallBackIntent = "<speak> Sorry, I could not get that <break strength=\"strong\"/>" +
                            "Collins cabin system offers you to <break strength=\"medium\"/>" +
                            "Watch movies on a monitor <break strength=\"strong\"/>" +
                            "track your flight information with live 3D maps <break strength=\"strong\"/>" +
                            "And control cabin lighting systems <break strength=\"strong\"/>" +
                            "What would collins cabin system do for you ?. You can say something like <break time=\"400ms\"/>" +
                            "Push HDMI movie on forward monitor <break time=\"400ms\"/>" +
                            "or Shut off cabin reading lights. <break time=\"400ms\"/>" +
                            "</speak>";
        const string StopMessage = "Have a good one!";
        

        /// <summary>
        /// A simple function that takes a string and does a ToUpper
        /// </summary>
        /// <param name="input"></param>
        /// <param name="context"></param>
        /// <returns></returns>
        async public Task<SkillResponse> FunctionHandler(SkillRequest input, ILambdaContext context)
        {            
            SkillResponse response = new SkillResponse();
            response.Response = new ResponseBody();
            response.Response.ShouldEndSession = false;           
            IOutputSpeech innerResponse = null;
            var log = context.Logger;
            Dictionary<string, Slot> slots = new Dictionary<string, Slot>();
            log.LogLine($"Skill Request Object:");
            log.LogLine(JsonConvert.SerializeObject(input));


            if (input.GetRequestType() == typeof(LaunchRequest))
            {
                log.LogLine($"LaunchRequest made, Skill ID is " + input.Context.System.Application.ApplicationId);
                LaunchRequestHandler launchHandler = new LaunchRequestHandler();
                return launchHandler.HandleLaunchRequest(input.Context.System.Application.ApplicationId);
            }
            else if (input.GetRequestType() == typeof(IntentRequest))
            {
                var intentRequest = (IntentRequest)input.Request;

                switch (intentRequest.Intent.Name)
                {
                    case "AMAZON.CancelIntent":
                        log.LogLine($"AMAZON.CancelIntent: send StopMessage");
                        innerResponse = new PlainTextOutputSpeech();
                        (innerResponse as PlainTextOutputSpeech).Text = StopMessage;
                        response.Response.ShouldEndSession = true;
                        break;
                    case "AMAZON.StopIntent":
                        log.LogLine($"AMAZON.StopIntent: send StopMessage");
                        innerResponse = new PlainTextOutputSpeech();
                        (innerResponse as PlainTextOutputSpeech).Text = StopMessage;
                        response.Response.ShouldEndSession = true;
                        break;
                    case "AMAZON.HelpIntent":
                        log.LogLine($"AMAZON.HelpIntent: send HelpMessage");
                        innerResponse = new PlainTextOutputSpeech();
                        (innerResponse as PlainTextOutputSpeech).Text = HelpMessage;
                        break;
                    case "AMAZON.FallbackIntent":
                        log.LogLine($"AMAZON.FallbackIntent: send HelpMessage");
                        //innerResponse = new PlainTextOutputSpeech();
                        //(innerResponse as PlainTextOutputSpeech).Text = FallBackIntent;
                        innerResponse = new SsmlOutputSpeech();
                        (innerResponse as SsmlOutputSpeech).Ssml = FallBackIntent;
                        break;
                    case "Monitor_Video_Source":
                        IntentHandler intentHandler = new MonVidSrcIntent();                        
                        intentHandler.CurContext = context;
                        SkillResponse resp = await intentHandler.HandleIntentRequest(intentRequest);
                        return resp;
                    case "Monitor_Airshow":
                        IntentHandler monAirshowIntentHandler = new MonAirshowIntent();
                        monAirshowIntentHandler.CurContext = context;
                        SkillResponse monAirshowIntentHandlerResp = await monAirshowIntentHandler.HandleIntentRequest(intentRequest);
                        return monAirshowIntentHandlerResp;
                    //break;

                    case "List_Video_Sources":
                        IntentHandler listVidSrcIntentHandler = new ListVidSrcIntent();
                        listVidSrcIntentHandler.CurContext = context;
                        SkillResponse listVidSrcResponse = await listVidSrcIntentHandler.HandleIntentRequest(intentRequest);
                        return listVidSrcResponse;

                    case "Cabin_Wash_Lights":
                        IntentHandler cabinWashLtsIntentHandler = new WashLtsIntent();
                        cabinWashLtsIntentHandler.CurContext = context;
                        SkillResponse washLtResponse = await cabinWashLtsIntentHandler.HandleIntentRequest(intentRequest);
                        return washLtResponse;
                    default:
                        log.LogLine($"Unknown intent: " + intentRequest.Intent.Name);
                        innerResponse = new PlainTextOutputSpeech();
                        (innerResponse as PlainTextOutputSpeech).Text = HelpReprompt;
                        break;
                }
            }

            response.Response.OutputSpeech = innerResponse;
            response.Version = "1.0";
            log.LogLine($"Skill Response Object...");
            log.LogLine(JsonConvert.SerializeObject(response));
            return response;
        }
    }
}
