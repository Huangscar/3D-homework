using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour {
	public Transform star;
	public float speed = 20;
	float y, z;

	float distence;
	// Use this for initialization
	void Start () {
		z = Random.Range(1, 360);
		y = Random.Range(1, 360);
		
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 axis = new Vector3(0,y,z);
		this.transform.RotateAround(star.position, axis, speed * Time.deltaTime);
	}
}
