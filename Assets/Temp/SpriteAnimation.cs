using UnityEngine;
using System.Collections;

[RequireComponent(typeof(SpriteRenderer))]
public class SpriteAnimation : MonoBehaviour
{
    public bool isPlaying
    {
        get { return m_playing; }
    }

    [SerializeField]
    private Sprite[] m_frames;          // User defined frames

    [SerializeField]
    private float m_fps;            // User defined framerate

    [SerializeField]
    private bool b_loop;      // Should we play animation automatically on awake?

    [SerializeField]
    private bool m_playOnAwake;      // Should we play animation automatically on awake?

    private float m_frameTime;         // Cached target frame time

    public int m_currentFrame;   // Current frame index
    private float m_timer;          // Current frame time

    private bool m_playing = false;       // Is currently playing?

    private SpriteRenderer m_renderer;

    public void Play()
    {
        if (m_fps <= 0)
        {
            Debug.LogError("[ASSERT] SpriteAnimation frame rate must be positive non-zero");
            return;
        }

        m_frameTime = 1.0f / m_fps;      // DOH! time is in seconds, not milis :)

        m_playing = true;
        SetFrame(0);
    }

    void Update()
    {
        if (m_playing)
        {
            m_timer += Time.deltaTime;

            if (m_timer > m_frameTime)
            {
                GetNextFrame();
            }
        }
    }

    void Awake()
    {
        m_renderer = renderer as SpriteRenderer;  // Cache sprite renderer

        if (m_playOnAwake)
        {
            Play();
        }
    }

    private void GetNextFrame()
    {
        m_currentFrame++;

        if (m_currentFrame < m_frames.Length)
        {
            SetFrame(m_currentFrame);
            return;
        }
        else
        {
            if (b_loop)
            {
                m_currentFrame = 0;
            }
            else
            {
                enabled = false;
            }
        }
        //m_playing = false;
    }

    public void SetFrame( int frame )
    {
        m_currentFrame = frame;
        m_timer = 0;

        m_renderer.sprite = m_frames[frame];
    }
}