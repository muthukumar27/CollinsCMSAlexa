using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Alexa.NET.Response;
using Alexa.NET.Request;

namespace CESController_AWSLambda
{
    public class LaunchRequestHandler
    {
        const string vidCtrlSkillID = "amzn1.ask.skill.5a7652a2-8424-4015-8a60-c3c4dc6c5fd1";
        protected SkillResponse skillResponse = null;
        string skillRespVersion = "1.0";
        protected IOutputSpeech outSpeech = null;

        public LaunchRequestHandler()
        {
            InitializeSkillResponse();
        }

        public SkillResponse HandleLaunchRequest(string skillID)
        {
            //outSpeech = new PlainTextOutputSpeech();
            outSpeech = new SsmlOutputSpeech();            
            switch (skillID)
            {
                case vidCtrlSkillID:
                    //(outSpeech as PlainTextOutputSpeech).Text = "How may I help you on Collins Video Control ?";
                    (outSpeech as SsmlOutputSpeech).Ssml = SsmlDecorate("Hi There!");
                    break;
                default:
                    (outSpeech as PlainTextOutputSpeech).Text = "How may I help you on Rockwell's CES system ?";
                    break;
            }
            skillResponse.Response.OutputSpeech = outSpeech;
            return skillResponse;
        }

        void InitializeSkillResponse()
        {
            skillResponse = new SkillResponse();
            skillResponse.Response = new ResponseBody();
            skillResponse.Response.ShouldEndSession = false;
            skillResponse.Version = skillRespVersion;
        }

        private string SsmlDecorate(string speech)
        {
            return "<speak>" + speech + "</speak>";
        }
    }
}
