using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemySpawner : MonoBehaviour 
{
    public static EnemySpawner instance;
    public GameObject[] normalEnemySpawns;
    public GameObject[] rareEnemySpawns;
    public GameObject[] normalEnemies;
    public GameObject[] rareEnemies;
    public GameObject[] bosses;
	public GameObject[] miniBosses;
	
    private float normalEnemyTimer;
    private float rareEnemyTimer;
    private Variables v;
    private bool bossSpawned;
    //public List<GameObject> roundEnemies = new List<GameObject>();
    public List<GameObject> rareEnemiesList = new List<GameObject>();
	public Transform kSpawn;
	
    private GameObject[] r1Comet = new GameObject[4];
    private int numberOfr1Comets = 4;
    private GameObject[] r1Comet1 = new GameObject[4];
    private int numberOfr1Comets1 = 4;
	private GameObject[] r1Comet2 = new GameObject[4];
	private int numberOfr1Comets2 = 4;

    private GameObject[] r1Metor = new GameObject[4];
    private int numberOfr1Metors = 4;
    private GameObject[] r1Metor1 = new GameObject[4];
    private int numberOfr1Metors1 = 4;

    private GameObject[] r2Enemy = new GameObject[4];
    private int numberOfr2Enemies = 4;
    private GameObject[] r3Enemy = new GameObject[4];
    private int numberOfr3Enemies = 4;
    private GameObject[] r4Enemy = new GameObject[4];
    private int numberOfr4Enemies = 4;
    private GameObject[] r5Enemy = new GameObject[4];
    private int numberOfr5Enemies = 4;
    private GameObject[] r6Enemy = new GameObject[4];
    private int numberOfr6Enemies = 4;
    private GameObject[] r7Enemy = new GameObject[4];
    private int numberOfr7Enemies = 4;
    private GameObject[] r8Enemy = new GameObject[4];
    private int numberOfr8Enemies = 4;
	
	private float spawnMortTimer = 0.0f;
	//private int numberOfMortsSpawned = 0;
	private bool spawnMiniBoss;
	private int enemySpawnNumber = 2;
	private Transform t;

	private float rotSpeed = 10;

    void Init()
    {
        for (int i = 0; i < numberOfr1Comets; i++)
        {
            r1Comet[i] = Instantiate(normalEnemies[0]) as GameObject;
            r1Comet[i].transform.parent = t;
            r1Comet[i].SetActive(false);

            r1Comet1[i] = Instantiate(normalEnemies[1]) as GameObject;
            //r1Comet1[i].transform.parent = t;
            r1Comet1[i].SetActive(false);

			r1Comet2[i] = Instantiate(normalEnemies[2]) as GameObject;
            //r1Comet2[i].transform.parent = t;
			r1Comet2[i].SetActive(false);

            r1Metor[i] = Instantiate(normalEnemies[3]) as GameObject;
            //r1Metor[i].transform.parent = t;
            r1Metor[i].SetActive(false);

            r1Metor1[i] = Instantiate(normalEnemies[4]) as GameObject;
            //r1Metor1[i].transform.parent = t;
            r1Metor1[i].SetActive(false);

            r2Enemy[i] = Instantiate(normalEnemies[5]) as GameObject;
            //r2Enemy[i].transform.parent = t;
            r2Enemy[i].SetActive(false);

            r3Enemy[i] = Instantiate(normalEnemies[6]) as GameObject;
            //r3Enemy[i].transform.parent = t;
            r3Enemy[i].SetActive(false);

            r4Enemy[i] = Instantiate(normalEnemies[7]) as GameObject;
            //r4Enemy[i].transform.parent = t;
            r4Enemy[i].SetActive(false);

            r5Enemy[i] = Instantiate(normalEnemies[8]) as GameObject;
            //r5Enemy[i].transform.parent = t;
            r5Enemy[i].SetActive(false);

            r6Enemy[i] = Instantiate(normalEnemies[9]) as GameObject;
            //r6Enemy[i].transform.parent = t;
            r6Enemy[i].SetActive(false);

            r7Enemy[i] = Instantiate(normalEnemies[10]) as GameObject;
            //r7Enemy[i].transform.parent = t;
            r7Enemy[i].SetActive(false);

            r8Enemy[i] = Instantiate(normalEnemies[11]) as GameObject;
            //r8Enemy[i].transform.parent = t;
            r8Enemy[i].SetActive(false);
        }
    }

    void Awake()
    {
        instance = this;
		t = transform;

		Init();
		SpawnAllRareEnemies();
    }

    void Start()
    {
        v = Variables.instance;
		
		if(Variables.instance.currentInvasion > 1)
		{
			for(int i = 1; i < Variables.instance.currentInvasion; i++)
			{
				Variables.instance.numberOfEnemiesPerInvasion *= Variables.instance.enemyIncreasedAmount;
				Variables.instance.enemySpawnTimer *= Variables.instance.enemySpawnTimerAdjuster;
			}
			//Variables.instance.numberOfEnemiesPerInvasion = 1;
		}
		
		normalEnemySpawns = GameObject.FindGameObjectsWithTag("EnemySpawner");
		rareEnemySpawns = GameObject.FindGameObjectsWithTag("RareEnemySpawner");

        //Init();
        //SpawnAllRareEnemies();
        //SpawnAllEnemies();
    }

    void Update()
    {
        // updates to spawn enemies after a certain amount of seconds that changes every round
        if (v.gameState == Variables.GameState.Game)
        {
			t.RotateAround(new Vector3(0, -5, 0), -Vector3.forward, rotSpeed * Time.deltaTime);
			
            // normal game
            if (DontDestoryValues.instance.gameType == 0)
            {
                if (v.currentInvasion == 10)
                {
                    if(!bossSpawned)
                        StartCoroutine(SpawnBoss());
					
					if(DontDestoryValues.instance.planetNumber == 0)
					{
						//if(GameManager.boss1Alive && GameManager.boss1Spawned)
							//SpawnKamakizeMorts();
					}
                }
				else if(v.currentInvasion == 5)
				{
					if(!spawnMiniBoss)
					{
						spawnMiniBoss = true;
						Instantiate(miniBosses[DontDestoryValues.instance.planetNumber]);
					}
				}
                else
                {
                    if (v.numberOfEnemiesSpawned < v.numberOfEnemiesPerInvasion)
                    {
                        if (v.currentInvasion > 2)
                        {
                            rareEnemyTimer += Time.deltaTime;

                            if (rareEnemyTimer > v.rareEnemySpawnTimer)
                            {
                                SpawnRareEnemy();
                            }
                        }

                        normalEnemyTimer += Time.deltaTime;

                        if (normalEnemyTimer > (v.enemySpawnTimer - (DontDestoryValues.instance.planetNumber * 0.5f)))
                        {
                            SpawnNormalEnemy();
                        }
                    }
                }
            }

            // evac game
            else if (DontDestoryValues.instance.gameType == 1)
            {
                if (v.numberOfEnemiesSpawned > 10)
                {
					if(enemySpawnNumber < 10)
						enemySpawnNumber++;
					
					v.enemySpawnTimer *= 0.9f;
					v.numberOfEnemiesSpawned = 0;
				}
				
				if(enemySpawnNumber > 2)
                	rareEnemyTimer += Time.deltaTime;

                if (rareEnemyTimer > v.rareEnemySpawnTimer)
                {
                    SpawnRareEnemy();
                }

                normalEnemyTimer += Time.deltaTime;

                if (normalEnemyTimer > v.enemySpawnTimer)
                {
                    SpawnNormalEnemy();
                }
            }
        }
    }

    void SpawnNormalEnemy()
    {
        // increase the number of enemies spawned, and alive, spawns a random enemy at a random position, variables are used to check for end of round
        SpawnEnemys();
        
        normalEnemyTimer = 0;
    }

    void SpawnRareEnemy()
    {
        // increase the number of enemies alive, spawns a random enemy at a random position
        int randomEnemy = Random.Range(0, rareEnemies.Length);
		
		if(DontDestoryValues.instance.planetNumber == 0)
		{
			if(randomEnemy == 4)
				randomEnemy = 0;
		}
		if(DontDestoryValues.instance.planetNumber == 0 || DontDestoryValues.instance.planetNumber == 1)
		{
			if(randomEnemy == 5)
				randomEnemy = 0;
		}
		
		if(v.currentInvasion == 3)
		{
			randomEnemy = 0;
		}
		else if(v.currentInvasion == 4)
		{
			randomEnemy = 3;
		}
		else if(v.currentInvasion == 6)
		{
			randomEnemy = 2;
		}
		
        if (rareEnemiesList[randomEnemy].gameObject.activeSelf == false)
        {
            v.numberOfEnemiesSpawned++;
            v.numberOfEnemiesAlive++;
			
            rareEnemiesList[randomEnemy].gameObject.SetActive(true);
        }
		
        rareEnemyTimer = 0;
    }

    IEnumerator SpawnBoss()
    {
        bossSpawned = true;
        yield return new WaitForSeconds(5.0f);

        if (DontDestoryValues.instance.planetNumber == 0)
        {
            Instantiate(bosses[0], new  Vector3(0, 2.25f, 1), Quaternion.Euler(new Vector3(0, 0, 0)));
            //Instantiate(bosses[0], new Vector3(0, -13, 1), Quaternion.Euler(new Vector3(0, -180, 180)));
        }
        else if (DontDestoryValues.instance.planetNumber == 1)
        {
            Instantiate(bosses[1], new Vector3(-1.573363f, -4.596423f, 0), Quaternion.Euler(new Vector3(0, 180, 0)));
        }
        else if (DontDestoryValues.instance.planetNumber == 2)
        {
            Instantiate(bosses[2], new Vector3(0, 3, 1), Quaternion.Euler(new Vector3(0, -180, 0)));
            //Instantiate(bosses[2], new Vector3(0, -13, 1), Quaternion.Euler(new Vector3(0, -180, 180)));
        }
    }

    public void SpawnAllEnemies()
    {
        SpawnAllRareEnemies();
    }

    public void SpawnAllRareEnemies()
    {
        if (DontDestoryValues.instance.gameType == 1)
        {
            for (int i = 0; i < rareEnemies.Length; i++)
            {
                GameObject rareEnemy = Instantiate(rareEnemies[i]) as GameObject;
                rareEnemiesList.Add(rareEnemy);
                rareEnemy.SetActive(false);
            }
        }
        else
        {
            for (int i = 0; i < rareEnemies.Length; i++)
            {
                GameObject rareEnemy = Instantiate(rareEnemies[i]) as GameObject;
                rareEnemiesList.Add(rareEnemy);
                rareEnemy.SetActive(false);
            }
        }

        
    }
	
	void SpawnKamakizeMorts()
	{
		spawnMortTimer += Time.deltaTime;
		
		if(spawnMortTimer > 3.0f)
		{
			spawnMortTimer = 0.0f;
			GameManager.ActivateKamzkieMort(kSpawn.position);
		}
	}
	
	void SpawnEnemys()
	{
		StartCoroutine(_SpawnEnemys());
	}

    IEnumerator _SpawnEnemys()
    {
        int enemyToSpawn;
		int numberOfEnemiesToSpawn = Random.Range(2, 6);;
		rotSpeed = Random.Range(-1f, 1f);

		if(Variables.instance.numberOfEnemiesAlive <= 1)
		{
		for(int i = 0; i < numberOfEnemiesToSpawn; i++)
		{
			rotSpeed = Random.Range(-10f, 10f);

			normalEnemyTimer -= 2.5f;
			yield return new WaitForSeconds(1.7f);
	        if (DontDestoryValues.instance.gameType == 1)
	        {
	            enemyToSpawn = Random.Range(0, enemySpawnNumber);

	            if (enemyToSpawn > 10)
	                enemyToSpawn = 10;
	        }
	        else
	        {
				if(Variables.instance.currentInvasion > 7)
				{
					enemyToSpawn = Random.Range(0, normalEnemies.Length);
				}
				else
				{
					enemyToSpawn = Random.Range(0, 2 + Variables.instance.currentInvasion);
				}
	            
				
	            if (enemyToSpawn > 11)
	                enemyToSpawn = 1;
	        }
	        

	        switch (enemyToSpawn)
	        {
	            case 0:
	                ActivateR1Comet(normalEnemySpawns[Random.Range(0, normalEnemySpawns.Length - 1)].transform.position);
	                break;
	            case 1:
	                ActivateR1Comet1(normalEnemySpawns[Random.Range(0, normalEnemySpawns.Length - 1)].transform.position);
	                break;
				case 2:
					ActivateR1Comet2(normalEnemySpawns[Random.Range(0, normalEnemySpawns.Length - 1)].transform.position);
					break;
	            case 3:
	                ActivateR1Metor(normalEnemySpawns[Random.Range(0, normalEnemySpawns.Length - 1)].transform.position);
	                break;
	            case 4:
	                ActivateR1Metor1(normalEnemySpawns[Random.Range(0, normalEnemySpawns.Length - 1)].transform.position);
	                break;
	            case 5:
	                ActivateR2Enemy(normalEnemySpawns[Random.Range(0, normalEnemySpawns.Length - 1)].transform.position);
	                break;
	            case 6:
	                ActivateR3Enemy(normalEnemySpawns[Random.Range(0, normalEnemySpawns.Length - 1)].transform.position);
	                break;
	            case 7:
	                ActivateR4Enemy(normalEnemySpawns[Random.Range(0, normalEnemySpawns.Length - 1)].transform.position);
	                break;
	            case 8:
	                ActivateR5Enemy(normalEnemySpawns[Random.Range(0, normalEnemySpawns.Length - 1)].transform.position);
	                break;
	            case 9:
	                ActivateR6Enemy(normalEnemySpawns[Random.Range(0, normalEnemySpawns.Length - 1)].transform.position);
	                break;
	            case 10:
	                ActivateR7Enemy(normalEnemySpawns[Random.Range(0, normalEnemySpawns.Length - 1)].transform.position);
	                break;
	            case 11:
	                ActivateR8Enemy(normalEnemySpawns[Random.Range(0, normalEnemySpawns.Length - 1)].transform.position);
	                break;
	        }
		}
		}
    }

    void ActivateR1Comet(Vector3 position)
    {
        for (int i = 0; i < numberOfr1Comets; i++)
        {
            if (r1Comet[i].activeSelf == false)
            {
                r1Comet[i].SetActive(true);
                r1Comet[i].transform.position = position;
                v.numberOfEnemiesSpawned++;
                v.numberOfEnemiesAlive++;
                return;
            }
        }
    }

    void ActivateR1Comet1(Vector3 position)
    {
        for (int i = 0; i < numberOfr1Comets1; i++)
        {
            if (r1Comet1[i].activeSelf == false)
            {
                r1Comet1[i].SetActive(true);
                r1Comet1[i].transform.position = position;
                v.numberOfEnemiesSpawned++;
                v.numberOfEnemiesAlive++;
                return;
            }
        }
    }
	void ActivateR1Comet2(Vector3 position)
	{
		for (int i = 0; i < numberOfr1Comets2; i++)
		{
			if (r1Comet2[i].activeSelf == false)
			{
				r1Comet2[i].SetActive(true);
				r1Comet2[i].transform.position = position;
				v.numberOfEnemiesSpawned++;
				v.numberOfEnemiesAlive++;
				return;
			}
		}
	}
    void ActivateR1Metor(Vector3 position)
    {
        for (int i = 0; i < numberOfr1Metors; i++)
        {
            if (r1Metor[i].activeSelf == false)
            {
                r1Metor[i].SetActive(true);
                r1Metor[i].transform.position = position;
                v.numberOfEnemiesSpawned++;
                v.numberOfEnemiesAlive++;
                return;
            }
        }
    }

    void ActivateR1Metor1(Vector3 position)
    {
        for (int i = 0; i < numberOfr1Metors1; i++)
        {
            if (r1Metor1[i].activeSelf == false)
            {
                r1Metor1[i].SetActive(true);
                r1Metor1[i].transform.position = position;
                v.numberOfEnemiesSpawned++;
                v.numberOfEnemiesAlive++;
                return;
            }
        }
    }

    void ActivateR2Enemy(Vector3 position)
    {
        for (int i = 0; i < numberOfr2Enemies; i++)
        {
            if (r2Enemy[i].activeSelf == false)
            {
                r2Enemy[i].SetActive(true);
                r2Enemy[i].transform.position = position;
                v.numberOfEnemiesSpawned++;
                v.numberOfEnemiesAlive++;
                return;
            }
        }
    }

    void ActivateR3Enemy(Vector3 position)
    {
        for (int i = 0; i < numberOfr3Enemies; i++)
        {
            if (r3Enemy[i].activeSelf == false)
            {
                r3Enemy[i].SetActive(true);
                r3Enemy[i].transform.position = position;
                v.numberOfEnemiesSpawned++;
                v.numberOfEnemiesAlive++;
                return;
            }
        }
    }

    void ActivateR4Enemy(Vector3 position)
    {
        for (int i = 0; i < numberOfr4Enemies; i++)
        {
            if (r4Enemy[i].activeSelf == false)
            {
                r4Enemy[i].SetActive(true);
                r4Enemy[i].transform.position = position;
                v.numberOfEnemiesSpawned++;
                v.numberOfEnemiesAlive++;
                return;
            }
        }
    }

    void ActivateR5Enemy(Vector3 position)
    {
        for (int i = 0; i < numberOfr5Enemies; i++)
        {
            if (r5Enemy[i].activeSelf == false)
            {
                r5Enemy[i].SetActive(true);
                r5Enemy[i].transform.position = position;
                v.numberOfEnemiesSpawned++;
                v.numberOfEnemiesAlive++;
                return;
            }
        }
    }

    void ActivateR6Enemy(Vector3 position)
    {
        for (int i = 0; i < numberOfr6Enemies; i++)
        {
            if (r6Enemy[i].activeSelf == false)
            {
                r6Enemy[i].SetActive(true);
                r6Enemy[i].transform.position = position;
                v.numberOfEnemiesSpawned++;
                v.numberOfEnemiesAlive++;
                return;
            }
        }
    }

    void ActivateR7Enemy(Vector3 position)
    {
        for (int i = 0; i < numberOfr7Enemies; i++)
        {
            if (r7Enemy[i].activeSelf == false)
            {
                r7Enemy[i].SetActive(true);
                r7Enemy[i].transform.position = position;
                v.numberOfEnemiesSpawned++;
                v.numberOfEnemiesAlive++;
                return;
            }
        }
    }

    void ActivateR8Enemy(Vector3 position)
    {
        for (int i = 0; i < numberOfr8Enemies; i++)
        {
            if (r8Enemy[i].activeSelf == false)
            {
                r8Enemy[i].SetActive(true);
                r8Enemy[i].transform.position = position;
                v.numberOfEnemiesSpawned++;
                v.numberOfEnemiesAlive++;
                return;
            }
        }
    }
}
