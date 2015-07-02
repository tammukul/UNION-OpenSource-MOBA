using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class InputManager : MonoBehaviour
{
    /// <summary>
    /// Static instance.
    /// </summary>
    public static InputManager instance;

    /// <summary>
    /// Boolean containing if the input will be mocked with mouse. Make it false when build occurs.
    /// </summary>
    public bool useWithMouse;

    /// <summary>
    /// Left analog stick image reference.
    /// </summary>
    public Image leftTouchImage;
    /// <summary>
    /// Right analog stick image reference.
    /// </summary>
    public Image rightTouchImage;

    /// <summary>
    /// Left analog stick pointer image referente.
    /// </summary>
    public Image leftTouchPointer;
    /// <summary>
    /// Right analog stick pointer image reference.
    /// </summary>
    public Image rightTouchPointer;

    /// <summary>
    /// Number of touches current happening.
    /// </summary>
    int touchesCount;
    /// <summary>
    /// Current touches info list.
    /// </summary>
    List<CustomTouch> touches;

    /// <summary>
    /// Delegate used when touch starts.
    /// </summary>
    ProjectDelegates.TouchStartCallback OnTouchStart = delegate(CustomTouch touch) {/*Debug.Log("touch "+touch.fingerID+" start "+touch.side);*/};
    /// <summary>
    /// Delegate used when touch moves/updates.
    /// </summary>
    ProjectDelegates.TouchMoveCallback OnTouchMove = delegate(CustomTouch touch, Vector2 deltaPosition) {/*Debug.Log("touch "+touch.fingerID+" moved "+touch.side+" "+deltaPosition);*/};
    /// <summary>
    /// Delegate used when touch ends.
    /// </summary>
    ProjectDelegates.TouchEndCallback OnTouchEnd = delegate(CustomTouch touch) {/*Debug.Log("touch "+touch.fingerID+" end "+touch.side);*/};

	/// <summary>
	/// Reference to the scale of the screen, based on the 1920x1080 layout.
	/// </summary>
	float screenScale;

    /// <summary>
    /// Initialization method. Gets static instance, and initializes touches list.
    /// </summary>
    void Awake()
    {
        instance = this;

        touches = new List<CustomTouch>();
        touchesCount = 0;
		screenScale = Screen.width / 1920f;
    }

    /// <summary>
    /// Adds callback listeners. Used to handle actual player input, and give it to the movement manager, or somewhere else needed.
    /// </summary>
    /// <param name="startCallback">Touch start callback.</param>
    /// <param name="moveCallback">Touch moved callback.</param>
    /// <param name="endCallback">Touch ended callback.</param>
    public void AddListener(ProjectDelegates.TouchStartCallback startCallback, ProjectDelegates.TouchMoveCallback moveCallback, ProjectDelegates.TouchEndCallback endCallback)
    {
        this.OnTouchStart += startCallback;
        this.OnTouchMove += moveCallback;
        this.OnTouchEnd += endCallback;
    }

    /// <summary>
    /// Update method. Checks if mouse mock, and handles actual input depending on the boolean value.
    /// </summary>
    void Update()
    {
        if (useWithMouse)
        {
            this.CheckClicks();
        }
        else
        {
            this.CheckTouches();
        }
    }

    /// <summary>
    /// Checks all current touches, its states, and handles all touch states.
    /// </summary>
    public void CheckTouches()
    {
        int touchCount = Input.touchCount;
        if (touchCount == 0) return;

        Touch touch;
        for (int i = 0; i < touchCount; i++)
        {
            touch = Input.GetTouch(i);

            switch (touch.phase)
            {
                case TouchPhase.Began:
                    this.CreateNewTouch(touch.fingerId, touch.position);
                    break;
                case TouchPhase.Stationary:
                case TouchPhase.Moved:
                    this.UpdateTouch(touch.fingerId, touch.position);
                    break;
                case TouchPhase.Canceled:
                case TouchPhase.Ended:
                    this.RemoveTouch(touch.fingerId);
                    break;
            }
        }
    }

    /// <summary>
    /// Touch mock method, to use mouse.
    /// </summary>
    public void CheckClicks()
    {
        if (Input.GetMouseButtonDown(0))
        {
            this.CreateNewTouch(0, Input.mousePosition);
        }
        else if (Input.GetMouseButtonUp(0))
        {
            this.RemoveTouch(0);
        }
        else if (Input.GetMouseButton(0))
        {
            this.UpdateTouch(0, Input.mousePosition);
        }
    }

    /// <summary>
    /// Creates a new touch from parameters, and sends it to the listeners.
    /// </summary>
    /// <param name="ID">Touch ID.</param>
    /// <param name="position">Touch position on screen.</param>
    void CreateNewTouch(int ID, Vector2 position)
    {
        TouchSides side;
        side = position.x < Screen.width / 2f ? TouchSides.Left : TouchSides.Right;

        if (this.ContainsTouchInSide(side) || this.ContainsTouchWithFinger(ID))
        {
            return;
        }

        CustomTouch touch = new CustomTouch();
        touch.fingerID = ID;
        touch.touchPosition = position;
        touch.actualPhase = TouchPhase.Began;
        touch.side = side;

        touches.Add(touch);
        touchesCount++;
        this.ReorderTouches();

        switch (side)
        {
            case TouchSides.Left:
                leftTouchImage.gameObject.SetActive(true);
                Vector3 touchPosition = touch.touchPosition;
				touchPosition /= screenScale;
                RectTransform rect = (RectTransform)leftTouchImage.transform;
                rect.anchoredPosition = touchPosition;
                break;
            case TouchSides.Right:
                rightTouchImage.gameObject.SetActive(true);
                touchPosition = touch.touchPosition;
				touchPosition /= screenScale;
                rect = (RectTransform)rightTouchImage.transform;
                rect.anchoredPosition = touchPosition;
                break;
        }

        this.OnTouchStart(touch);
    }

    /// <summary>
    /// Method used to reorder the touches array.
    /// The array must be ordered in a way that all shooting touches comes after all movement touches.
    /// </summary>
    void ReorderTouches()
    {
        CustomTouch[] touchesArray = touches.ToArray();

        System.Array.Sort(touchesArray, delegate(CustomTouch touch1, CustomTouch touch2)
        {
            return touch1.side.GetHashCode().CompareTo(touch2.side.GetHashCode());
        });

        touches.Clear();
        touches.AddRange(touchesArray);
    }

    /// <summary>
    /// Removes touch from list. Called when the touch ends.
    /// </summary>
    /// <param name="ID">Touch ID.</param>
    void RemoveTouch(int ID)
    {
        for (int i = 0; i < touchesCount; i++)
        {
            if (touches[i].fingerID == ID)
            {
                touches[i].actualPhase = TouchPhase.Ended;

                switch (touches[i].side)
                {
                    case TouchSides.Left:
                        leftTouchImage.gameObject.SetActive(false);
                        break;
                    case TouchSides.Right:
                        rightTouchImage.gameObject.SetActive(false);
                        break;
                }

                this.OnTouchEnd(touches[i]);
                touches.Remove(touches[i]);
                touchesCount--;
                break;
            }
        }
    }

    /// <summary>
    /// Updates touch. Called when the user's finger moves on screen.
    /// </summary>
    /// <param name="ID">Touch ID.</param>
    /// <param name="newPosition">New position for touch.</param>
    void UpdateTouch(int ID, Vector2 newPosition)
    {
        for (int i = 0; i < touchesCount; i++)
        {
            if (touches[i].fingerID == ID)
            {
                touches[i].actualPhase = TouchPhase.Moved;
                Vector2 deltaPosition = new Vector2(newPosition.x - touches[i].touchPosition.x, newPosition.y - touches[i].touchPosition.y);

                switch (touches[i].side)
                {
                    case TouchSides.Left:
                        leftTouchPointer.transform.localPosition = deltaPosition.normalized * 200;
                        break;
                    case TouchSides.Right:
                        rightTouchPointer.transform.localPosition = deltaPosition.normalized * 200;
                        break;
                }

                this.OnTouchMove(touches[i], deltaPosition);
            }
        }
    }

    /// <summary>
    /// Checks if screen side already contains touch on it.
    /// </summary>
    /// <param name="side">Touch side.</param>
    /// <returns>Boolean if contains touch.</returns>
    bool ContainsTouchInSide(TouchSides side)
    {
        for (int i = 0; i < touchesCount; i++)
        {
            if (touches[i].side == side)
            {
                return true;
            }
        }
        return false;
    }

    /// <summary>
    /// Checks if the finger ID is already linked with some touch.
    /// </summary>
    /// <param name="fingerID">Finger ID.</param>
    /// <returns>Bool if finger ID contains touch.</returns>
    bool ContainsTouchWithFinger(int fingerID)
    {
        for (int i = 0; i < touchesCount; i++)
        {
            if (touches[i].fingerID == fingerID)
            {
                return true;
            }
        }
        return false;
    }
}