using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameBoarder : MonoBehaviour 
{
    public static List<GameObject> cleanUpObjects = new List<GameObject>();

    void OnTriggerEnter(Collider coll)
    {
        if (coll.tag == "EscapePod")
        {
            Variables.instance.playerEscapePodsRecused += 1;
        }

        //ObjectPoolManager.DestroyPooled(coll.gameObject);
        //cleanUpObjects.Add(coll.gameObject);
        //coll.gameObject.active = false;
        //Destroy(coll.gameObject);
        //coll.gameObject.active = false;
        //ObjectPool.instance.PoolObject(coll.gameObject);
    }

    public static void CleanUp()
    {
        for (int i = 0; i < cleanUpObjects.Count; i++)
        {
            Destroy(cleanUpObjects[i].gameObject);
        }

        cleanUpObjects.Clear();
        cleanUpObjects = new List<GameObject>();
    }
}
