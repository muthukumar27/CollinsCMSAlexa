using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CESController_AWSLambda
{
    public class VideoSourceHandler
    {
        Dictionary<string, VideoSource> configuredVidSrcList;
        Random rand = new Random();

        public VideoSourceHandler()
        {
            configuredVidSrcList = new Dictionary<string, CESController_AWSLambda.VideoSource>();
            LoadConfiguredVidSrcList();
        }

        public Dictionary<string, VideoSource> ConfiguredVidSrcList
        {
            get
            {
                return configuredVidSrcList;
            } 
        }

        private void LoadConfiguredVidSrcList()
        {
            configuredVidSrcList.Add("blu-ray 1", new VideoSource { DeviceType = "BDP",Instance = 1, IsDeviceNumberRequired = false});
            configuredVidSrcList.Add("hdmi", new VideoSource { DeviceType = "HDMI", Instance = 5, IsDeviceNumberRequired = false });
            configuredVidSrcList.Add("forward camera", new VideoSource { DeviceType = "CAMERA", Instance = 1, IsDeviceNumberRequired = false });
            configuredVidSrcList.Add("blu-ray 2", new VideoSource { DeviceType = "BDP", Instance = 2, IsDeviceNumberRequired = false });
            configuredVidSrcList.Add("blu-ray 3", new VideoSource { DeviceType = "BDP", Instance = 3, IsDeviceNumberRequired = false });
            configuredVidSrcList.Add("left camera", new VideoSource { DeviceType = "CAMERA", Instance = 3, IsDeviceNumberRequired = false });
            configuredVidSrcList.Add("blu-ray 4", new VideoSource { DeviceType = "BDP", Instance = 4, IsDeviceNumberRequired = false });
            configuredVidSrcList.Add("bdp1", new VideoSource { DeviceType = "BDP", Instance = 1, IsDeviceNumberRequired = false });
            configuredVidSrcList.Add("bdp2", new VideoSource { DeviceType = "BDP", Instance = 2, IsDeviceNumberRequired = false });
            configuredVidSrcList.Add("bdp3", new VideoSource { DeviceType = "BDP", Instance = 3, IsDeviceNumberRequired = false });
            configuredVidSrcList.Add("bdp4", new VideoSource { DeviceType = "BDP", Instance = 4, IsDeviceNumberRequired = false });
            configuredVidSrcList.Add("ipod", new VideoSource { DeviceType = "iPod", Instance = 20, IsDeviceNumberRequired = false });

            configuredVidSrcList.Add("total route", new VideoSource { DeviceType = "AIRSHOW", Instance = 1, IsDeviceNumberRequired = false });            
            configuredVidSrcList.Add("high resolution maps", new VideoSource { DeviceType = "AIRSHOW", Instance = 2, IsDeviceNumberRequired = false });
            configuredVidSrcList.Add("city lights", new VideoSource { DeviceType = "AIRSHOW", Instance = 4, IsDeviceNumberRequired = false });
            configuredVidSrcList.Add("time zone", new VideoSource { DeviceType = "AIRSHOW", Instance = 5, IsDeviceNumberRequired = false });
            configuredVidSrcList.Add("airshow", new VideoSource { DeviceType = "AIRSHOW", Instance = 14, IsDeviceNumberRequired = false });
            configuredVidSrcList.Add("maps", new VideoSource { DeviceType = "AIRSHOW", Instance = 6, IsDeviceNumberRequired = false });
            configuredVidSrcList.Add("global zoom", new VideoSource { DeviceType = "AIRSHOW", Instance = 7, IsDeviceNumberRequired = false });
            configuredVidSrcList.Add("compass", new VideoSource { DeviceType = "AIRSHOW", Instance = 9, IsDeviceNumberRequired = false });
            configuredVidSrcList.Add("your flight", new VideoSource { DeviceType = "AIRSHOW", Instance = 10, IsDeviceNumberRequired = false });
            configuredVidSrcList.Add("flight information", new VideoSource { DeviceType = "AIRSHOW", Instance = 12, IsDeviceNumberRequired = false });
            configuredVidSrcList.Add("cycle maps", new VideoSource { DeviceType = "AIRSHOW", Instance = 13, IsDeviceNumberRequired = false });
            configuredVidSrcList.Add("auto play", new VideoSource { DeviceType = "AIRSHOW", Instance = 14, IsDeviceNumberRequired = false });

            configuredVidSrcList.Add("aft camera", new VideoSource { DeviceType = "CAMERA", Instance = 2, IsDeviceNumberRequired = false });
            
            configuredVidSrcList.Add("right camera", new VideoSource { DeviceType = "CAMERA", Instance = 4, IsDeviceNumberRequired = false });
            configuredVidSrcList.Add("belly camera", new VideoSource { DeviceType = "CAMERA", Instance = 6, IsDeviceNumberRequired = false });
            configuredVidSrcList.Add("down camera", new VideoSource { DeviceType = "CAMERA", Instance = 6, IsDeviceNumberRequired = false });
            configuredVidSrcList.Add("fin camera", new VideoSource { DeviceType = "CAMERA", Instance = 5, IsDeviceNumberRequired = false });
            configuredVidSrcList.Add("glareshield camera", new VideoSource { DeviceType = "CAMERA", Instance = 7, IsDeviceNumberRequired = false });
        }

        public bool IsConfigured(string srcName)
        {
            return configuredVidSrcList.ContainsKey(srcName);
        }

        public string GetRandomVidSrcName()
        {            
            return configuredVidSrcList.ElementAt<KeyValuePair<string, VideoSource>>(rand.Next(0, configuredVidSrcList.Count)).Key;
        }

        public List<string> GetAvailableVidSources()
        {
            List<string> availableVidSrcList = new List<string> { "Blu ray players", "HDMI", "iPod"};
            return availableVidSrcList;
        }
        
    }
}
