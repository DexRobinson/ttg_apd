using UnityEngine;
using System.Collections;

public class Boss2 : MonoBehaviour 
{
    public float health = 15;
    public Boss2Arm lArm;
    public Boss2Arm rArm;

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
        if (lArm.armAlive == false && rArm.armAlive == false)
        {
            health -= damage;

            if (health < 1)
            {
                Destroy(gameObject);
            }
        }
    }
}
