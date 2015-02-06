using UnityEngine;
using System.Collections;

public class Flak : MonoBehaviour {
    public float explodeTime = 1.5f;
    public SpriteRenderer flakBlob;
    public GameObject explosion;
    public SpriteRenderer yellowExplosion;
    public float growSpeed = 2.0f;
	public float bulletSpeed = 20.0f;

    private Transform t;
    private float explodeTimer = 0.0f;
    private float yellowAlpha = 1.0f;
    private Vector3 flakOrginalSize;
    private float speed = 1.0f;
	private GameObject go;
	private Rigidbody r;

    void Awake()
    {
        t = transform;
		go = gameObject;
		r = rigidbody;

        flakOrginalSize = flakBlob.transform.localScale;
    }

    public void Activate()
    {
        //speed = 1f;
		//Debug.Log("FF");
		go.SetActive(true);

		r.velocity = Vector3.zero;
		r.angularVelocity = Vector3.zero;
		t.rotation = Variables.playerGameObject.transform.rotation;
		t.position = Player.instance.singleBulletSpawn[Random.Range(0, Player.instance.singleBulletSpawn.Length)].position;

        flakBlob.transform.localScale = flakOrginalSize;
        flakBlob.enabled = true;
        
		yellowAlpha = 1.0f;
		yellowExplosion.color = new Color(1f, 1f, 1f, yellowAlpha);

        if (explosion.GetComponent<SpriteAnimation>())
        {
            explosion.GetComponent<SpriteAnimation>().m_currentFrame = 0;
        }

        explosion.GetComponent<SpriteRenderer>().enabled = false;
        explosion.GetComponent<SpriteAnimation>().enabled = false;
        explodeTimer = 0f;

        yellowExplosion.renderer.enabled = false;
        yellowExplosion.transform.localScale = Vector3.one * 0.5f;
    }

    void Update()
    {
        //transform.position += Vector3.up * speed * Time.deltaTime;
        explodeTimer += Time.deltaTime;
        if (explodeTimer > explodeTime)
        {
            if (!explosion.GetComponent<SpriteRenderer>().enabled)
            {
                speed = 0f;
				r.velocity = Vector3.zero;
				r.angularVelocity = Vector3.zero;
                flakBlob.enabled = false;

                explosion.GetComponent<SpriteRenderer>().enabled = true;
                explosion.GetComponent<SpriteAnimation>().enabled = true;
                
				yellowExplosion.gameObject.SetActive(true);
                yellowExplosion.renderer.enabled = true;
            }

            yellowExplosion.transform.localScale += Vector3.one * growSpeed * Time.deltaTime;
            yellowExplosion.color = new Color(1f, 1f, 1f, yellowAlpha);

            if (yellowExplosion.transform.localScale.x > 1.5)
            {
                yellowAlpha -= 2f * Time.deltaTime;
            }

            if (yellowAlpha <= 0.1f)
            {
                yellowExplosion.gameObject.SetActive(false);
            }

            if (explodeTimer > 5)
            {
                go.SetActive(false);
            }

        }
        else
        {
            flakBlob.transform.localScale += Vector3.one * 2 * Time.deltaTime;
			r.AddRelativeForce(Vector3.up * bulletSpeed * Time.deltaTime);
        }
    }
}
