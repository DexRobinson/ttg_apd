using UnityEngine;
using System.Collections;

public class Tutorial : MonoBehaviour {
	public static Tutorial instance;
	
	public TextMesh textData;
	public TextMesh blackText;
	
	public Camera cameraToTurnOn;
	public GameObject[] objectsToRender;
	
	private Ray ray;
    //private RaycastHit hit;
	
	public int currentIndex = 0;
	private float pressTimer = 0f;

    private bool renderOnce; // used to hide the crate after it inits, so it doesn't keep reappearing after bein shot. 
	void Awake()
	{
		instance = this;
		GameController.ChangeControlType(2);
	}
	
	// Use this for initialization
	void Start () {
		UnRenderAll();
		textData.text = "Thanks for purchasing the \nACME Planetary Defense system.\nGuaranteed for the life of the \nplanet.\nTap continue to proceed to\nthe operator's manual.";
		Variables.instance.gameState = Variables.GameState.Tutorial;
		Time.timeScale = 1;
	}
	
	void OnMouseDown()
	{
		currentIndex++;
	}
	
	// Update is called once per frame
	void Update () {
		if(pressTimer < 0.5f)
			pressTimer += Time.deltaTime;
		
		Tut();
		blackText.text = textData.text;
		
		foreach (Touch touch in Input.touches)
        {
			//Touch touch = Input.GetTouch(0);
            ray = Camera.main.ScreenPointToRay(touch.position);
			RaycastHit hit;
            if(collider.Raycast(ray, out hit, 1000.0f))
            {
                // handles all the single button press objects
                if (touch.phase == TouchPhase.Ended){
					if(pressTimer > 0.4f)
					{
						pressTimer = 0f;
						currentIndex++;
					}
				}
			}
		}
	}
	
	void Tut()
	{
		if(currentIndex == 1)
		{
			cameraToTurnOn.enabled = true;
			textData.text = "The system's defense and \noffense are displayed by the \ngreen planet health, \norange fire recharge, \n and blue shield bars\n in the upper right corner.";
		}
		else if(currentIndex == 2)
		{
			cameraToTurnOn.enabled = true;
			if(Application.platform == RuntimePlatform.Android || Application.platform == RuntimePlatform.IPhonePlayer)
				textData.text = "Pull the joystick left or right\nfor planet rotation.\n(Other control options are\navailable from the pause\n menu)";
			else
				textData.text = "Use A & D to spin left or right.";//\nfor planet rotation.\n(Other control options are\navailable from the pause\n menu)";
				
		}
		else if(currentIndex == 3)
		{
			if(Application.platform == RuntimePlatform.Android || Application.platform == RuntimePlatform.IPhonePlayer)
				RenderIndex(0);
			cameraToTurnOn.enabled = false;
		}
		else if(currentIndex == 4)
		{
			UnRenderAll();
			cameraToTurnOn.enabled = true;
			if(Application.platform == RuntimePlatform.Android || Application.platform == RuntimePlatform.IPhonePlayer)
				textData.text = "Activation of the weapon \nsystem is obtained through \nthe use of the\nfire button.";
			else
				textData.text = "Activation of the weapon \nsystem is obtained through \nthe use of the\nfire button(Space Bar).";
		}
		else if(currentIndex == 5)
		{
			if(Application.platform == RuntimePlatform.Android || Application.platform == RuntimePlatform.IPhonePlayer)
				RenderIndex(1);
			cameraToTurnOn.enabled = false;
		}
		else if(currentIndex == 6)
		{
			UnRenderAll();
			cameraToTurnOn.enabled = true;
			RenderIndex(2);
			textData.text = "When encountering a large \nnumber of invaders the \nuse of the bomb button\nis highly recommended.";
		}
		else if(currentIndex == 7)
		{
			cameraToTurnOn.enabled = false;
		}
		else if(currentIndex == 8)
		{
			UnRenderAll();
			cameraToTurnOn.enabled = true;
			
			textData.text = "The ACME targeting system \ncomes complete with it's \nvery own targeting reticule.\nWe have supplied a \ncomplimentry target to \nannihilate at your convenince.";
		}
		else if(currentIndex == 9)
		{
			RenderIndex(3);
			cameraToTurnOn.enabled = false;
		}
		else if(currentIndex == 10)
		{
			UnRenderAll();
			cameraToTurnOn.enabled = true;
			textData.text = "Here at ACME we drop \n ship you all supplies via crates.\nShoot to activate.";
		}
		else if(currentIndex == 11)
		{
            if (!renderOnce)
            {
                renderOnce = true;
                RenderIndex(4);
            }
			cameraToTurnOn.enabled = false;
		}
		else if(currentIndex == 12)
		{
			UnRenderAll();
			cameraToTurnOn.enabled = true;
			
			textData.text = "For a temporary advantage\nACME engineers have developed a\n\"Super Weapon\"\nto aid in invader annihilation.\nFive of these super crates are \nneeded to fuel the weapon.";
		}
		else if(currentIndex == 13)
		{
			RenderIndex(5);
			cameraToTurnOn.enabled = false;
		}
		else if(currentIndex == 14)
		{
			UnRenderAll();
			cameraToTurnOn.enabled = true;
			
			textData.text = "During planetary evacuation \nmode each escape pod require \nfive fuel canisters in order\nto launch.";
		}
		else if(currentIndex == 15)
		{
			RenderIndex(6);
			cameraToTurnOn.enabled = false;
		}
		else if(currentIndex == 16)
		{
			UnRenderAll();
			cameraToTurnOn.enabled = true;
			textData.text = "After fuel has been collected\nthe launch button will indicate\nwith a red glow that\nan escape pod is ready\nto be launched.";
		}
		else if(currentIndex == 17)
		{
			RenderIndex(7);
			cameraToTurnOn.enabled = false;
		}
		else if(currentIndex == 18)
		{
			UnRenderAll();
			cameraToTurnOn.enabled = true;
            textData.text = "Here at ACME we take\npride in crafting the finest\nplanetary defense system and \nhope that continues to\ndefend planets for many\nyears to come!";
		}
		else if(currentIndex == 19)
		{
            DontDestoryValues.instance.GameMusicSwitch();
            Application.LoadLevel("PreLoader");

			//cameraToTurnOn.enabled = true;
			//textData.text = "Here at ACME we take\npride in crafting the finest\nplanetary defense system and \nhope that continues to\ndefend planets for many\nyears to come!";
		}
        //else if(currentIndex == 20)
        //{
        //    DontDestoryValues.instance.GameMusicSwitch();
        //    Application.LoadLevel("PreLoader");
        //}
	}
	
	void RenderIndex(int index)
	{
		for(int i = 0; i < objectsToRender.Length; i++)
		{
			if(i == index)
			{
				objectsToRender[i].SetActive(true);
			}
			else
			{
				objectsToRender[i].SetActive(false);
			}
		}
	}

    

	void UnRenderAll()
	{
		for(int i = 0; i < objectsToRender.Length; i++)
		{
			objectsToRender[i].SetActive(false);
		}
	}
	
	public IEnumerator UpdateIndex(int myId)
	{
		//Debug.Log("MY: " + myId);
		//Debug.Log("C: " + currentIndex);
		yield return new WaitForSeconds(3.0f);
		if(currentIndex == myId)
			currentIndex++;
	}
}
