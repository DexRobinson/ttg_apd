using UnityEngine;
using System.Collections;

public class PhoneDetection {
    public enum Device
    {
        _3by2,
        _16by9,
        _4by3,
        _16by10
    }
    public static Device device;

    public static void Init()
    {
        float aspectRatio = (float)Screen.width / (float)Screen.height;
		
        if (aspectRatio == 1.5f)    // iPhone4, iPhone4s
        {
            device = Device._3by2;
            //QualitySettings.SetQualityLevel(0);
        }
        else if (aspectRatio >= 1.76f && aspectRatio <= 1.79f)   // iPhone5
        {
            device = Device._16by9;
            //QualitySettings.SetQualityLevel(1);
        }
        else if (aspectRatio >= 1.32f && aspectRatio < 1.34f)   // iPad
        {
			
            //if (iPhone.generation == iPhoneGeneration.iPad3Gen )
            //{
               device = Device._4by3;
                QualitySettings.SetQualityLevel(1);
            //}
            //else
            //{
              //  device = Device._4by3;
                //QualitySettings.SetQualityLevel(0);
            //}
        }
        else if (aspectRatio >= 1.59f && aspectRatio < 1.61f)   // wide screen, mainly used on android devices
        {
            device = Device._16by10;
            //QualitySettings.SetQualityLevel(0);
        }
        else
        {
            device = Device._3by2;
            //QualitySettings.SetQualityLevel(0);
        }

        //Debug.Log(device);
    }

    public static Device CurrentDevice()
    {
        return device;
    }
}
