using UnityEngine;
using System.Collections;

public class GameButton : MonoBehaviour {
	public enum ButtonType
	{
		Left,
		Right,
		Fire
	}
	public ButtonType type;
	public Player player;
	public GameController gameController;

	private Ray ray;
	private RaycastHit hit;
	private bool runOnce;

	void OnMouseDown()
	{
		ButtonDownEvent();
	}

	void OnMouseUp()
	{
		ButtonUpEvent();
	}

	void Update () 
	{
		// handles all the controlls used inside of the game
		foreach (Touch touch in Input.touches)
		{
			//Touch touch = Input.GetTouch(0);
			ray = Camera.main.ScreenPointToRay(touch.position);

			if(Physics.Raycast(ray, out hit))
			{
				if(hit.collider == this.collider)
				{
					/*if(type == ButtonType.Fire)
					{
						if(touch.phase == TouchPhase.Stationary || touch.phase == TouchPhase.Began)
						{
							ButtonDownEvent();
						}

						if(touch.phase == TouchPhase.Ended || touch.phase == TouchPhase.Canceled)
						{
							ButtonUpEvent();
						}
					}
					else
					{*/
						// handles all the single button press objects
						//if (touch.phase == TouchPhase.Ended || touch.phase == TouchPhase.Canceled || touch.phase == TouchPhase.Moved)
						//{
						//	ButtonUpEvent();
						//}

						if(touch.phase == TouchPhase.Began || touch.phase == TouchPhase.Stationary)
						{
							runOnce = true;
							ButtonDownEvent();
						}
					//}
				}
				else
				{

				}
			}

			if (touch.phase == TouchPhase.Ended || touch.phase == TouchPhase.Canceled)
			{
				if(runOnce)
				{
					ButtonUpEvent();
					runOnce = false;
				}
			}
		}
	}

	void ButtonDownEvent()
	{
		switch(type)
		{
		case ButtonType.Fire:
			//player.Fire();
			//gameController.isFiring = true;
			gameController.FireInput();
			break;
		case ButtonType.Left:
			gameController.isMovingLeft = true;
			//player.MoveLeftEase();
			break;
		case ButtonType.Right:
			//player.MoveRightEase();
			gameController.isMovingRight = true;
			break;
		}
	}

	void ButtonUpEvent()
	{
		switch(type)
		{
		case ButtonType.Fire:
			gameController.FireDone();
			//gameController.isFiring = false;
			break;
		case ButtonType.Left:
			gameController.isMovingLeft = false;
			break;
		case ButtonType.Right:
			gameController.isMovingRight = false;
			break;
		}
	}
}
