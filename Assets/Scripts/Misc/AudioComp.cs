using UnityEngine;
using System.Collections;

public class AudioComp : MonoBehaviour 
{
    public bool loop;
    public bool sfx;
    public AudioClip audioSound;
    private float timeToDie;
	private AudioSource audioS;

	void Awake()
	{
		audioS = audio;
	}

    void OnEnable()
    {
        Activate();
    }

    void Update()
    {
        if(!loop)
            CountDown();
    }

    private void Activate()
    {
        if (sfx)
			audioS.volume = DontDestoryValues.instance.effectVolume;
        else
			audioS.volume = DontDestoryValues.instance.musicVolume;

		audioS.clip = audioSound;
		timeToDie = Time.time + audioS.clip.length;
		audioS.loop = loop;
		audioS.Play();
    }

    private void Deactivate()
    {
        //GameBoarder.cleanUpObjects.Add(gameObject);
        gameObject.SetActive(false);
    }

    private void CountDown()
    {
        if (timeToDie < Time.time)
        {
            Deactivate();
        }
    }
}
