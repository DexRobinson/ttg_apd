using UnityEngine;
using System.Collections;

public class MiniMapChange : MonoBehaviour 
{
    private int currentScreenWidth;

    void Start()
    {
        ChangeMiniMap();
    }

    void Update()
    {
        if (Screen.width != currentScreenWidth)
        {
            ChangeMiniMap();
        }
    }

	// Use this for initialization
	void ChangeMiniMap() 
    {
        if (Screen.width > Screen.height)
        {
            camera.rect = new Rect(0.05f, 0.7f, 0.25f, 0.25f);
        }
        else
        {
            camera.rect = new Rect(0.05f, 0.7f, 0.55f, 0.25f);
        }

        currentScreenWidth = Screen.width;
	}
}
