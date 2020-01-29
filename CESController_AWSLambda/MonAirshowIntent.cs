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
    public class MonAirshowIntent : IntentHandler
    {
        const string MONAIRSHOW = "Monitor_Airshow_Mode";
        const string MONITORLOC = "Monitor_Loc";
        //Dictionary<string, VideoSource> vidSrcList;
        VideoSourceHandler vidSrcHandler = new VideoSourceHandler();

        public MonAirshowIntent()
        {
            helpMsg = new StringBuilder("You can say, push Blu-Ray 1 video on forward monitor, or, you can say exit... What can I help you with?");
            rePromptMsg = new StringBuilder("Please say something like, push blu-ray 1 video on forward monitor!");
            //vidSrcList = handler.ConfiguredVidSrcList;       
        }

        async public override Task<SkillResponse> HandleIntentRequest(IntentRequest request)
        {
            var log = curContext.Logger;
            slots = request.Intent.Slots;
           
            Slot monAirshowSlot = null;
            Slot monLocSlot = null;

            if (slots.ContainsKey(MONAIRSHOW))
            {
                monAirshowSlot = slots[MONAIRSHOW];
                log.LogLine($"Slot : Monitor_Video_Src : Value -" + monAirshowSlot.Value);
            }
            if (slots.ContainsKey(MONITORLOC))
            {
                monLocSlot = slots[MONITORLOC];
                log.LogLine($"Slot : Monitor_Loc : Value - " + monLocSlot.Value);
            }

            //if (String.IsNullOrEmpty(monVidSrcSlot.Value) || String.IsNullOrWhiteSpace(monVidSrcSlot.Value))
            //{
            //    log.LogLine($"Dialog State - " + request.DialogState);

            //    // Using Alexa sdk's DialogDelegate                
            //    dlgElicitSlot = new DialogElicitSlot(monVidSrcSlot.Name);
            //    outSpeech = new PlainTextOutputSpeech();
            //    (outSpeech as PlainTextOutputSpeech).Text = "Ok!. I can help you on that!. Tell me the name of the video source that you wanted to watch. " + "You can say, " + vidSrcHandler.GetRandomVidSrcName() + ", or , " + vidSrcHandler.GetRandomVidSrcName();
            //    dlgElicitSlot.UpdatedIntent = request.Intent;
            //    skillResponse.Response.Directives.Add(dlgElicitSlot);
            //    skillResponse.Response.OutputSpeech = outSpeech;
            //    return skillResponse;
            //}

            //else if (String.IsNullOrEmpty(monLocSlot.Value) || String.IsNullOrWhiteSpace(monLocSlot.Value))
            //{
            //    dlgElicitSlot = new DialogElicitSlot(monLocSlot.Name);
            //    outSpeech = new PlainTextOutputSpeech();
            //    (outSpeech as PlainTextOutputSpeech).Text = "Tell me the location of the monitor where you wanted to watch " + monVidSrcSlot.Value + ". You can say, forward, or, Aft!";
            //    dlgElicitSlot.UpdatedIntent = request.Intent;
            //    skillResponse.Response.Directives.Add(dlgElicitSlot);
            //    skillResponse.Response.OutputSpeech = outSpeech;
            //    return skillResponse;
            //}
            //if (!vidSrcHandler.IsConfigured(monVidSrcSlot.Value.ToLower()))
            //{
            //    log.LogLine($"Not Configured - " + monVidSrcSlot.Value);
            //    dlgElicitSlot = new DialogElicitSlot(monVidSrcSlot.Name);
            //    outSpeech = new PlainTextOutputSpeech();

            //    StringBuilder vidSources = new StringBuilder();
            //    vidSrcHandler.GetAvailableVidSources().ForEach(vidSrc => vidSources.AppendLine(vidSrc));

            //    (outSpeech as PlainTextOutputSpeech).Text = monVidSrcSlot.Value + " is not available on your aircraft! " + "Currently available video sources are \n" + vidSources.ToString();
            //    dlgElicitSlot.UpdatedIntent = request.Intent;
            //    skillResponse.Response.Directives.Add(dlgElicitSlot);
            //    skillResponse.Response.OutputSpeech = outSpeech;
            //    return skillResponse;
            //}
            //else
            //{
                Monitor_Video_Source monAirshowModeMsg = new Monitor_Video_Source
                {
                    DeviceType = vidSrcHandler.ConfiguredVidSrcList[monAirshowSlot.Value.ToLower()].DeviceType,
                    Instance = vidSrcHandler.ConfiguredVidSrcList[monAirshowSlot.Value.ToLower()].Instance,
                    MonitorLocation = monLocSlot.Value
                };
                                              
                AlexaMsg msg = new AlexaMsg
                {
                    ID = 1010,
                    IntentName = request.Intent.Name,
                    Slot = monAirshowModeMsg

                };

                await DynamoDB.PutAlexaMsg(msg);

                outSpeech = new PlainTextOutputSpeech();
                (outSpeech as PlainTextOutputSpeech).Text = "Alright. " + monAirshowSlot.Value + " Airshow mode is pushed on " + monLocSlot.Value + " Monitor. Have a nice flight!";
              //(outSpeech as PlainTextOutputSpeech).Text = "Have a nice flight!.";
                skillResponse.Response.OutputSpeech = outSpeech;
                skillResponse.Response.ShouldEndSession = true;

            //}

            return skillResponse;
        }

    }
}
