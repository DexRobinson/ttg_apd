using UnityEngine;
using System.Collections;

public class PlaySpriteOnce : MonoBehaviour 
{
    public int colcount;
    public int rowcount;

    private bool readyToFire;
    private bool _playAnimation;

    private int index;
    private Vector2 offset;
    private float timer;
    private int totalcells = 8;
    //private bool playAnimation = true;
    private int rownum = 0;
    private int colnum = 0;
	private float delayTimer = 0.0f;
	private bool isWaiting;

	private Vector2 size;
	private float uIndex;
	private float vIndex;
	private Renderer rend;

    void Start()
    {
        totalcells = colcount * rowcount;
        //Debug.Log(totalcells);
		rend = renderer;
    }

    void Update()
    {
		if(isWaiting)
		{
			delayTimer += Time.deltaTime;
			if(delayTimer > 3.0f)
			{
				isWaiting = false;
				delayTimer = 0.0f;
			}
		}
		
        if (_playAnimation && !isWaiting)
        {
            RunAnimation(colcount, rowcount, rownum, colnum, totalcells, 24);
        }
    }

    private void RunAnimation(int colCount, int rowCount, int rowNumber, int colNumber, int totalCells, int fps)
    {
        if (Time.timeScale == 1)
        {
            timer += Time.deltaTime;
            index = (int)(timer * (float)fps);

            // Repeat when exhausting all cells
            index = index % totalCells;

            // Size of every cell
            size = new Vector2(1.0f / colCount, 1.0f / rowCount);

            // split into horizontal and vertical index

            uIndex = index % colCount;
            vIndex = index / colCount;

            // build offset
            // v coordinate is the bottom of the image in opengl so we need to invert.
            offset = new Vector2((uIndex + colNumber) * size.x, (1.0f - size.y) - (vIndex + rowNumber) * size.y);

            rend.material.SetTextureOffset("_MainTex", offset);
            rend.material.SetTextureScale("_MainTex", size);

            /*if (index >= (totalcells - 1))
            {
                Debug.Log("Ready to fire");
                readyToFire = true;
                index = 0;
                timer = 0;
            }*/
			
			if (index >= (totalcells / 2 + 2))
            {
                //Debug.Log("Ready to fire");
                readyToFire = true;
                //index = 0;
                //timer = 0;
            }
			
			if (index >= (totalcells - 1))
            {
				isWaiting = true;
				readyToFire = false;
        		_playAnimation = false;
        		index = 0;
			}
        }
    }

    public bool ReadyToFire
    {
        get { return readyToFire; }
        set { readyToFire = value; }
    }

    public bool PlayAnimaion
    {
        get { return _playAnimation; }
        set { _playAnimation = value; }
    }

    public void PlayFireAnimation()
    {
        readyToFire = false;
        _playAnimation = true;
        index = 0;
    }
}
