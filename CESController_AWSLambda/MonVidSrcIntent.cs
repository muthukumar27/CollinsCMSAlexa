using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Text;
using Alexa.NET.Request.Type;
using Alexa.NET.Response;
using Alexa.NET.Request;
using Alexa.NET.Response.Directive;

namespace CESController_AWSLambda
{
    public class MonVidSrcIntent : IntentHandler
    {
        const string MONVIDSRC = "Monitor_Video_Src";
        const string MONITORLOC = "Monitor_Loc";
        //Dictionary<string, VideoSource> vidSrcList;
        VideoSourceHandler vidSrcHandler = new VideoSourceHandler();

        public MonVidSrcIntent()
        {
            helpMsg = new StringBuilder("You can say, push Blu-Ray 1 video on forward monitor, or, you can say exit... What can I help you with?");
            rePromptMsg = new StringBuilder("Please say something like, push blu-ray 1 video on forward monitor!");
            //vidSrcList = handler.ConfiguredVidSrcList;       
        }

        public override SkillResponse HandleIntentRequest(IntentRequest request)
        {
            var log = curContext.Logger;
            slots = request.Intent.Slots;
           
            Slot monVidSrcSlot = null;
            Slot monLocSlot = null;

            if (slots.ContainsKey(MONVIDSRC))
            {
                monVidSrcSlot = slots[MONVIDSRC];
                log.LogLine($"Slot : Monitor_Video_Src : Value -" + monVidSrcSlot.Value);
            }
            if (slots.ContainsKey(MONITORLOC))
            {
                monLocSlot = slots[MONITORLOC];
                log.LogLine($"Slot : Monitor_Loc : Value - " + monLocSlot.Value);
            }

            if (String.IsNullOrEmpty(monVidSrcSlot.Value) || String.IsNullOrWhiteSpace(monVidSrcSlot.Value))
            {                
                log.LogLine($"Dialog State - " + request.DialogState);

                // Using Alexa sdk's DialogDelegate                
                dlgElicitSlot = new DialogElicitSlot(monVidSrcSlot.Name);
                outSpeech = new PlainTextOutputSpeech();               
                (outSpeech as PlainTextOutputSpeech).Text = "Tell me the name of the video source that you wanted to watch. " + "You can say, " + vidSrcHandler.GetRandomVidSrcName() + ", or , " + vidSrcHandler.GetRandomVidSrcName();
                dlgElicitSlot.UpdatedIntent = request.Intent;
                skillResponse.Response.Directives.Add(dlgElicitSlot);
                skillResponse.Response.OutputSpeech = outSpeech;
                return skillResponse;               
            }

            else if(String.IsNullOrEmpty(monLocSlot.Value) || String.IsNullOrWhiteSpace(monLocSlot.Value))
            {
                dlgElicitSlot = new DialogElicitSlot(monLocSlot.Name);
                outSpeech = new PlainTextOutputSpeech();
                (outSpeech as PlainTextOutputSpeech).Text = "Tell me the location of the monitor where you wanted to watch " + monVidSrcSlot.Value + ". You can say, forward, or, Aft!";
                dlgElicitSlot.UpdatedIntent = request.Intent;
                skillResponse.Response.Directives.Add(dlgElicitSlot);
                skillResponse.Response.OutputSpeech = outSpeech;
                return skillResponse;
            }
            else if (!vidSrcHandler.IsConfigured(monVidSrcSlot.Value.ToLower()))
            {
                dlgElicitSlot = new DialogElicitSlot(monVidSrcSlot.Name);
                outSpeech = new PlainTextOutputSpeech();
                
                (outSpeech as PlainTextOutputSpeech).Text = monVidSrcSlot.Value + " is not configured on your aircraft! " +  "You can say, " + vidSrcHandler.GetRandomVidSrcName() + ", or , " + vidSrcHandler.GetRandomVidSrcName();
                dlgElicitSlot.UpdatedIntent = request.Intent;
                skillResponse.Response.Directives.Add(dlgElicitSlot);
                skillResponse.Response.OutputSpeech = outSpeech;
                return skillResponse;
            }
            else
            {

                outSpeech = new PlainTextOutputSpeech();
                (outSpeech as PlainTextOutputSpeech).Text = monVidSrcSlot.Value + " on " + monLocSlot.Value + " is received from you!";
                skillResponse.Response.OutputSpeech = outSpeech;


            }

            return skillResponse;
        }

    }
}
