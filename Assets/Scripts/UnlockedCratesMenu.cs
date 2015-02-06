using UnityEngine;
using System.Collections;

public class UnlockedCratesMenu : MonoBehaviour {

	public GameController gameController;

	public ParticleSystem fireworks;
	public GameObject crate;
	public AnimationClip inAnimation;
	public AnimationClip outAnimation;

	public GUIText tapText;
	public GUIText unlockText;

	private bool isCrateIn;
	private float waitTimer;
	// used for font scaling...
	private bool isScalingUp;
	// orginal font size...
	private int orginalFontSize;

	void Start()
	{
		orginalFontSize = tapText.fontSize;
		tapText.enabled = false;
		unlockText.enabled = false;
	}

	public void AnimateIn(int textureId, Variables.CrateType crateType)
	{
		isCrateIn = true;

		unlockText.enabled = true;
		tapText.enabled = true;

		unlockText.text = "Unlocked: " + crateType.ToString() + "!";

		//unlock the new crate
		Variables.instance.UnlockUpgrades(textureId);
		fireworks.Play();

		crate.renderer.material.mainTexture = Resources.Load("Crate_" + crateType.ToString()) as Texture2D;
	
		crate.animation.clip = inAnimation;
		crate.animation.Play();
	}

	public void AnimateOut()
	{
		crate.animation.clip = outAnimation;
		crate.animation.Play();
	}



	private IEnumerator TapFontScale()
	{
		if(!isScalingUp)
		{
			tapText.fontSize += 1;
			if(tapText.fontSize > orginalFontSize + 5)
			{
				isScalingUp = true;
			}

			yield return new WaitForEndOfFrame();
		}
		else
		{
			tapText.fontSize -= 1;
			if(tapText.fontSize < orginalFontSize - 5)
			{
				isScalingUp = false;
			}

			yield return new WaitForEndOfFrame();
		}
	}

	void Reset()
	{
        DontDestoryValues.instance.TurnOnAudio();

		gameController.UpdateInvasionAfterUnlock();
		tapText.enabled = false;
		unlockText.enabled = false;
		isCrateIn = false;
		waitTimer = 0;
		AnimateOut();
	}

	void Update()
	{
		if(isCrateIn)
		{
			StartCoroutine(TapFontScale());

			waitTimer += Time.deltaTime;
			if(waitTimer > 3.0f)
			{
#if UNITY_IPHONE
				// handles all the controlls used inside of the game
				foreach (Touch touch in Input.touches)
				{
					// handles all the single button press objects
					if (touch.phase == TouchPhase.Ended)
					{
						Reset();
					}
				}
#endif
				if( Input.GetKeyUp(KeyCode.Return))
				{
					Reset();
				}
			}
		}
	}
}
