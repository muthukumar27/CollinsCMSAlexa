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

        const string HelpMessage = "You can say Push Blu-Ray 1 on forward monitor, or, you can say exit... What can I help you with?";
        const string HelpReprompt = "You can say stream blu-ray 1 video on forward monitor";
        const string StopMessage = "Have a good one!";
        

        /// <summary>
        /// A simple function that takes a string and does a ToUpper
        /// </summary>
        /// <param name="input"></param>
        /// <param name="context"></param>
        /// <returns></returns>
        public SkillResponse FunctionHandler(SkillRequest input, ILambdaContext context)
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
                    case "Monitor_Video_Source":
                        IntentHandler intentHandler = new MonVidSrcIntent();                        
                        intentHandler.CurContext = context;
                        return intentHandler.HandleIntentRequest(intentRequest);                        
                        //break;
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
