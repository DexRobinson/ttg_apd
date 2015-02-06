using UnityEngine;
using System.Collections;

public class CratePowerWeapon : MonoBehaviour {
	public Texture2D[] crateTexture;
	
	private float life = 20.0f;
	private float timeToDie = 0.0f;
	private bool wasTutorial;
	private bool isDead;

    private ExplosionManager explosionManager;

	void Start()
	{
        explosionManager = GameObject.FindGameObjectWithTag("Variables").GetComponent<ExplosionManager>();
		//if(Application.platform == RuntimePlatform.WindowsEditor)
			//life = 5.0f;
		//renderer.material.mainTexture = crateTexture[DontDestoryValues.instance.planetNumber];
	}
	
 	void OnEnable(){
		
		isDead = false;
		SpawnInEffects();
		timeToDie = Time.time + life;
    }
	
	void OnBecameVisible(){
		gameObject.tag = "Enemy";
	}
	
	void FixedUpdate(){
		if(Variables.instance.gameState == Variables.GameState.Game)
		{
	        transform.Rotate(new Vector3(0.1f, 0.2f, 0.05f));
	        CountDown();
		}
		else if(Variables.instance.gameState == Variables.GameState.Tutorial)
		{
			if(wasTutorial)
			{
				wasTutorial = false;
				StartCoroutine(Tutorial.instance.UpdateIndex(13));
			}
		}
    }
	
 	void OnTriggerEnter(Collider coll){
        if (coll.tag == "Bullet" || coll.tag == "Laser" || coll.tag == "Seeker" || coll.tag == "BulletFlak" || coll.tag == "Bomb"){
           	explosionManager.ActivateGreenGlow(transform.position);
			PickUp();
			this.gameObject.SetActive(false);
            //Deactivate();
            
        }
        if (coll.tag == "CrateSeeker"){
            explosionManager.ActivateGreenGlow(transform.position);
			this.gameObject.SetActive(false);
			//Deactivate();
        }
    }
	
	private void CountDown(){
        if (timeToDie < Time.time){
			if(!isDead)
			{
				isDead = true;
            	Deactivate();
			}
        }
    }
	
	private void Deactivate(){
		SpawnOutEffect();
        //this.gameObject.SetActive(false);
    }
	
	private void PickUp (){
		if(Variables.instance.gameState == Variables.GameState.Tutorial)
		{
			wasTutorial = true;
			gameObject.collider.enabled = false;
			gameObject.renderer.enabled = false;
		}
		
		if(Variables.instance.numberOfPowerCrystals < 5)
			Variables.instance.numberOfPowerCrystals++;
	}
	
	void SpawnInEffects()
	{
		StartCoroutine(SpawnInCrateEffect());
	}
	void SpawnOutEffect()
	{
		gameObject.tag = "";
		StartCoroutine(SpawnOutCrateEffect());
	}
	
	IEnumerator SpawnInCrateEffect()
	{
		transform.localScale = (Vector3.one * 0.1f);
		
		for(int i = 0; i < 10; i++){
			//transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
			transform.localScale += (Vector3.one * 0.1f);
			yield return new WaitForSeconds(0.005f);
		}
		
		for(int i = 0; i < 10; i++){
			//transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
			transform.localScale -= (Vector3.one * 0.01f);
			yield return new WaitForSeconds(0.005f);
		}
	}
	IEnumerator SpawnOutCrateEffect()
	{
		for(int i = 0; i < 10; i++){
			//transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
			transform.localScale += (Vector3.one * 0.01f);
			yield return new WaitForSeconds(0.005f);
		}
		
		for(int i = 0; i < 10; i++){
			//transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
			transform.localScale -= (Vector3.one * 0.1f);
			yield return new WaitForSeconds(0.005f);
		}
		
		if(Variables.instance.gameState == Variables.GameState.Tutorial)
		{
			gameObject.collider.enabled = false;
			gameObject.renderer.enabled = false;
			wasTutorial = true;
		}
		else
		{
        	this.gameObject.SetActive(false);
		}
	}
}
