using UnityEngine;
using System.Collections;

public class Boss2Arm : MonoBehaviour 
{
    public float health = 30.0f;
    public bool armAlive = true;

    void OnTriggerEnter(Collider coll)
    {
        switch (coll.tag)
        {
            case "Bullet":
                ChangeHealth(Variables.instance.playerBulletDamage);
                Variables.instance.numberOfHitsRound += 1;
                Variables.instance.numberOfHitsTotal += 1;

                Destroy(coll.gameObject);
                break;

            case "Laser":
                ChangeHealth(Variables.instance.playerBulletDamage);
                Variables.instance.numberOfHitsRound += 1;
                Variables.instance.numberOfHitsTotal += 1;
                break;

            case "Seeker":
                ChangeHealth(Variables.instance.playerBulletDamage);
                Variables.instance.numberOfHitsRound += 1;
                Variables.instance.numberOfHitsTotal += 1;

                Destroy(coll.gameObject);
                break;

            case "Bomb":
                ChangeHealth(10);
                Variables.instance.numberOfHitsRound += 1;
                Variables.instance.numberOfHitsTotal += 1;
                break;
        }
    }

    void ChangeHealth(float damage)
    {
        health -= damage;

        if (health < 1)
        {
            armAlive = false;
            renderer.enabled = false;
            collider.enabled = false;
        }
    }
}
