using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Alexa.NET.Response;
namespace CESController_AWSLambda
{
    public class DialogDirective : IDirective
    {
        [JsonProperty("type")]
        public string Type { get; set; }
    }
}
