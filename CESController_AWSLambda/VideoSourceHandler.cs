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
            configuredVidSrcList.Add("blu-ray 1", new VideoSource("Blu-Ray 1", "BDP1", false));
            configuredVidSrcList.Add("blu-ray 2", new VideoSource("Blu-Ray 2", "BDP2", false));
            configuredVidSrcList.Add("blu-ray 3", new VideoSource("Blu-Ray 3", "BDP3", false));
            configuredVidSrcList.Add("blu-ray 4", new VideoSource("Blu-Ray 4", "BDP4", false));
            configuredVidSrcList.Add("your flight", new VideoSource("Your Flight", "MMEURL1", false));
            configuredVidSrcList.Add("auto play", new VideoSource("Auto Play", "MMEURL6", false));
            configuredVidSrcList.Add("maps", new VideoSource("Maps", "MMEURL2", false));
            configuredVidSrcList.Add("high resolution maps", new VideoSource("High Resolution Maps", "MMEURL3", false));
            configuredVidSrcList.Add("logo", new VideoSource("Logo", "MMEURL4", false));
            configuredVidSrcList.Add("rli", new VideoSource("RLI", "MMEURL5", false));
            configuredVidSrcList.Add("hdmi", new VideoSource("HDMI", "HDMI", false));

            configuredVidSrcList.Add("forward camera", new VideoSource("Forward Camera", "Camera1", false));
            configuredVidSrcList.Add("aft camera", new VideoSource("Aft Camera", "Camera2", false));
            configuredVidSrcList.Add("left camera", new VideoSource("Left Camera", "Camera4", false));
            configuredVidSrcList.Add("right camera", new VideoSource("Right Camera", "Camera5", false));
            configuredVidSrcList.Add("belly camera", new VideoSource("Belly Camera", "Camera3", false));
            configuredVidSrcList.Add("down camera", new VideoSource("Down Camera", "Camera3", false));
            configuredVidSrcList.Add("fin camera", new VideoSource("Fin Camera", "Camera6", false));
            configuredVidSrcList.Add("glareshield camera", new VideoSource("Glareshield Camera", "Camera7", false));
        }

        public bool IsConfigured(string srcName)
        {
            return configuredVidSrcList.ContainsKey(srcName);
        }

        public string GetRandomVidSrcName()
        {            
            return configuredVidSrcList.ElementAt<KeyValuePair<string, VideoSource>>(rand.Next(0, configuredVidSrcList.Count)).Value.Name;
        }
        
    }
}
