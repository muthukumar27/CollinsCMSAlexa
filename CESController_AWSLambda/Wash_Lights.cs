using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Amazon.DynamoDBv2.DataModel;
using Amazon.DynamoDBv2.Internal;
using Amazon.DynamoDBv2.DocumentModel;
using Newtonsoft.Json;

namespace CESController_AWSLambda
{
    public class Wash_Lights
    {
        public string DeviceType { get; set; }
        public int Instance { get; set; }
        public string LightState { get; set; }
    }

    public class WashLtsConverter : IPropertyConverter
    {
        public DynamoDBEntry ToEntry(object value)
        {
            Wash_Lights WashLtState = value as Wash_Lights;
            if (WashLtState == null) throw new ArgumentOutOfRangeException();

            //string data = string.Format("\"DeviceType\":\"{1}\"{0}\"Instance\":\"{2}\"{0}\"MonitorLocation\":\"{3}\"", ",",
            //MonVidSrc.DeviceType, MonVidSrc.Instance.ToString(), MonVidSrc.MonitorLocation);
            string data = JsonConvert.SerializeObject(WashLtState);

            DynamoDBEntry entry = new Primitive
            {
                Value = data
            };
            return entry;
        }

        public object FromEntry(DynamoDBEntry entry)
        {
            Primitive primitive = entry as Primitive;
            if (primitive == null || !(primitive.Value is String) || string.IsNullOrEmpty((string)primitive.Value))
                throw new ArgumentOutOfRangeException();

            string[] data = ((string)(primitive.Value)).Split(new string[] { " x " }, StringSplitOptions.None);
            if (data.Length != 3) throw new ArgumentOutOfRangeException();

            Wash_Lights complexData = new Wash_Lights
            {
                DeviceType = data[0],
                Instance = Convert.ToInt32(data[1]),
                LightState = data[2]
            };
            return complexData;
        }
    }
}
