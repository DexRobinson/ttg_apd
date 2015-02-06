using UnityEngine;
using System.Collections;

public class Mines : MonoBehaviour 
{
    private bool isRight;
    private Transform t;
    private float speed = 3.0f;

    void Start()
    {
        int random = Random.Range(0, 3);
        speed = Random.Range(3.0f, 5.0f);

        t = transform;
        if (random == 0)
            isRight = true;
        else
            isRight = false;
    }

    void FixedUpdate()
    {
        if (isRight)
        {
            t.Rotate(new Vector3(0, 0, 1.0f));
            t.RotateAround(new Vector3(0, -5, 0), Vector3.forward, speed * Time.deltaTime);
        }
        else
        {
            t.Rotate(new Vector3(0, 0, -1.0f));
            t.RotateAround(new Vector3(0, -5, 0), -Vector3.forward, speed * Time.deltaTime);
        }
    }

    void OnTriggerEnter(Collider coll)
    {
        if (coll.tag != "Enemy")
        {
            Physics.IgnoreCollision(gameObject.collider, coll.gameObject.collider);
        }
    }
}
