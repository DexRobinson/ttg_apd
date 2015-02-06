using UnityEngine;
using System.Collections;

public class JoystickScript : MonoBehaviour
{

    // A simple class for bounding how far the GUITexture will move
    class Boundary
    {
        public Vector2 min = Vector2.zero;
        public Vector2 max = Vector2.zero;
    }

    public static JoystickScript instance;

    static private JoystickScript[] joysticks;				// A static collection of all joysticks
    static private bool enumeratedJoysticks = false;
    static private float tapTimeDelta = 0.3f;				// Time allowed between taps

    public bool lockY;                                      // Lock the Y position for the joystick
	public GameObject joystickBackground;
    public bool touchPad; 									// Is this a TouchPad?
    public Rect touchZone;
    public Vector2 deadZone = Vector2.zero;				    // Control when position is output
    public bool normalize = false; 							// Normalize output after the dead-zone?
    public Vector2 position; 								// [-1, 1] in x,y
    public int tapCount;									// Current tap count

    public bool maxTilt = false;

    private int lastFingerId = -1;							// Finger last used for this joystick
    private float tapTimeWindow;							// How much time there is left for a tap to occur
    private Vector2 fingerDownPos;
    private float fingerDownTime;
    private float firstDeltaTime = 0.5f;

    private GUITexture gui;								    // Joystick graphic
    private Rect originalRect;                              // original rect of pixel inset before any resizing logic is applied
    private Rect defaultRect;								// Default position / extents of the joystick graphic
    private Boundary guiBoundary = new Boundary();			// Boundary for joystick graphic
    private Vector2 guiTouchOffset;						    // Offset to apply to touch input
    private Vector2 guiCenter;							    // Center of joystick

    private Resolution prevResolution;
    private Resolution currResolution;

	void Start()
    {
        instance = this;

        // Cache this component at startup instead of looking up every frame	
        gui = GetComponent<GUITexture>();

        originalRect = gui.pixelInset;

        prevResolution = Screen.currentResolution;
        currResolution = Screen.currentResolution;

        UpdateValues();
	}
	
	void OnMouseDown()
	{
		if(Variables.instance.gameState == Variables.GameState.Tutorial)
		{
			StartCoroutine(Tutorial.instance.UpdateIndex(3));
		}
	}
	
    public void Disable()
    {
        gameObject.SetActive(false);
        enumeratedJoysticks = false;
    }

    void ResetJoystick()
    {
        // Release the finger control and set the joystick back to the default position
        gui.pixelInset = defaultRect;
        lastFingerId = -1;
		
		if(position.x != 0)
		{
			Player.instance.joystickPositionX = position.x;
		}
		//Player.instance.joystickBeingPressed = false;
        position = Vector2.zero;
		
        fingerDownPos = Vector2.zero;
		
        if (touchPad)
            gui.color = new Color (gui.color.r, gui.color.g, gui.color.b, 0.025f);
    }

    public bool IsFingerDown()
    {
        return (lastFingerId != -1);
    }

    void LatchedFinger(int fingerId)
    {
        // If another joystick has latched this finger, then we must release it
        if (lastFingerId == fingerId)
            ResetJoystick();
    }

