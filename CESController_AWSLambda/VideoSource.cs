using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CESController_AWSLambda
{
    public class VideoSource
    {        
        //int instance;
        //string deviceType = string.Empty;
        //bool isDevNumReq = false;

        //public VideoSource(string _deviceType, int _instance, bool _isDevNumReq)
        //{
        //    deviceType = _deviceType;
        //    instance = _instance;
        //    isDevNumReq = _isDevNumReq;
        //}
        public string DeviceType
        {
            get;
            set;
        }
        public int Instance
        {
            get;
            set;
        }
        public bool IsDeviceNumberRequired
        {
            get;
            set;
        }
    }
}
