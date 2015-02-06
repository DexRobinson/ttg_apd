using UnityEngine;
using System.Collections;

public class PlasmaCrystal : MonoBehaviour 
{
	// Use this for initialization
	void OnEnable () {
        StartCoroutine(Disable());
	}

    IEnumerator Disable()
    {
        yield return new WaitForSeconds(10.0f);
        gameObject.SetActive(false);
    }
	 
	void OnTriggerEnter(Collider coll){
		
		if (coll.tag == "Bullet" || coll.tag == "Laser" || coll.tag == "Seeker" || coll.tag == "BulletFlak" || coll.tag == "Bomb")
        {
			if(Variables.instance.gameState == Variables.GameState.Tutorial)
			{
				if(Tutorial.instance.currentIndex == 15)
					Tutorial.instance.currentIndex++;
			}
			
			if(Variables.instance.playerPlasmaCrystals < 5)
				Variables.instance.playerPlasmaCrystals++;
			
			gameObject.SetActive(false);
		}
	}
}
