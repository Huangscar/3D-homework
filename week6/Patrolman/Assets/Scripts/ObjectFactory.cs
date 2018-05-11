using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectFactory : MonoBehaviour {

	private static List<GameObject> used = new List<GameObject>();
	private static List<GameObject> free = new List<GameObject>();


	public GameObject setObjectPosition(Vector3 targetPosition, Quaternion facePosition) {
		if(free.Count == 0) {
			GameObject gameObject = Instantiate(Resources.Load("prefab/Patrol"), targetPosition, facePosition) as GameObject;
			used.Add(gameObject);
		}
		else {
			used.Add(free[0]);
			free.RemoveAt(0);
			used[used.Count - 1].SetActive(true);
			used[used.Count - 1].transform.position = targetPosition;
			used[used.Count - 1].transform.localRotation = facePosition;
		}
		return used[used.Count - 1];
	}

	public void freeObject(GameObject gameObject) {
		gameObject.SetActive(false);
		used.Remove(gameObject);
		free.Add(gameObject);
	}

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
