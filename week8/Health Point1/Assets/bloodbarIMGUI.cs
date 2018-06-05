using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bloodbarIMGUI : MonoBehaviour {
	private Rect big;

	//public GUISkin mySkin;
	
	private float startBlood = 100.0f;

	//public GUIStyle style;
	// Use this for initialization
	void Start () {
		big = new Rect(20,20, 200, 20);
	}
	
	void Update() {
		
		
	}
	// Update is called once per fra
	void OnGUI() {
		
		if(Input.GetKeyUp(KeyCode.C)) {
			if(startBlood > 0) {
				startBlood = startBlood - 10;
			}
			Debug.Log(startBlood);
		}
		if(Input.GetKeyUp(KeyCode.X)) {
			if(startBlood < 100.0f) {
				startBlood = startBlood + 10;
			}
			Debug.Log(startBlood);
		}
		GUI.HorizontalScrollbar(big, 0.0f, startBlood, 0.0f, 100.0f);
		
	}
	
}
