using UnityEngine;
using System.Collections;

public class ExplosionManager : MonoBehaviour 
{
    public int numberOfExplosions = 3;
    private GameObject[] orangeExplosions = null;

    public  int numberOfFlakShells = 3;
    private  GameObject[] flakExplosionProjectile = null;
    private  GameObject[] flakShells = null;
	
	public  int numberOfFlakShellsPower = 3;
    private  GameObject[] flakExplosionProjectilePower = null;
	
    public  int numberOfMines = 16;
    private  GameObject[] mineProjectiles = null;

    public  int numberOfShockwaveExplosions = 5;
    private  GameObject[] shockwaveExplosions = null;

    public  int numberOfRedExplosions = 3;
    private  GameObject[] redExplosions = null;

     GameObject[] blueExplosions = null;
     int numberOfBlueExplosions = 3;
	
	 GameObject[] greenGlowExplosions = null;
     int numberOfGreenGlowExplosions = 3;
	 GameObject[] purpleGlowExplosions = null;
     int numberOfPurpleGlowExplosions = 3;
	
	 GameObject[] explosion1 = null;
     int numberOfExplosion1 = 3;
	 GameObject[] explosion2 = null;
     int numberOfExplosion2 = 3;
	 GameObject[] explosion5 = null;
     int numberOfExplosion5 = 3;
	 GameObject[] explosion7 = null;
     int numberOfExplosion7 = 3;
	 GameObject[] explosion9 = null;
     int numberOfExplosion9 = 3;
	
	 GameObject[] smoke = null;
	 int numOfSmoke = 5;
	
	 GameObject[] mortWarpIn = null;
	 int mortWarpInNum = 3;

    private Transform t;


    void Start()
    {
        t = transform;

        StartCoroutine(Init());
    }

