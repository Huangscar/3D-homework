using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour {
	private int turn = 1;
	private int[,] condition = new int[3,3];
	private int result = 0;

	public Texture2D img1;
	public Texture2D img2;
	public Texture2D img3;
	// Use this for initialization
	void Start () {
		Reset();
	}
	
	void Reset() {
		turn = 1;
		for(int i = 0; i < 3; i++) {
			for(int j = 0; j < 3; j++) {
				condition[i,j] = 0;
			}
		}
		result = 0;
	}

	void OnGUI() {
		GUIStyle Style1 = new GUIStyle();
		GUIStyle Style2 = new GUIStyle();

		Style1.normal.background = img1;
		Style2.fontSize = 50;

		GUI.skin.button.fontSize = 50;

		GUI.Label(new Rect(0,0,1024,781), "", Style1);

		if(GUI.Button(new Rect(392,688,240,80), "RESET")) {
			Reset();
		}
		if(check() == 1) {
			if(result == 1) {
				GUI.Label(new Rect(482, 609, 60, 20), "1 win", Style2);
			}
			else if(result == 2) {
				GUI.Label(new Rect(482, 609, 60, 20), "2 win", Style2);
			}
			else {
				GUI.Label(new Rect(482, 609, 60, 20), "draw", Style2);
			}
		}
		for(int i = 0; i < 3; i++) {
			for(int j = 0; j < 3; j++) {
				if(condition[i,j] == 1) {
					GUI.Button(new Rect (272+i*160, 70+j*160, 160, 160), img2);
				}
				if(condition[i,j] == 2) {
					GUI.Button(new Rect (272+i*160, 70+j*160, 160, 160), img3);
				}
				if(GUI.Button(new Rect(272+i*160, 70+j*160, 160, 160), "")) {
					if(result == 0) {
						if(turn == 1) {
							condition[i,j] = 1;
							turn = 2;
						}
						else {
							condition[i, j] = 2;
							turn = 1;
						}
					}
				}
			}
		}
	}

	int check() {
		for(int i = 0; i < 3; i++) {
			if(condition[i,0] == 1 && condition[i,1] == 1 && condition[i,2] == 1) {
				result = 1;
				return 1;
			}
			else if(condition[i,0] == 2 && condition[i,1] == 2 && condition[i,2] == 2) {
				result = 2;
				return 1;
			}
		}
		for(int j = 0; j < 3; j++) {
			if(condition[0,j] == 1 && condition[1,j] == 1 && condition[2,j] == 1) {
				result = 1;
				return 1;
			}
			else if(condition[0,j] == 2 && condition[1,j] == 2 && condition[2,j] == 2) {
				result = 2;
				return 1;
			}
		}
		if(condition[0,0] == 1 && condition[1,1] == 1 && condition[2,2] == 1) {
			result = 1;
			return 1;
		}
		if(condition[0,0] == 2 && condition[1,1] == 2 && condition[2,2] == 2) {
			result = 2;
			return 1;
		}
		if(condition[0,2] == 1 && condition[1,1] == 1 && condition[2,0] == 1) {
			result = 1;
			return 1;
		}
		if(condition[0,2] == 2 && condition[1,1] == 2 && condition[2,0] == 2) {
			result = 2;
			return 1;
		}
		for(int i = 0; i < 3; i++) {
			for(int j = 0; j < 3; j++) {
				if(condition[i, j] == 0) {
					return 0;
				}
			}
		}
		return 1;
	}
}
