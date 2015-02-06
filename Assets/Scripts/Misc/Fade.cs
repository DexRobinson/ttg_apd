using UnityEngine;
using System.Collections;

public class Fade : MonoBehaviour 
{
	public static Fade instance;
	public GUIText text;
	public GameObject image;
	public bool opening;
	public bool inGameFade;

	private Renderer rend;

	void Awake()
	{
		instance = this;
		rend = renderer;
	}
	
    void Start()
    {
        StartCoroutine(Fades());
    }
	
	IEnumerator Fades(){
		FadeToWhite();
		
		yield return new WaitForSeconds(2.5f);
		
		if(!inGameFade)
			FadeToBlack("MainMenu");
	}
    IEnumerator _FadeToWhite()
    {
        for (int i = 10; i > 0; i--)
        {
            yield return new WaitForSeconds(0.1f);
            rend.material.color = new Color(0, 0, 0, (i * 0.1f));
        }

        rend.material.color = new Color(0, 0, 0, 0);
    }

    IEnumerator _FadeToBlack(string levelToLoad)
    {
        for (int i = 0; i < 10; i++)
        {
            yield return new WaitForSeconds(0.1f);
			rend.material.color = new Color(0, 0, 0, (i * 0.1f));
        }
		if(opening)
		{
			if(Application.platform == RuntimePlatform.WindowsWebPlayer ||
			Application.platform == RuntimePlatform.OSXWebPlayer || 
			Application.platform == RuntimePlatform.OSXPlayer ||
			Application.platform == RuntimePlatform.OSXEditor ||
			Application.platform == RuntimePlatform.WindowsPlayer ||
			Application.platform == RuntimePlatform.WindowsEditor)
			{
				GoToLevel();
			}
			else
			{
				PlayVideo();
			}
		}
		else
		{
			GoToLevel(levelToLoad);
		}
		
		
		rend.material.color = new Color(0, 0, 0, 1);
		
    }

    public void FadeToWhite()
    {
        StartCoroutine(_FadeToWhite());
    }

    public void FadeToBlack(string level)
    {
        StartCoroutine(_FadeToBlack(level)); 
    }
	
	private void PlayVideo() 
	{
		IOSVideoPlayerBinding.instance.PlayVideo("Intro.mp4", 5, 5);
	}
	
	public void GoToLevel()
	{
        if (text)
		    text.enabled = true;

		image.SetActive(true);
		
		if(PlayerPrefs.GetInt("Save") == 0)
		{
            if(text)
			    text.text = "Loading Tutorial...";

            if (Application.isEditor)
            {
                Application.LoadLevel("MainMenu");
            }
            else
                Application.LoadLevel("Tutorial");
		}
		else
		{
        	Application.LoadLevel("MainMenu");
		}
		
	}
	public void GoToLevel(string level)
	{
        if (text)
		    text.enabled = true;
		if(PlayerPrefs.GetInt("Save") == 0)
		{
            if (text)
			    text.text = "Loading Tutorial...";
			Application.LoadLevel("Tutorial");
		}
		else
		{
        	Application.LoadLevel(level);
		}
		
	}
}
