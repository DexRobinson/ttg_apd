using UnityEngine;
using System.Collections;

public class StoreButtonIcon : MonoBehaviour 
{
	public int index;
	
	private Ray ray;
    //private RaycastHit hit;
	private bool touchBegan;
	private GameObject storeCam;
	
	void Start()
	{
		storeCam = GameObject.Find("CrateCamera");
	}
	/*
	void OnMouseDown()
	{
		if(MainMenuManager.instance.storeTouchIndex == -1)
			MainMenuManager.instance.storeTouchIndex = index;
	}
	
	void OnMouseUp()
	{
		if(MainMenuManager.instance.storeTouchIndex == index)
			ButtonDownEvents();
	}
	*/
	void Update()
	{
		if (Input.touchCount > 0)
        {
            foreach (Touch touch in Input.touches)
            {
                ray = storeCam.camera.ScreenPointToRay(touch.position);
				RaycastHit hit;
                if (collider.Raycast(ray, out hit, 1000.0f))
                {
                    if (touch.phase == TouchPhase.Began)
                    {
						touchBegan = true;
						MainMenuManager.instance.storeTouchIndex = index;
					}
					
					if(touch.phase == TouchPhase.Ended)
					{
						if(!MainMenuManager.timerUpdating && touchBegan && MainMenuManager.instance.storeTouchIndex == index)
						{
							touchBegan = false;
                        	ButtonDownEvents();
						}
                    }
                }
            }
        }
		
		if(Application.isEditor || Application.isWebPlayer)
		{
		 	ray = storeCam.camera.ScreenPointToRay(Input.mousePosition);
			RaycastHit hit;
	        if (collider.Raycast(ray, out hit, 1000.0f))
	        {
				if(Input.GetMouseButtonUp(0))
				{
					if(!MainMenuManager.timerUpdating && touchBegan && MainMenuManager.instance.storeTouchIndex == index)
					{
						touchBegan = false;
	                	ButtonDownEvents();
					}
				}
			
				if(Input.GetMouseButtonDown(0))
				{
					touchBegan = true;
					MainMenuManager.instance.storeTouchIndex = index;
				}
			
	        }
		}
	}
	
	void ButtonDownEvents()
	{
		MainMenuManager.instance.storeTouchIndex = -1;
		MainMenuManager.instance.ButtonDownEffect();
		Variables.instance.ChangeCrate(index);
	}
}
