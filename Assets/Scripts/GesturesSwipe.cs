using UnityEngine;
using System.Collections;

public class GesturesSwipe : MonoBehaviour
{
    private GameObject thisObject;
    private Vector3 startPosition = new Vector3();
    private Vector3 endPosition = new Vector3();
    private Vector3 currentPosition = new Vector3();
	private Vector3 oldPosition = new Vector3();
	
    private float y;
	private float z;
	
	public float targetPosition = 0;
	public float minTarget = -8;
	public float maxTarget = 8;
	public static bool isMoving;

	public int currentIndex;

    void Start()
    {
        thisObject = gameObject;
		y = thisObject.transform.position.y;
		z = thisObject.transform.position.z;
    }

	void OnSwipe(SwipeGesture gesture) 
	{ 
		if(MainMenuManager.instance.isInStore)
		{
			/* your code here */ 
			if(gesture.Direction == FingerGestures.SwipeDirection.Right || gesture.Direction == FingerGestures.SwipeDirection.Up)
			{
				currentIndex++;
				MainMenuManager.instance.SpawnCrateStore(false);
			}
			else if(gesture.Direction == FingerGestures.SwipeDirection.Left || gesture.Direction == FingerGestures.SwipeDirection.Down)
			{
				currentIndex--;
				MainMenuManager.instance.SpawnCrateStore(true);
			}
			if(currentIndex < 0)
				currentIndex = Variables.instance.upgradeCrateTextures.Length - 1;
			else if(currentIndex > Variables.instance.upgradeCrateTextures.Length - 1)
				currentIndex = 0;

			Variables.instance.ChangeCrate(currentIndex);
		}
	}

	void Update()
	{
		if(!MainMenuManager.instance.isInStore)
		{
			currentIndex = 0;
		}
	}
	/*
    void Update()
    {
		/
        if (Input.GetMouseButtonDown(0))
        {
            if (Input.mousePosition.y < Screen.height * 0.75 && Input.mousePosition.y > Screen.height * 0.05f)
                startPosition = Input.mousePosition * 0.5f;
        }

        if (startPosition != Vector3.zero)
        {
            endPosition = Input.mousePosition * 0.5f;
            currentPosition = endPosition - startPosition;
			
			if(currentPosition != oldPosition){
				if(thisObject.transform.position.x >= minTarget && thisObject.transform.position.x <= maxTarget){
	            	thisObject.transform.position += new Vector3((currentPosition.x / Screen.width) * 5.75f, 0, 0);
					oldPosition = currentPosition;
				}
			}
			
			startPosition = Input.mousePosition * 0.5f;
			
            if (Input.GetMouseButtonUp(0))
            {
				targetPosition = Mathf.Round(thisObject.transform.position.x);
				
                startPosition = Vector3.zero;
                endPosition = Vector3.zero;
                currentPosition = Vector3.zero;
				
            }
        }
        else
        {
			if(targetPosition < minTarget)
				targetPosition = minTarget;
			else if(targetPosition > maxTarget)
				targetPosition = maxTarget;
			
            if (Vector3.Distance(thisObject.transform.position, new Vector3(targetPosition, y, z)) < 0.01f)
            {
                thisObject.transform.position = new Vector3(targetPosition, y, z);
				isMoving = false;
            }
			else
			{
            	thisObject.transform.position = Vector3.Lerp(thisObject.transform.position, new Vector3(targetPosition, y, z), Time.deltaTime * 2f);
			}
        }
			*/
		/*
        int touchCount = Input.touchCount;
        if (touchCount == 1)
        {
            Touch touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Began)
            {
				isMoving = false;
				
                if (touch.position.y < Screen.height * 0.75 && touch.position.y > Screen.height * 0.05f)
                    startPosition = touch.position * 0.5f;
            }
			
			if(touch.phase == TouchPhase.Moved)
			{
				isMoving = true;
			}

            if (startPosition != Vector3.zero)
            {
                endPosition = touch.position * 0.5f;
                currentPosition = endPosition - startPosition;
				thisObject.transform.position += new Vector3((currentPosition.x / Screen.width) * 0.75f, 0, 0);
				
                if (touch.phase == TouchPhase.Ended)
                {
                   	if (currentPosition.x > 10)
	                {
	                    targetPosition += (int)(currentPosition.x / 60);
	                }
	                else if (currentPosition.x < -10)
	                {
	                    targetPosition -= (int)(currentPosition.x / -60);
	                }
	                else if (endPosition.x > (Screen.width * 0.5f) * 0.85f)
	                {
	                    targetPosition--;
	                }
	                else if (endPosition.x < (Screen.width * 0.5f) * 0.15f)
	                {
	                    targetPosition++;
	                }
					
	                startPosition = Vector3.zero;
	                endPosition = Vector3.zero;
	                currentPosition = Vector3.zero;
                }
            }
        }
        else
        {
			if(targetPosition < minTarget)
				targetPosition = minTarget;
			else if(targetPosition > maxTarget)
				targetPosition = maxTarget;
			
            if (Vector3.Distance(thisObject.transform.position, new Vector3(targetPosition, y, z)) < 0.05f)
            {
                thisObject.transform.position = new Vector3(targetPosition, y, z);
				isMoving = false;
            }
			else
			{
            	thisObject.transform.position = Vector3.Lerp(thisObject.transform.position, new Vector3(targetPosition, y, z), Time.deltaTime * 2f);
			}
        }*/
    //}
}