    IEnumerator Init()
    {
		smoke = new GameObject[numOfSmoke];
		shockwaveExplosions = new GameObject[numberOfShockwaveExplosions];

		yield return new WaitForSeconds(1.0f);
		for (int i = 0; i < numOfSmoke; i++)
		{
			smoke[i] = Instantiate(Resources.Load("ExplosionSmoke")) as GameObject;
            smoke[i].transform.parent = t;
			smoke[i].SetActive(false);
		}

		for (int i = 0; i < numberOfShockwaveExplosions; i++)
		{
			shockwaveExplosions[i] = Instantiate(Resources.Load("ExplosionGlowBlue")) as GameObject;
            shockwaveExplosions[i].transform.parent = t;
			shockwaveExplosions[i].SetActive(false);
		}

		yield return new WaitForSeconds(8.0f);

		orangeExplosions = new GameObject[numberOfExplosions];
		flakExplosionProjectile = new GameObject[numberOfFlakShells];
		flakShells = new GameObject[numberOfFlakShells];
		flakExplosionProjectilePower = new GameObject[numberOfFlakShellsPower];
		mineProjectiles = new GameObject[numberOfMines];

		mortWarpIn = new GameObject[mortWarpInNum];
		

		greenGlowExplosions = new GameObject[numberOfGreenGlowExplosions];
		purpleGlowExplosions = new GameObject[numberOfPurpleGlowExplosions];
		
		redExplosions = new GameObject[numberOfRedExplosions];
		blueExplosions = new GameObject[numberOfBlueExplosions];
		explosion1 = new GameObject[numberOfExplosion1];
		explosion2 = new GameObject[numberOfExplosion2];
		explosion5 = new GameObject[numberOfExplosion5];
		explosion7 = new GameObject[numberOfExplosion7];
		explosion9 = new GameObject[numberOfExplosion9];

		yield return new WaitForSeconds(2.0f);

		for (int i = 0; i < mortWarpInNum; i++)
        {
            mortWarpIn[i] = Instantiate(Resources.Load("MortWarpIn")) as GameObject;
            mortWarpIn[i].transform.parent = t;
            mortWarpIn[i].SetActive(false);
        }
		

		
		for (int i = 0; i < numberOfPurpleGlowExplosions; i++)
        {
            purpleGlowExplosions[i] = Instantiate(Resources.Load("ExplosionGlowPurple")) as GameObject;
            purpleGlowExplosions[i].transform.parent = t;
            purpleGlowExplosions[i].SetActive(false);
        }
		
		for (int i = 0; i < numberOfGreenGlowExplosions; i++)
        {
            greenGlowExplosions[i] = Instantiate(Resources.Load("ExplosionGlowGreen")) as GameObject;
            greenGlowExplosions[i].transform.parent = t;
            greenGlowExplosions[i].SetActive(false);
        }
		
		for (int i = 0; i < numberOfExplosion9; i++)
        {
            explosion9[i] = Instantiate(Resources.Load("Explode09")) as GameObject;
            explosion9[i].transform.parent = t;
            explosion9[i].SetActive(false);
        }
		for (int i = 0; i < numberOfExplosion7; i++)
        {
            explosion7[i] = Instantiate(Resources.Load("Explode07")) as GameObject;
            explosion7[i].transform.parent = t;
            explosion7[i].SetActive(false);
        }
		for (int i = 0; i < numberOfExplosion5; i++)
        {
            explosion5[i] = Instantiate(Resources.Load("Explode05")) as GameObject;
            explosion5[i].transform.parent = t;
            explosion5[i].SetActive(false);
        }
		for (int i = 0; i < numberOfExplosion2; i++)
        {
            explosion2[i] = Instantiate(Resources.Load("Explode02")) as GameObject;
            explosion2[i].transform.parent = t;
            explosion2[i].SetActive(false);
        }
		for (int i = 0; i < numberOfExplosion1; i++)
        {
            explosion1[i] = Instantiate(Resources.Load("Explode01")) as GameObject;
            explosion1[i].transform.parent = t;
            explosion1[i].SetActive(false);
        }
		
        for (int i = 0; i < numberOfExplosions; i++)
        {
            orangeExplosions[i] = Instantiate(Resources.Load("ExplosionOrange")) as GameObject;
            orangeExplosions[i].transform.parent = t;
            orangeExplosions[i].SetActive(false);
        }

        for (int i = 0; i < numberOfFlakShells; i++)
        {
            flakExplosionProjectile[i] = Instantiate(Resources.Load("ExplosionFlak")) as GameObject;
            flakExplosionProjectile[i].transform.parent = t;
            flakExplosionProjectile[i].SetActive(false);

            flakShells[i] = Instantiate(Resources.Load("BulletFlakShell")) as GameObject;
            flakShells[i].transform.parent = t;
            flakShells[i].SetActive(false);
        }
		
		for(int i = 0; i < numberOfFlakShellsPower; i++)
		{
			flakExplosionProjectilePower[i] = Instantiate(Resources.Load("ExplosionFlakPower")) as GameObject;
            flakExplosionProjectilePower[i].transform.parent = t;
            flakExplosionProjectilePower[i].SetActive(false);
		}
		
        for (int i = 0; i < numberOfMines; i++)
        {
            mineProjectiles[i] = Instantiate(Resources.Load("Mine")) as GameObject;
            mineProjectiles[i].transform.parent = t;
            mineProjectiles[i].SetActive(false);
        }

        

        for (int i = 0; i < numberOfRedExplosions; i++)
        {
            redExplosions[i] = Instantiate(Resources.Load("ExplosionRed")) as GameObject;
            redExplosions[i].transform.parent = t;
            redExplosions[i].SetActive(false);
        }

        for (int i = 0; i < numberOfBlueExplosions; i++)
        {
            blueExplosions[i] = Instantiate(Resources.Load("ExplosionBlue")) as GameObject;
            blueExplosions[i].transform.parent = t;
            blueExplosions[i].SetActive(false);
        }
    }
	
