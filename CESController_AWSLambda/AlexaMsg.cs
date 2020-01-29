using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DataModel;

namespace CESController_AWSLambda
{
    [DynamoDBTable("AlexaMsg")]
    class AlexaMsg
    {
        [DynamoDBHashKey]
        public int ID
        {
            get;
            set;
        }
        [DynamoDBProperty]
        public string IntentName
        {
            get;
            set;
        }
        [DynamoDBProperty(typeof(MonVidSrcConverter))]
        public Monitor_Video_Source Slot
        {
            get;
            set;
        }
    }

    [DynamoDBTable("AlexaMsg")]
    class LightsAlexaMsg
    {
        [DynamoDBHashKey]
        public int ID
        {
            get;
            set;
        }
        [DynamoDBProperty]
        public string IntentName
        {
            get;
            set;
        }
        [DynamoDBProperty(typeof(WashLtsConverter))]
        public Wash_Lights Slot
        {
            get;
            set;
        }
    }
}