    void Update()
    {
		if(GameController.currentControlType == 2 && Variables.instance.playerCurrentHealth > 0)
		{
			if(!guiTexture.enabled)
			{
				guiTexture.enabled = true;
				joystickBackground.guiTexture.enabled = true;
			}
			
	        currResolution = Screen.currentResolution;
	
	        if (currResolution.width != prevResolution.width || currResolution.height != prevResolution.height)
	        {
	            UpdateValues();
	        }
	
	        if (!enumeratedJoysticks)
	        {
	            // Collect all joysticks in the game, so we can relay finger latching messages
	            joysticks = FindObjectsOfType(typeof(JoystickScript)) as JoystickScript[];
	            enumeratedJoysticks = true;
	        }
	
	        var count = Input.touchCount;
			
			
	        // Adjust the tap time window while it still available
	        if (tapTimeWindow > 0)
	            tapTimeWindow -= Time.deltaTime;
	        else
	            tapCount = 0;
	
	        if (count == 0 || Variables.instance.gameState == Variables.GameState.GameOver || Variables.instance.gameState == Variables.GameState.Pause)
	            ResetJoystick();
	        else
	        {
	            for (int i = 0; i < count; i++)
	            {
					Player.instance.joystickBeingPressed = true;
					
	                Touch touch = Input.GetTouch(i);
	                Vector2 guiTouchPos = new Vector2(touch.position.x - guiTouchOffset.x - transform.position.x * Screen.width, touch.position.y - guiTouchOffset.y - transform.position.y * Screen.height);
					
					if(Variables.instance.gameState == Variables.GameState.Tutorial)
					{
						if(count > 0 && touch.position.x > 0.5f && Tutorial.instance.currentIndex > 2)
							StartCoroutine(Tutorial.instance.UpdateIndex(3));
					}
					
	                var shouldLatchFinger = false;
	                if (touchPad)
	                {
	                    if (touchZone.Contains(touch.position))
	                        shouldLatchFinger = true;
	                }
	                else if (gui.HitTest(touch.position))
	                {
	                    shouldLatchFinger = true;
	                }
	
	                // Latch the finger if this is a new touch
	                if (shouldLatchFinger && (lastFingerId == -1 || lastFingerId != touch.fingerId))
	                {
	
	                    if (touchPad)
	                    {
	                        gui.color = new Color(gui.color.r, gui.color.g, gui.color.b, 0.15f);
	
	                        lastFingerId = touch.fingerId;
	                        fingerDownPos = touch.position;
	                        fingerDownTime = Time.time;
	                    }
	
	                    lastFingerId = touch.fingerId;
	
	                    // Accumulate taps if it is within the time window
	                    if (tapTimeWindow > 0)
	                        tapCount++;
	                    else
	                    {
	                        tapCount = 1;
	                        tapTimeWindow = tapTimeDelta;
	                    }
	
	                    // Tell other joysticks we've latched this finger
	                    foreach (JoystickScript j in joysticks)
	                    {
	                        if (j != this)
	                            j.LatchedFinger(touch.fingerId);
	                    }
	                }
	
	                if (lastFingerId == touch.fingerId)
	                {
	                    // Override the tap count with what the iPhone SDK reports if it is greater
	                    // This is a workaround, since the iPhone SDK does not currently track taps
	                    // for multiple touches
	                    if (touch.tapCount > tapCount)
	                        tapCount = touch.tapCount;
	
	                    if (touchPad)
	                    {
	                        // For a touchpad, let's just set the position directly based on distance from initial touchdown
	                        position.x = Mathf.Clamp((touch.position.x - fingerDownPos.x) / (touchZone.width / 2), -1, 1);
	                        if (!lockY)
	                        {
	                            position.y = Mathf.Clamp((touch.position.y - fingerDownPos.y) / (touchZone.height / 2), -1, 1);
	                        }
	                    }
	                    else
	                    {
	                        // Change the location of the joystick graphic to match where the touch is
	                        gui.pixelInset = new Rect(Mathf.Clamp(guiTouchPos.x, guiBoundary.min.x, guiBoundary.max.x), gui.pixelInset.y, gui.pixelInset.width, gui.pixelInset.height);
	                        if (!lockY)
	                        {
	                            gui.pixelInset = new Rect(gui.pixelInset.x, Mathf.Clamp(guiTouchPos.y, guiBoundary.min.y, guiBoundary.max.y), gui.pixelInset.width, gui.pixelInset.height);
	                        }
	
	                        if (Mathf.Abs (gui.pixelInset.x) > guiBoundary.max.y * 0.5f
	                            || Mathf.Abs (gui.pixelInset.y) > guiBoundary.max.y * 0.5f)
	                        {
	                            maxTilt = true;
	                        }
	                        else if (maxTilt)
	                        {
	                            maxTilt = false;
	                        }
	                    }
	
	                    if (touch.phase == TouchPhase.Ended || touch.phase == TouchPhase.Canceled)
						{
	                        ResetJoystick();
						}
	                }
	            }
	        }
	
	        if (!touchPad)
	        {
	            // Get a value between -1 and 1 based on the joystick graphic location
	            position.x = (gui.pixelInset.x + guiTouchOffset.x - guiCenter.x) / guiTouchOffset.x;
	            if (!lockY)
	            {
	                position.y = (gui.pixelInset.y + guiTouchOffset.y - guiCenter.y) / guiTouchOffset.y;
	            }
				if(position.x == 0)
				{
					Player.instance.joystickBeingPressed = false;
				}
	        }
	
	        // Adjust for dead zone	
	        var absoluteX = Mathf.Abs(position.x);
	        var absoluteY = Mathf.Abs(position.y);
	
	        if (absoluteX < deadZone.x)
	        {
	            // Report the joystick as being at the center if it is within the dead zone
	            position.x = 0;
	        }
	        else if (normalize)
	        {
	            // Rescale the output after taking the dead zone into account
	            position.x = Mathf.Sign(position.x) * (absoluteX - deadZone.x) / (1 - deadZone.x);
	        }
	
	        if (absoluteY < deadZone.y)
	        {
	            // Report the joystick as being at the center if it is within the dead zone
	            position.y = 0;
	        }
	        else if (normalize)
	        {
	            // Rescale the output after taking the dead zone into account
	            position.y = Mathf.Sign(position.y) * (absoluteY - deadZone.y) / (1 - deadZone.y);
	        }
	
	        prevResolution = currResolution;
		}
		else
		{
			guiTexture.enabled = false;
			joystickBackground.guiTexture.enabled = false;
		}
    }

    void UpdateValues()
    {
        // Store the default rect for the gui, so we can snap back to it
        gui.pixelInset = new Rect(originalRect.x * ResizingViewport.instance.ratio,
            originalRect.y * ResizingViewport.instance.ratio,
            originalRect.width * ResizingViewport.instance.ratio,
            originalRect.height * ResizingViewport.instance.ratio);
        defaultRect = gui.pixelInset;

        if (touchPad)
        {
            // If a texture has been assigned, then use the rect ferom the gui as our touchZone
            if (gui.texture)
                touchZone = defaultRect;
        }
        else
        {
            // This is an offset for touch input to match with the top left
            // corner of the GUI
            guiTouchOffset.x = defaultRect.width * 0.5f;
            guiTouchOffset.y = defaultRect.height * 0.5f;

            // Cache the center of the GUI, since it doesn't change
            guiCenter.x = defaultRect.x + guiTouchOffset.x;
            guiCenter.y = defaultRect.y + guiTouchOffset.y;

            // Let's build the GUI boundary, so we can clamp joystick movement
            guiBoundary.min.x = defaultRect.x - guiTouchOffset.x;
            guiBoundary.max.x = defaultRect.x + guiTouchOffset.x;
            guiBoundary.min.y = defaultRect.y - guiTouchOffset.y;
            guiBoundary.max.y = defaultRect.y + guiTouchOffset.y;
        }
    }
}