	public  void ActivateMortWarpIn(Vector3 position)
    {
        for (int i = 0; i < mortWarpInNum; i++)
        {
            if (mortWarpIn[i].activeSelf == false)
            {
                mortWarpIn[i].transform.position = position;
                mortWarpIn[i].SetActive(true);
                //orangeExplosions[i].GetComponent<Explosion>().Activate();
                return;
            }
        }
    }
	
    public  void ActivateMine(Vector3 position)
    {
        for (int i = 0; i < numberOfMines; i++)
        {
            if (mineProjectiles[i].activeSelf == false)
            {
                mineProjectiles[i].transform.position = position;
                mineProjectiles[i].SetActive(true);
                //mineProjectiles[i].GetComponent<Explosion>().Activate();
                return;
            }
        }
    }
	
	public  void ActivateSmoke(Vector3 position)
    {
        for (int i = 0; i < numOfSmoke; i++)
        {
            if (smoke[i].activeSelf == false)
            {
                smoke[i].transform.position = position;
                smoke[i].SetActive(true);
                //orangeExplosions[i].GetComponent<Explosion>().Activate();
                return;
            }
        }
    }
	
    public  void ActivateOrangeExplosion(Vector3 position)
    {
        for (int i = 0; i < numberOfExplosions; i++)
        {
            if (orangeExplosions[i].activeSelf == false)
            {
                orangeExplosions[i].transform.position = position;
                orangeExplosions[i].SetActive(true);
                //orangeExplosions[i].GetComponent<Explosion>().Activate();
                return;
            }
        }
    }

    public  void ActivateFlakExplosion(Vector3 position)
    {
        for (int i = 0; i < numberOfFlakShells; i++)
        {
            if (flakExplosionProjectile[i].activeSelf == false)
            {
				//Debug.Log(i);
				
                //flakShells[i].transform.position = position;
                flakExplosionProjectile[i].transform.position = position;

                flakExplosionProjectile[i].SetActive(true);
                //flakExplosionProjectile[i].GetComponent<Explosion>().Activate();

                //flakShells[i].SetActive(true);
                //flakShells[i].GetComponent<Explosion>().Activate();

                return;
            }
        }
	}
		
	public  void ActivateFlakShell(Vector3 position)
	{
		for(int x = 0; x < numberOfFlakShells; x++)
		{
			if (flakShells[x].activeSelf == false)
            {
				//Debug.Log(x);
				flakShells[x].transform.position = position;
				flakShells[x].SetActive(true);
				
				return;
			}
		}
	}
	
	public  void ActivateFlakExplosionPower(Vector3 position)
    {
        for (int i = 0; i < numberOfFlakShellsPower; i++)
        {
            if (flakExplosionProjectilePower[i].activeSelf == false)
            {
				//Debug.Log(i);
				
                //flakShells[i].transform.position = position;
                flakExplosionProjectilePower[i].transform.position = position;

                flakExplosionProjectilePower[i].SetActive(true);
                //flakExplosionProjectile[i].GetComponent<Explosion>().Activate();

                //flakShells[i].SetActive(true);
                //flakShells[i].GetComponent<Explosion>().Activate();

                break;
            }
        }
		
		for(int x = 0; x < numberOfFlakShellsPower; x++)
		{
			if (flakShells[x].activeSelf == false)
            {
				//Debug.Log(x);
				flakShells[x].transform.position = position;
				flakShells[x].SetActive(true);
				
				return;
			}
		}
    }
	
    public  void ActivateShockwaveExplosion(Vector3 position)
    {
        for (int i = 0; i < numberOfShockwaveExplosions; i++)
        {
            if (shockwaveExplosions[i].activeSelf == false)
            {
                shockwaveExplosions[i].transform.position = position;
                shockwaveExplosions[i].SetActive(true);
                //shockwaveExplosions[i].GetComponent<Explosion>().Activate();
                return;
            }
        }
    }

    public  void ActivateRedExplosion(Vector3 position)
    {
        for (int i = 0; i < numberOfRedExplosions; i++)
        {
            if (redExplosions[i].activeSelf == false)
            {
                redExplosions[i].transform.position = position;
                redExplosions[i].SetActive(true);
                //redExplosions[i].GetComponent<Explosion>().Activate();
                return;
            }
        }
    }

