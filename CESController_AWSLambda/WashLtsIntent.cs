using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Text;
using Alexa.NET.Request.Type;
using Alexa.NET.Response;
using Alexa.NET.Request;
using Alexa.NET.Response.Directive;
using Newtonsoft.Json;

namespace CESController_AWSLambda
{
    public class WashLtsIntent : IntentHandler
    {
        const string LIGHTCONTROl = "light_control";

        public WashLtsIntent()
        {
            //helpMsg = new StringBuilder("You can say, push Blu-Ray 1 video on forward monitor, or, you can say exit... What can I help you with?");
            //rePromptMsg = new StringBuilder("Please say something like, push blu-ray 1 video on forward monitor!");
            //vidSrcList = handler.ConfiguredVidSrcList;       
        }

        async public override Task<SkillResponse> HandleIntentRequest(IntentRequest request)
        {
            var log = curContext.Logger;
            slots = request.Intent.Slots;

            Slot lightStateSlot = null;

            if(slots.ContainsKey(LIGHTCONTROl))
            {
                lightStateSlot = slots[LIGHTCONTROl];
            }

            // Need to seperate it out.
            Wash_Lights washLtsMsg = new Wash_Lights
            {
                DeviceType = "200",
                Instance = 1,
                LightState = lightStateSlot.Value
            };

            LightsAlexaMsg msg = new LightsAlexaMsg
            {
                ID = 2000,
                IntentName = request.Intent.Name,
                Slot = washLtsMsg

            };
            await DynamoDB.PutAlexaMsg(msg);

            outSpeech = new PlainTextOutputSpeech();
            //(outSpeech as PlainTextOutputSpeech).Text = "Alright!. I have sent a message to venue system to push " + monVidSrcSlot.Value + " on " + monLocSlot.Value + " Monitor.";
            (outSpeech as PlainTextOutputSpeech).Text = "Cabin system accepted your request. Enjoy your flight!. ";
            skillResponse.Response.OutputSpeech = outSpeech;
            skillResponse.Response.ShouldEndSession = true;

            return skillResponse;
        }

    }
}
