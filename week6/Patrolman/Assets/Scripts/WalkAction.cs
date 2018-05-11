using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkAction : SSAction {

	private float speed;
	private Vector3 target;
	private Animator animator;

	public static WalkAction GetWalkAction(Vector3 target, float speed, Animator animator) {
		WalkAction walk = ScriptableObject.CreateInstance<WalkAction>();
		walk.speed = speed;
		walk.target = target;
		walk.animator = animator;
		return walk;
	}

	// Use this for initialization
	public override void Start () {
		animator.SetFloat("Speed", 0.5f);
	}
	
	// Update is called once per frame
	public override void Update () {
		Quaternion rotation = Quaternion.LookRotation(target - transform.position);
		if(transform.rotation != rotation) {
			transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * speed * 5);
		}
		this.transform.position = Vector3.MoveTowards(this.transform.position, target, speed*Time.deltaTime);
		if(this.transform.position == target) {
			this.destory = true;
			this.actionCallback.SSEventAction(this);
		}
	}
}
