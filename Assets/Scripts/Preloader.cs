using UnityEngine;
using System.Collections;

public class Preloader : MonoBehaviour 
{
    public bool firstScene;
	public bool victoryScene;
    public GameObject newspaper;
    public Texture2D[] newspaperGraphic;
	public Texture2D evacGraphic;
	
	public float preTime = 2.0f;
    private float preTimer;

    void Start()
    {
        if(!firstScene)
            newspaper.renderer.material.mainTexture = newspaperGraphic[DontDestoryValues.instance.planetNumber];
		
		if(victoryScene)
			newspaper.renderer.material.mainTexture = newspaperGraphic[DontDestoryValues.instance.planetNumber];
		
		if(DontDestoryValues.instance.gameType == 1)
			newspaper.renderer.material.mainTexture = evacGraphic;
    }

    void Update()
    {
        // runs a cache to the items inside of the resouces folder
        if (preTimer > preTime)
        {
            Resources.UnloadUnusedAssets();
	            if (firstScene)
	            {
	                Application.LoadLevel("MainMenu");
	            }
	            else
	                Application.LoadLevel("Game");
        }
        else
        {
            preTimer += Time.deltaTime;
        }
    }
}