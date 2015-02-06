using UnityEngine;
using System.Collections;

public class BuyGameButtons : MonoBehaviour {
	private Ray ray;
    private RaycastHit hit;
	
	void Update()
	{
		if (Input.GetMouseButtonUp(0))
        {
            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit))
            {
            	ButtonDownEvents();
            }
        }

        // handles all the input to the main menu objects
        if (Input.touchCount == 1)
        {
            foreach (Touch touch in Input.touches)
            {
                ray = Camera.main.ScreenPointToRay(touch.position);

                if (Physics.Raycast(ray, out hit))
                {
                    if (touch.phase == TouchPhase.Ended)
                    {
                    	ButtonDownEvents();
                    }
                }
            }
        }
	}
	
	void ButtonDownEvents()
	{
		switch (hit.collider.tag)
        {
			case "yesbuy":
				Debug.Log("yes");
				break;
			case "nobuy":
				Application.LoadLevel("MainMenu");
				break;
		}
	}
}
