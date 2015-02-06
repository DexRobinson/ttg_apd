using UnityEngine;

public class SpriteSheet : MonoBehaviour
{
    public float scrollSpeed = 0.5f;
    public float offset;
	private Renderer rend;

	void Awake()
	{
		rend = renderer;
	}

    void Update()
    {
        offset += (Time.deltaTime * scrollSpeed) / 10.0f;
        rend.material.SetTextureOffset("_MainTex", new Vector2(0, offset));
    }
}