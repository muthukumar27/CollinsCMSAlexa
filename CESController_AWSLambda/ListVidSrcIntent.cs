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
    public class ListVidSrcIntent : IntentHandler
    {
        VideoSourceHandler vidSrcHandler = new VideoSourceHandler();

        public ListVidSrcIntent()
        {
            //helpMsg = new StringBuilder("You can say, push Blu-Ray 1 video on forward monitor, or, you can say exit... What can I help you with?");
            //rePromptMsg = new StringBuilder("Please say something like, push blu-ray 1 video on forward monitor!");
            //vidSrcList = handler.ConfiguredVidSrcList;       
        }

        async public override Task<SkillResponse> HandleIntentRequest(IntentRequest request)
        {
            var log = curContext.Logger;

            StringBuilder vidSources = new StringBuilder();
            List<string> listVidSources = vidSrcHandler.GetAvailableVidSources();
            await Task.Run(() => {
            for (int index = 0; index < listVidSources.Count; index++)
            {
                    if (index == listVidSources.Count - 1)
                    { vidSources.AppendLine(String.Format(" {0} {1}", "and", listVidSources[index])); }
                    else
                    { vidSources.AppendLine(listVidSources[index]); }
            //vidSrcHandler.GetAvailableVidSources().ForEach(vidSrc => vidSources.AppendLine(vidSrc));
            }
        });

            outSpeech = new PlainTextOutputSpeech();
            //(outSpeech as PlainTextOutputSpeech).Text = "Alright!. I have sent a message to venue system to push " + monVidSrcSlot.Value + " on " + monLocSlot.Value + " Monitor.";
            (outSpeech as PlainTextOutputSpeech).Text = "Currently available video sources in your aircraft are \n" + vidSources.ToString();
            skillResponse.Response.OutputSpeech = outSpeech;
            skillResponse.Response.ShouldEndSession = false;

            return skillResponse;
        }

    }
}