    public  void ActivateBlueExplosion(Vector3 position)
    {
        for (int i = 0; i < numberOfBlueExplosions; i++)
        {
            if (blueExplosions[i].activeSelf == false)
            {
                blueExplosions[i].transform.position = position;
                blueExplosions[i].SetActive(true);
                //blueExplosions[i].GetComponent<Explosion>().Activate();
                return;
            }
        }
    }
	
	public  void ActivateExplosion1(Vector3 position)
    {
        for (int i = 0; i < numberOfExplosion1; i++)
        {
            if (explosion1[i].activeSelf == false)
            {
                explosion1[i].transform.position = position;
                explosion1[i].SetActive(true);
                //blueExplosions[i].GetComponent<Explosion>().Activate();
                return;
            }
        }
    }
	
	public  void ActivateExplosion2(Vector3 position)
    {
        for (int i = 0; i < numberOfExplosion2; i++)
        {
            if (explosion2[i].activeSelf == false)
            {
                explosion2[i].transform.position = position;
                explosion2[i].SetActive(true);
                //blueExplosions[i].GetComponent<Explosion>().Activate();
                return;
            }
        }
    }
	
	public  void ActivateExplosion5(Vector3 position)
    {
        for (int i = 0; i < numberOfExplosion5; i++)
        {
            if (explosion5[i].activeSelf == false)
            {
                explosion5[i].transform.position = position;
                explosion5[i].SetActive(true);
                //blueExplosions[i].GetComponent<Explosion>().Activate();
                return;
            }
        }
    }
	
	public  void ActivateExplosion7(Vector3 position)
    {
        for (int i = 0; i < numberOfExplosion7; i++)
        {
            if (explosion7[i].activeSelf == false)
            {
                explosion7[i].transform.position = position;
                explosion7[i].SetActive(true);
                //blueExplosions[i].GetComponent<Explosion>().Activate();
                return;
            }
        }
    }
	
	public  void ActivateExplosion9(Vector3 position)
    {
        for (int i = 0; i < numberOfExplosion9; i++)
        {
            if (explosion9[i].activeSelf == false)
            {
                explosion9[i].transform.position = position;
                explosion9[i].SetActive(true);
                //blueExplosions[i].GetComponent<Explosion>().Activate();
                return;
            }
        }
    }
	
	public  void ActivateGreenGlow(Vector3 position)
    {
        for (int i = 0; i < numberOfGreenGlowExplosions; i++)
        {
            if (greenGlowExplosions[i].activeSelf == false)
            {
                greenGlowExplosions[i].transform.position = position;
                greenGlowExplosions[i].SetActive(true);
                //blueExplosions[i].GetComponent<Explosion>().Activate();
                return;
            }
        }
    }
	
	public  void ActivatePurpleGlow(Vector3 position)
    {
        for (int i = 0; i < numberOfPurpleGlowExplosions; i++)
        {
            if (purpleGlowExplosions[i].activeSelf == false)
            {
                purpleGlowExplosions[i].transform.position = position;
                purpleGlowExplosions[i].SetActive(true);
                //blueExplosions[i].GetComponent<Explosion>().Activate();
                return;
            }
        }
    }
	
	public void ActivateRandomExplosion(Vector3 position)
	{
		int randomExplosion = Random.Range(0, 8);
		
		switch(randomExplosion)
		{
			case 0:
			ActivateOrangeExplosion(position);
			break;
			case 1:
			ActivateRedExplosion(position);
			break;
			case 2:
			ActivateBlueExplosion(position);
			break;
			case 3:
			ActivateExplosion1(position);
			break;
			case 4:
			ActivateExplosion2(position);
			break;
			case 5:
			ActivateExplosion5(position);
			break;
			case 6:
			ActivateExplosion7(position);
			break;
			case 7:
			ActivateExplosion9(position);
			break;
			
			default:
			ActivateExplosion1(position);
			break;
			}
	}
}
