using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CESController_AWSLambda
{
    public class VideoSource
    {
        string name = string.Empty;
        string cmdVal = string.Empty;
        bool isDevNumReq = false;

        public VideoSource(string _name, string _cmdVal, bool _isDevNumReq)
        {
            name = _name;
            cmdVal = _cmdVal;
            isDevNumReq = _isDevNumReq;
        }
        public string Name
        {
            get
            {
                return name;
            }
            set
            {
                name = value;
            }
        }
        public string CommandValue
        {
            get
            {
                return cmdVal;
            }
            set
            {
                cmdVal = value;
            }
        }
        public bool IsDeviceNumberRequired
        {
            get
            {
                return isDevNumReq;
            }
            set
            {
                isDevNumReq = value;
            }
        }
    }
}
