using UnityEngine;
using System.Collections;

public class Boss1 : MonoBehaviour 
{
    public float health = 50;
    private Transform t;
    private float attackTimer;
    private float attackSpeed;
    private int mode = 0;

    void Start()
    {
        t = transform;
        attackSpeed = Random.Range(3, 5);
    }

    void Update()
    {
        if(Variables.instance.gameState == Variables.GameState.Game)
            BossMovement();
    }

    void BossMovement()
    {
        t.RotateAround(new Vector3(0, -5, 0), Vector3.forward, 40 * Time.deltaTime);

        Boss1Attack();
    }

    void Boss1Attack()
    {
        attackTimer += Time.deltaTime;

        if (attackTimer > attackSpeed)
        {
            if (mode == 0)
            {
                renderer.enabled = false;
                collider.enabled = false;
                mode = 1;

                attackTimer = 0;
                attackSpeed = Random.Range(2, 4);
            }
            else
            {
                renderer.enabled = true;
                collider.enabled = true;
                mode = 0;

                attackTimer = 0;
                attackSpeed = Random.Range(5, 6);
            }

           // Instantiate(Variables.instance.enemyFleetShip, t.position, t.rotation);
            //Instantiate(Variables.instance.enemyPlayerSeekerBullet, t.position, Quaternion.identity);
        }
    }

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

    void ChangeHealth(float amount)
    {
        if(mode == 0)
            health -= amount;

        if (health <= 0)
        {
            health = 0;
            // dead, play explosion
            Variables.instance.isBoss1Dead = true;
            Destroy(gameObject);
        }
    }
}
