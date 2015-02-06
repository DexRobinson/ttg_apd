using UnityEngine;
using System.Collections;

public class PhoneDetectionGUI : MonoBehaviour {

    [HideInInspector]
    public Rect _3by2Scale;
    [HideInInspector]
    public Vector3 _3by2Position;
    [HideInInspector]
    public Rect _16by9Scale;
    [HideInInspector]
    public Vector3 _16by9Position;
    [HideInInspector]
    public Rect _4by3Scale;
    [HideInInspector]
    public Vector3 _4by3Position;
    [HideInInspector]
    public Rect _16by10Scale;
    [HideInInspector]
    public Vector3 _16by10Position;

    // Use this for initialization
    void Start()
    {
        if (PhoneDetection.CurrentDevice() == PhoneDetection.Device._3by2)
        {
            guiTexture.pixelInset = _3by2Scale;
            transform.position = _3by2Position;
        }
        else if (PhoneDetection.CurrentDevice() == PhoneDetection.Device._16by9)
        {
            transform.position = _16by9Position;
            guiTexture.pixelInset = _16by9Scale;
        }
        if (PhoneDetection.CurrentDevice() == PhoneDetection.Device._4by3)
        {
            transform.position = _4by3Position;
            guiTexture.pixelInset = _4by3Scale;
        }
        if (PhoneDetection.CurrentDevice() == PhoneDetection.Device._16by10)
        {
            transform.position = _16by10Position;
            guiTexture.pixelInset = _16by10Scale;
        }
    }
}
