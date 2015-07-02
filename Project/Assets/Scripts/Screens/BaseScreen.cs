using UnityEngine;
using System.Collections;

public class BaseScreen : MonoBehaviour
{
    /// <summary>
    /// Default delay time to call LoadLevel.
    /// </summary>
    protected float defaultDelayTime = 0.5f;

    /// <summary>
    /// Calls LoadLevel to loading scene.
    /// </summary>
    protected void CallChangeScene()
    {
        Application.LoadLevel(Scenes.Loading.ToString());
    }
}