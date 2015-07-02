using UnityEngine;
using System.Collections;

public class TestPush : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	void OnGUI()
	{
		Rect pos = new Rect(Screen.width /2 - Screen.width*.10f, 5, Screen.width*.20f, 40);
		if(GUI.Button(pos, "Test Push"))
		{
			PlayFabManager.instance.SendPush("BD0D893542C2369E", "TEST MESSAGE FROM UNION...");
		}
	}
}
