using UnityEngine;
using System.Collections;

public class Explosion : MonoBehaviour
{
    public float life = 2.0f;
    private float timeToDie;

    void Update()
    {
        if(life != 0)
            CountDown();
    }

    public void OnEnable()
    {
        timeToDie = Time.time + life;
    }

    public void Deactivate()
    {
        this.gameObject.SetActive(false);
    }

    private void CountDown()
    {
        if (timeToDie < Time.time)
        {
            Deactivate();
        }
    }
}
