using UnityEngine;
using System.Collections;

// carries the default resolution sizes to be used in other scripts

public class ResizingDefaultSizes : MonoBehaviour 
{
    private static ResizingDefaultSizes instance = null;

    public float DefaultResolutionWidth = 960.0f;
    public float DefaultResolutionHeight = 640.0f;

    public static ResizingDefaultSizes Instance
    {
        get
        {
            if (instance == null)
            {
                GameObject temp = new GameObject();
                instance = temp.AddComponent<ResizingDefaultSizes>();
                temp.name = "ResizingDefaultSizes";
                DontDestroyOnLoad(temp);
            }

            return instance;
        }
    }
}
