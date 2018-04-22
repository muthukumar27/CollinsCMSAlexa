using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Text;
using Alexa.NET.Request;
using Alexa.NET.Response;
using Alexa.NET.Request.Type;
using Alexa.NET.Response.Directive;
using Amazon.Lambda.Core;

namespace CESController_AWSLambda
{
    public abstract class IntentHandler
    {
        protected StringBuilder helpMsg = new StringBuilder();
        protected StringBuilder rePromptMsg = new StringBuilder();
        protected string stopMsg = "Have a good one!";
        protected string skillRespVersion = "1.0";
        protected IOutputSpeech outSpeech = null;
        protected ILambdaContext curContext;
        protected SkillResponse skillResponse = null;
        protected DialogElicitSlot dlgElicitSlot = null;

        public IntentHandler()
        {
            InitializeSkillResponse();
        }

        protected Dictionary<string, Slot> slots;

        public StringBuilder HelpMsg
        {
            get
            {
                return helpMsg;
            }
            set
            {
                helpMsg = value;
            }
        }
        public StringBuilder RepromptMsg
        {
            get
            {
                return rePromptMsg;
            }
            set
            {
                rePromptMsg = value;
            }
        }
        public string StopMsg
        {
            get
            {
                return stopMsg;
            }
            set
            {
                stopMsg = value;
            }
        }

        public string SkillRespVersion
        {
            get
            {
                return skillRespVersion;
            }
            set
            {
                skillRespVersion = value;
            }
        }

        public ILambdaContext CurContext
        {
            get
            {
                return curContext;
            }
            set
            {
                curContext = value;
            }
        }

        void InitializeSkillResponse()
        {
            skillResponse = new SkillResponse();
            skillResponse.Response = new ResponseBody();
            skillResponse.Response.ShouldEndSession = false;
            skillResponse.Version = skillRespVersion;
        }

        abstract public SkillResponse HandleIntentRequest(IntentRequest request);

    }
}
