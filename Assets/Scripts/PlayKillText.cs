using UnityEngine;
using System.Collections;

public class PlayKillText : MonoBehaviour 
{
	public bool isCombo;
	private GUIText thisGT;
	public string text = "";
	// Use this for initialization
	private Transform t;
	private GameObject go;


	void Awake () {
		thisGT = gameObject.GetComponent<GUIText>();
		t = transform;
		go = gameObject;
	}
	
	void OnEnable()
	{
		thisGT.text = text;
		StartCoroutine(PlayAnimation());
	}
	
	private IEnumerator PlayAnimation()
	{
		float animationTime = 1.5f;
		
		/*if(isCombo)
			animationTime = thisGT.animation["ComboDropAnimation"].length;
		else
			animationTime = thisGT.animation["KillTextAnimation"].length;
		
		animation.Play();*/
		thisGT.material.color = new Color(Random.Range(0.2f, 1f), Random.Range(0.2f, 1f), Random.Range(0.2f, 1f), 1f);
		
		for(int i = 10; i > 0; i--)
		{
			thisGT.material.color = new Color(thisGT.material.color.r, thisGT.material.color.g, thisGT.material.color.b, (float)i * 0.1f);
			yield return new WaitForSeconds(animationTime / 10);
		}
		
		thisGT.material.color = new Color(thisGT.material.color.r, thisGT.material.color.g, thisGT.material.color.b, 0);
		go.SetActive(false);
	}

	void Update()
	{
		t.position += Vector3.up * 0.2f * Time.deltaTime;
	}
}
