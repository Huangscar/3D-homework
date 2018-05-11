using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleAction : SSAction {

	private float time;
	private Animator animator;

	public static IdleAction GetIdleAction(float time, Animator animator) {
		IdleAction currentAction = ScriptableObject.CreateInstance<IdleAction>();
		currentAction.time = time;
		currentAction.animator = animator;
		return currentAction;
	}

	// Use this for initialization
	public override void Start () {
		animator.SetFloat("Speed", 0);
	}
	
	// Update is called once per frame
	public override void Update () {
		if(time == -1) {
			return;
		}
		time -= Time.deltaTime;
		if(time < 0) {
			this.destory = true;
			this.actionCallback.SSEventAction(this);
		}
	}
}
