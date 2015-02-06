using UnityEngine;
using System.Collections;

public class Boss3Colliders : MonoBehaviour 
{
    public float health = 9;
    public bool spawner;

    void OnTriggerEnter(Collider coll)
    {
        switch (coll.tag)
        {
            case "Bullet":
                health -= Variables.instance.playerBulletDamage;
                Variables.instance.numberOfHitsRound += 1;
                Variables.instance.numberOfHitsTotal += 1;

                Destroy(coll.gameObject);

                break;

            case "Laser":
                health -= Variables.instance.playerBulletDamage;
                Variables.instance.numberOfHitsRound += 1;
                Variables.instance.numberOfHitsTotal += 1;

                Destroy(coll.gameObject);
                break;

            case "Seeker":
                health -= Variables.instance.playerBulletDamage;
                Variables.instance.numberOfHitsRound += 1;
                Variables.instance.numberOfHitsTotal += 1;

                Destroy(coll.gameObject);
                break;

            case "Bomb":
                health -= 1.0f;
                break;
        }

        CheckHealth();
    }

    void CheckHealth()
    {
        if (health <= 0)
        {
            if (spawner)
            {
                Variables.instance.boss3SpawnersLeft--;
            }
            else
            {
                gameObject.collider.enabled = false;
                gameObject.renderer.enabled = false;
                StartCoroutine(Respawn());
            }

            
        }
    }

    IEnumerator Respawn()
    {
        yield return new WaitForSeconds(20.0f);
        gameObject.collider.enabled = true;
        gameObject.renderer.enabled = true;
        health = 9;
    }
}
