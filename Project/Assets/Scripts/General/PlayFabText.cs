using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlayFabText : MonoBehaviour
{
    /// <summary>
    /// Key to load the title data from.
    /// </summary>
    [SerializeField]
    string keyToLoad;

    /// <summary>
    /// Local text reference.
    /// </summary>
    Text myText;

    /// <summary>
    /// Initialization method. If possible, gets data from PlayFab's Title Data, and puts it into the Text object.
    /// </summary>
    void Start()
    {
        if (keyToLoad.Length > 0)
        {
            myText = this.GetComponent<Text>();

            string value = PlayFabManager.instance.GetTitleValue(keyToLoad);
            if (value.Length > 0) myText.text = value;
        }
    }
}