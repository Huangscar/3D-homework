using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UGUI : MonoBehaviour {
	public Slider mySlider;
	public float startBlood = 50.0f;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
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
		mySlider.value = startBlood;
	}
}
