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
    public class Monitor_Video_Source
    {
        public string DeviceType { get; set; }
        public int Instance { get; set; }
        public string MonitorLocation { get; set; }
    }

    public class MonVidSrcConverter : IPropertyConverter
    {
        public DynamoDBEntry ToEntry(object value)
        {
            Monitor_Video_Source MonVidSrc = value as Monitor_Video_Source;
            if (MonVidSrc == null) throw new ArgumentOutOfRangeException();

            //string data = string.Format("\"DeviceType\":\"{1}\"{0}\"Instance\":\"{2}\"{0}\"MonitorLocation\":\"{3}\"", ",",
            //MonVidSrc.DeviceType, MonVidSrc.Instance.ToString(), MonVidSrc.MonitorLocation);
            string data = JsonConvert.SerializeObject(MonVidSrc);

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

            Monitor_Video_Source complexData = new Monitor_Video_Source
            {
                DeviceType = data[0],
                Instance = Convert.ToInt32(data[1]),
                MonitorLocation = data[2]
            };
            return complexData;
        }
    }
}
