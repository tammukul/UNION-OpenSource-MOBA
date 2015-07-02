using UnityEditor;
using UnityEngine;
using System.Collections;

public class DeletePlayerPrefs : Editor {

    [MenuItem("Flux/Delete Player Prefs")]
    public static void ShowWindow()
    {
        PlayerPrefs.DeleteAll();
    }
}
