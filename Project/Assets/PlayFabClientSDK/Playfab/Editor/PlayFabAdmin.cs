using UnityEditor;
using UnityEngine;
using PlayFab;

namespace PlayFab.Editor
{
	public class PlayFabAdmin: EditorWindow
    {
		private float titleId;
		private float catalogVersion;

        [MenuItem ("Playfab/Dashboard")]
        private static void ShowWindow ()
        {
			Application.OpenURL("https://developer.playfab.com/");
        }
      }
}
