using UnityEngine;
using System.Collections;

public class StatsBar : MonoBehaviour {
	private float maxFront = 1;
	private float maxMiddle = 1;
	private float currentFront = 1;
	private float currentMiddle = 1;

	public Transform frontTransform;
	public Transform middleTransform;
	public Transform background;

	private float maxX;

	void Start()
	{
		maxX = frontTransform.localScale.x;
	}

	void Update () 
	{
		if(maxFront == 0)
		{
			background.GetChild(0).renderer.enabled = false;
			frontTransform.GetChild(0).renderer.enabled = false;
			middleTransform.GetChild(0).renderer.enabled = false;
		}
		else
		{
			background.GetChild(0).renderer.enabled = true;
			frontTransform.GetChild(0).renderer.enabled = true;
			middleTransform.GetChild(0).renderer.enabled = true;

			frontTransform.localScale = Vector3.Lerp(frontTransform.localScale, new Vector3((currentFront / maxFront) * maxX, frontTransform.localScale.y, frontTransform.localScale.z), Time.deltaTime * 3.0f);
			middleTransform.localScale = Vector3.Lerp(middleTransform.localScale, new Vector3((currentMiddle / maxMiddle) * maxX, frontTransform.localScale.y, frontTransform.localScale.z), Time.deltaTime * 2.5f);
		}
	}

	public void SetStatsBar(float mMaxFront, float mMaxMiddle,
	                        float mCurrentFront, float mCurrentMiddle)
	{
		maxFront = mMaxFront;
		maxMiddle = mMaxMiddle;

		currentFront = mCurrentFront;
		currentMiddle = mCurrentMiddle;
	}
}
