using UnityEngine;
using System.Collections;

public class IAPButtons : MonoBehaviour {
	public enum InAppButtonList
	{
		FullVersion,
		Level1Coins,
		Level2Coins,
		Level3Coins,
		Level1Exp,
		Level2Exp,
		Level3Exp,
		DevPack,
		DevPackFree
	}
	public InAppButtonList iap;
	public InAppManager IAP;

	private Ray ray;
    //private RaycastHit hit;
	private float pressedTimer = 0;
	
	// make sure the level buttons tags are GoToGame 
	void OnMouseDown(){
		IAPButtonEvents();
	}
	
	void Update(){
		if(pressedTimer < 1)
			pressedTimer += Time.deltaTime;
		
		if (Input.touchCount > 0)
        {
			foreach (Touch touch in Input.touches)
			{
				//Touch touch = Input.GetTouch(0);k
	            ray = Camera.main.ScreenPointToRay(touch.position);
				RaycastHit hit;
				
	            if(collider.Raycast(ray, out hit, 10f)){
	                // handles all the single button press objects
	                if (touch.phase == TouchPhase.Began){
						if(pressedTimer > 0.3f)
							IAPButtonEvents();
					}
				}
			}
		}
	}
	
	
	void IAPButtonEvents(){
		Debug.Log("pressed: ");
		pressedTimer = 0;

#if UNITY_EDITOR || UNITY_WEBPLAYER || UNITY_STANDALONE
        Application.LoadLevel("MainMenu");
#endif

        #if UNITY_IPHONE
		switch(iap){
			case InAppButtonList.Level1Coins:
				#if UNITY_IPHONE
				IAP.BuyInAppPurchase(0); // 5
				#endif
				break;
			case InAppButtonList.Level2Coins:
				#if UNITY_IPHONE
			IAP.BuyInAppPurchase(1); // 3
				#endif
				break;
			case InAppButtonList.Level3Coins:
				#if UNITY_IPHONE
			IAP.BuyInAppPurchase(2); // 1
				#endif
				break;
			case InAppButtonList.Level1Exp:
				#if UNITY_IPHONE
			IAP.BuyInAppPurchase(4); // 6
				#endif
				break;
			case InAppButtonList.Level2Exp:
				#if UNITY_IPHONE
			IAP.BuyInAppPurchase(5); // 4
				#endif
				break;
			case InAppButtonList.Level3Exp:
				#if UNITY_IPHONE
			IAP.BuyInAppPurchase(6); // 2
				#endif
				break;
			case InAppButtonList.DevPack:
				#if UNITY_IPHONE
			IAP.BuyInAppPurchase(3); // 0
				#endif
				break;
		}
        #endif
	}
}
