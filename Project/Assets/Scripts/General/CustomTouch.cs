using UnityEngine;
using System.Collections;

/// <summary>
/// Custom touch class. Used to get all necessary references.
/// </summary>
public class CustomTouch {


    /// <summary>
    /// Finger ID of touch
    /// </summary>
	public int fingerID;

    /// <summary>
    /// Position of touch
    /// </summary>
	public Vector2 touchPosition;

    /// <summary>
    /// Current phase of touch
    /// </summary>
	public TouchPhase actualPhase;

    /// <summary>
    /// Side of touch
    /// </summary>
	public TouchSides side;
}
