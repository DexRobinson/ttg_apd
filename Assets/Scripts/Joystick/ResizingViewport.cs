using UnityEngine;
using System.Collections;

// used to adjust the viewport rectangle of the main camera to force an aspect ratio

public class ResizingViewport : MonoBehaviour 
{
    public static ResizingViewport instance;

    private Rect tempRect;

    public float ratio;
    public bool isTooWide = false;

	void Awake () 
    {
        instance = this;

        ratio = Screen.width / ResizingDefaultSizes.Instance.DefaultResolutionWidth;
        if (ratio > Screen.height / ResizingDefaultSizes.Instance.DefaultResolutionHeight)
        {
            ratio = Screen.height / ResizingDefaultSizes.Instance.DefaultResolutionHeight;
            isTooWide = true;
        }
        //Screen.SetResolution(960, 640, false);
	}
	
    //void Update () 
    //{	
    //    ratio = Screen.width / ResizingDefaultSizes.Instance.DefaultResolutionWidth;
    //    if (ratio > Screen.height / ResizingDefaultSizes.Instance.DefaultResolutionHeight)
    //    {
    //        ratio = Screen.height / ResizingDefaultSizes.Instance.DefaultResolutionHeight;
    //        isTooWide = true;
    //    }
    //    else
    //    {
    //        isTooWide = false;
    //    }
		
    //    if (isTooWide && tempRect.width != ResizingDefaultSizes.Instance.DefaultResolutionWidth * ratio / Screen.width)
    //    {
    //        ratio = Screen.height / ResizingDefaultSizes.Instance.DefaultResolutionHeight;

    //        tempRect.width = ResizingDefaultSizes.Instance.DefaultResolutionWidth * ratio / Screen.width;
    //        tempRect.height = 1.0f;
    //        tempRect.x = (1.0f - tempRect.width) / 2.0f;
    //        tempRect.y = 0.0f; 
    //    }
    //    else if (!isTooWide && tempRect.height != ResizingDefaultSizes.Instance.DefaultResolutionHeight * ratio / Screen.height)
    //    {
    //        ratio = Screen.width / ResizingDefaultSizes.Instance.DefaultResolutionWidth;

    //        tempRect.width = 1.0f;
    //        tempRect.height = ResizingDefaultSizes.Instance.DefaultResolutionHeight * ratio / Screen.height;
    //        tempRect.x = 0.0f;
    //        tempRect.y = (1.0f - tempRect.height) / 2.0f;
    //    }

    //    if (camera.rect != tempRect)
    //    {
    //        camera.rect = tempRect;
    //    }
    //}
}
