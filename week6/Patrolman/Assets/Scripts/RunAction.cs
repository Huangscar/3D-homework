using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunAction : SSAction {

	private float speed;
	private Transform target;
	private Animator animator;

	public static RunAction GetRunAction(Transform target, float speed, Animator animator) {
		RunAction run = ScriptableObject.CreateInstance<RunAction>();
		run.speed = speed;
		run.target = target;
		run.animator = animator;
		return run;
	}

	// Use this for initialization
	public override void Start () {
		animator.SetFloat("Speed", 1);
	}
	
	// Update is called once per frame
	public override void Update () {
		Quaternion rotation = Quaternion.LookRotation(target.position - transform.position);
		if(transform.rotation != rotation) {
			transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * speed * 5);
		}
		this.transform.position = Vector3.MoveTowards(this.transform.position, target.position, speed * Time.deltaTime);
		if(Vector3.Distance(this.transform.position, target.position) <0.5) {
			this.destory = true;
			this.actionCallback.SSEventAction(this);
		}
	}
}
