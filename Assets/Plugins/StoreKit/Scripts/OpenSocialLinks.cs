using UnityEngine;
using System.Collections;

public class OpenSocialLinks : MonoBehaviour {
    public enum Links
    {
        ODESIE,
        TECHTRANSFER,
        FACEBOOK,
        TWITTER,
        GOOGLEPLUS,
        LINKEDIN
    }
    public Links urlLink;

    void OnMouseUp()
    {
        OpenLinkURL();
    }

    void Update()
    {
#if UNITY_IPHONE
        if (Input.touchCount > 0)
        {
            if (guiTexture.HitTest(Input.GetTouch(0).position))
            {
                if (Input.GetTouch(0).phase == TouchPhase.Began)
                {
                    OpenLinkURL();
                }
            }
        }
#endif
    }

    void OpenLinkURL()
    {
        string url = "";

        switch (urlLink)
        {
            case Links.FACEBOOK:
                url = "https://www.facebook.com/TechnologyTransferServices";
                break;
            case Links.GOOGLEPLUS:
                url = "https://plus.google.com/113679131764785660319";
                break;
            case Links.LINKEDIN:
                url = "http://www.linkedin.com/company/technology-transfer-services";
                break;
            case Links.ODESIE:
                url = "http://www.myodesie.com";
                break;
            case Links.TECHTRANSFER:
                url = "http://www.techtransfer.com";
                break;
            case Links.TWITTER:
                url = "https://twitter.com/TTSTampa";
                break;
        }

        Application.OpenURL(url);
    }
}
