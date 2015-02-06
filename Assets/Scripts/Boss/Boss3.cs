using UnityEngine;
using System.Collections;

public class Boss3 : MonoBehaviour 
{
    public Transform shieldGroup;
    public Transform spawnerGroup;
    public GameObject spawnedObject;
    public Transform[] spawners;
    public float shrinkAmount;   // this is used for special enemies to come in closer to the planet and smash into it
    public float shrinkTimer;    // how long before it moves into the planet

    //private GameObject target;                 // player
    private float attackTimer;
    private float attackSpeed;
    //private float shrinkOrginalAmount;

    void Start()
    {
        attackSpeed = Random.Range(5, 7);

        //shrinkOrginalAmount = shrinkAmount;
        //target = GameObject.FindGameObjectWithTag("Player");
    }

    void Update()
    {
        if (Variables.instance.gameState == Variables.GameState.Game)
            BossMovement();
    }

    void BossMovement()
    {
        if (shrinkTimer > 0)
        {
            if (Time.time > shrinkTimer)
            {
                shieldGroup.transform.localScale -= new Vector3(shrinkAmount, shrinkAmount, shrinkAmount);
                shrinkTimer += 5.0f;
            }
        }

        shieldGroup.RotateAround(new Vector3(0, -5, 0), Vector3.forward, 40 * Time.deltaTime);
        spawnerGroup.RotateAround(new Vector3(0, -5, 0), Vector3.forward, 20 * Time.deltaTime);

        StartCoroutine(Boss3Attack());
    }

    IEnumerator Boss3Attack()
    {
        // boss will unleash 1-4 enemies at a time that the user will try and kill before they reach the planet
        attackTimer += Time.deltaTime;

        if (attackTimer > attackSpeed)
        {
            attackTimer = 0;
            attackSpeed = Random.Range(3, 7);

            //int randomSpawner = Random.Range(0, spawners.Length - 1);
            int numberOfSpawns = Random.Range(0, spawners.Length - 1);
            for (int i = 0; i < numberOfSpawns; i++)
            {
                Instantiate(spawnedObject, spawners[i].position, spawnerGroup.rotation);
                yield return new WaitForSeconds(Random.Range(0.5f, 1.5f));
            }

            //Instantiate(spawnedObject, spawners[randomSpawner].position, spawnerGroup.rotation);
        }
    }
}
