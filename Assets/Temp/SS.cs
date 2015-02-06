using UnityEngine;
using System.Collections;

public class SS : MonoBehaviour {

	public int uvAnimationTileX = 24;
	public int uvAnimationTileY = 1;
	public float framesPerSecond = 10;


	private float index;
	private Vector2 size;
	private Vector2 offset;
	private float uIndex;
	private float vIndex;
	private Renderer rend;

	void Awake()
	{
		rend = renderer;
	}

	// Update is called once per frame
	void Update () {
		// Calculate index
	    index = Time.time * framesPerSecond;
	    // repeat when exhausting all frames
	    index = index % (uvAnimationTileX * uvAnimationTileY);
	    
	    // Size of every tile
	    size = new Vector2 (1.0f / uvAnimationTileX, 1.0f / uvAnimationTileY);
	    
	    // split into horizontal and vertical index
	    uIndex = index % uvAnimationTileX;
	    vIndex = index / uvAnimationTileX;
	
	    // build offset
	    // v coordinate is the bottom of the image in opengl so we need to invert.
	    offset = new Vector2 (uIndex * size.x, 1.0f - size.y - vIndex * size.y);
	    
	    rend.material.SetTextureOffset ("_MainTex", offset);
	    rend.material.SetTextureScale ("_MainTex", size);
	}
}
