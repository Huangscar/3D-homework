using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(CapsuleCollider))]
[RequireComponent(typeof(Rigidbody))]

public class PatrolUI : SSActionManager, ISSActionCallback, Observer {

	public enum ActionState : int{IDLE, WALKLEFT, WALKFORWARD, WALKRIGHT, WALKBACK};

	private Animator animator;

	private SSAction sSAction;
	private ActionState actionState;

	private const float walkSpeed = 1f;
	private const float runSpeed = 3f;

	// Use this for initialization
	new void Start () {
		animator = this.gameObject.GetComponent<Animator>();
		Publish publish = Publisher.getInstance();
		publish.add(this);

		actionState = ActionState.IDLE;
		idle();
	}
	
	// Update is called once per frame
	new void Update () {
		base.Update();
		if (transform.localEulerAngles.x != 0 || transform.localEulerAngles.z != 0)
        {
            transform.localEulerAngles = new Vector3(0, transform.localEulerAngles.y, 0);
        }
        if (transform.position.y != 0)
        {
            transform.position = new Vector3(transform.position.x, 0, transform.position.z);
        }
	}

	public void SSEventAction(SSAction sSAction, SSAtionEventType events = SSAtionEventType.COMPLETED, int intParam = 0, string strParam = null, Object objParam = null) {
		actionState = actionState > ActionState.WALKBACK ? ActionState.IDLE : (ActionState)((int)actionState + 1);
		switch(actionState) {
			case ActionState.WALKLEFT: {
				walkLeft();
				break;
			}
			case ActionState.WALKRIGHT: {
				walkRight();
				break;
			}
			case ActionState.WALKFORWARD: {
				walkForward();
				break;
			}
			case ActionState.WALKBACK: {
				walkBack();
				break;
			}
			default: {
				idle();
				break;
			}
		}
	}

	public void idle() {
		sSAction = IdleAction.GetIdleAction(Random.Range(1, 1.5f), animator);
		this.runAction(this.gameObject, sSAction, this);
	}

	public void walkLeft() {
		Vector3 target = Vector3.left * Random.Range(3, 5) + this.transform.position;
		sSAction = WalkAction.GetWalkAction(target, walkSpeed, animator);
		this.runAction(this.gameObject, sSAction, this);
	}

	public void walkRight() {
		Vector3 target = Vector3.right * Random.Range(3, 5) + this.transform.position;
		sSAction = WalkAction.GetWalkAction(target, walkSpeed, animator);
		this.runAction(this.gameObject, sSAction, this);
	}

	public void walkForward() {
		Vector3 target = Vector3.forward * Random.Range(3, 5) + this.transform.position;
		sSAction = WalkAction.GetWalkAction(target, walkSpeed, animator);
		this.runAction(this.gameObject, sSAction, this);
	}

	public void walkBack() {
		Debug.Log("walkingback");
		Vector3 target = Vector3.back * Random.Range(3, 5) + this.transform.position;
		sSAction = WalkAction.GetWalkAction(target, walkSpeed, animator);
		this.runAction(this.gameObject, sSAction, this);
	}

	public void turnNextDirection() {
		sSAction.destory = true;
		switch(actionState) {
			case ActionState.WALKLEFT:
				actionState = ActionState.WALKRIGHT;
				walkRight();
				break;
			case ActionState.WALKRIGHT:
				actionState = ActionState.WALKLEFT;
				walkLeft();
				break;
			case ActionState.WALKFORWARD:
				walkBack();
				Debug.Log("walkback!");
				actionState = ActionState.WALKBACK;
				
				break;
			case ActionState.WALKBACK:
				actionState = ActionState.WALKFORWARD;
				walkForward();
				break;
		}
	}

	public void getGoal(GameObject gameObject) {
		sSAction.destory = true;
		sSAction = RunAction.GetRunAction(gameObject.transform, runSpeed, animator);
		this.runAction(this.gameObject, sSAction, this);
	}

	public void loseGoal() {
		sSAction.destory = true;
		idle();
	}

	public void stop() {
		sSAction.destory = true;
		sSAction = IdleAction.GetIdleAction(-1f, animator);
		this.runAction(this.gameObject, sSAction, this);
	}

	public void OnCollisionEnter(Collision collision) {
		Transform transform = collision.gameObject.transform.parent;
		if(transform != null && transform.CompareTag("Wall")) {
			turnNextDirection();
		}
	}

	private void OnTriggerEnter(Collider collider) {
		if(collider.gameObject.CompareTag("Door")) {
			Debug.Log("Door!");
			turnNextDirection();
		}
	}

	public void notified(ActorState actionState, int pos, GameObject gameObject) {
		if(actionState == ActorState.ENTER_AREA) {
			if(pos == this.gameObject.name[this.gameObject.name.Length - 1] - '0') {
				getGoal(gameObject);
			}
			else {
				loseGoal();
			}
		}
		else {
			stop();
		}
	}
}
