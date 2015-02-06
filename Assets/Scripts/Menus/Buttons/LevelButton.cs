using UnityEngine;
using System.Collections;

public class LevelButton : MonoBehaviour 
{
	public int level;
	public bool isUnlocked;
	
	private Ray ray;
    //private RaycastHit hit;
	
	// make sure the level buttons tags are GoToGame 
	void OnMouseDown()
	{
		if(isUnlocked && !MainMenuManager.timerUpdating)
		{
			// make it so the current invasion start at the level
			DontDestoryValues.instance.startingLevel = level;
			MainMenuManager.instance.StartGame();
		}
	}
	
	void Update(){
		foreach (Touch touch in Input.touches)
        {
			//Touch touch = Input.GetTouch(0);
            ray = Camera.main.ScreenPointToRay(touch.position);
			RaycastHit hit;
            if(collider.Raycast(ray, out hit, 1000.0f))
            {
                // handles all the single button press objects
                if (touch.phase == TouchPhase.Began)
                {
					if(isUnlocked && !MainMenuManager.timerUpdating)
					{
						// make it so the current invasion start at the level
						DontDestoryValues.instance.startingLevel = level;
						MainMenuManager.instance.StartGame();
					}
				}
			}
		}
	}
}
