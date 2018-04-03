using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript2 : MonoBehaviour {
	public Transform planet;
	public float distance;
	public float x;
	public float y;
	public float z;
	public float speed;

	private Vector3 normal;
	public GameObject shadow;
	// Use this for initialization
	void Start () {
		x = planet.position.z;
		y = planet.position.x;
		z = planet.position.y;
		float length = (float)System.Math.Sqrt(x*x+y*y+z*z);
		transform.parent = shadow.transform;
		transform.position = shadow.transform.position + new Vector3(distance, 0, 0);
		normal = new Vector3(x,y,z);
	}
	
	// Update is called once per frame
	void Update () {
		shadow.transform.position = planet.position;
		shadow.transform.Rotate(normal, speed*Time.deltaTime);
	}
}
