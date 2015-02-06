using UnityEngine;
using System.Collections;

public class DestroyOverTime : MonoBehaviour
{
    public float timer = 0.3f;
	// Use this for initialization
	IEnumerator Start () 
    {
        yield return new WaitForSeconds(timer);
	}
}
