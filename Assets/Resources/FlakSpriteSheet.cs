using UnityEngine;
using System.Collections;

public class FlakSpriteSheet : MonoBehaviour
{	
    public int _uvTieX = 1;
	public int _uvTieY = 1;
	public int _fps = 10;
 
	private Vector2 _size;
	private Renderer _myRenderer;
	private int _lastIndex = -1;
 	private float startTime = 0.0f;
	private bool playOnce;
	
	void Awake () 
	{
		_size = new Vector2 (1.0f / _uvTieX , 1.0f / _uvTieY);
		_myRenderer = renderer;
		if(_myRenderer == null)
			enabled = false;
		
		int uIndex = 1 % _uvTieX;
		int vIndex = 1 / _uvTieY;

		// build offset
		// v coordinate is the bottom of the image in opengl so we need to invert.
		Vector2 offset = new Vector2 (uIndex * _size.x, 1.0f - _size.y - vIndex * _size.y);

		_myRenderer.material.SetTextureOffset ("_MainTex", offset);
		_myRenderer.material.SetTextureScale ("_MainTex", _size);
	}
	
	void OnEnable(){
		playOnce = false;
		
		int uIndex = 1 % _uvTieX;
		int vIndex = 1 / _uvTieY;

		// build offset
		// v coordinate is the bottom of the image in opengl so we need to invert.
		Vector2 offset = new Vector2 (uIndex * _size.x, 1.0f - _size.y - vIndex * _size.y);

		_myRenderer.material.SetTextureOffset ("_MainTex", offset);
		_myRenderer.material.SetTextureScale ("_MainTex", _size);
		
		StartCoroutine(PlaySprite());
	}
	
	IEnumerator PlaySprite()
	{
		yield return new WaitForSeconds(0.65f);
		Debug.Log("F");
		playOnce = true;
		startTime = Time.time;
	}
	
	void Update(){
		if(playOnce){
			// Calculate index
			int index = (int)((Time.time - startTime) * _fps) % (_uvTieX * _uvTieY);
	    	if(index != _lastIndex)
			{
				// split into horizontal and vertical index
				int uIndex = index % _uvTieX;
				int vIndex = index / _uvTieY;
	 
				// build offset
				// v coordinate is the bottom of the image in opengl so we need to invert.
				Vector2 offset = new Vector2 (uIndex * _size.x, 1.0f - _size.y - vIndex * _size.y);
	 
				_myRenderer.material.SetTextureOffset ("_MainTex", offset);
				_myRenderer.material.SetTextureScale ("_MainTex", _size);
	 
				_lastIndex = index;
			}
		}
	}
}