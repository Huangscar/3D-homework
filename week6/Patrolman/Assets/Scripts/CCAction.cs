using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CCAction : SSAction, ISSActionCallback {


	public List<SSAction> sequence;
	public int repeat = -1;
	public int start = 0;
	// Use this for initialization
	public override void Start () {
		foreach(SSAction ac in sequence) {
			ac.gameObject = this.gameObject;
			ac.transform = this.transform;
			ac.actionCallback = this;
			ac.Start();
		}
	}
	
	// Update is called once per frame
	public override void Update () {
		if(sequence.Count == 0) {
			return;
		}
		if(start < sequence.Count) {
			sequence[start].Update();
		}
	}

	public void SSEventAction(SSAction source, SSAtionEventType events = SSAtionEventType.COMPLETED, int intParam = 0, string strParam = null, Object objParam = null) {
		source.destory = false;
		this.start++;
		if(this.start >= this.sequence.Count) {
			this.start = 0;
			if(this.repeat == 0) {
				this.destory = true;
				this.actionCallback.SSEventAction(this);
			}
		}
	}

	public static CCAction GetSSAction(List<SSAction> _sequence, int _start = 0, int _repeat = 1) {
		CCAction actions = ScriptableObject.CreateInstance<CCAction>();
		actions.sequence = _sequence;
		actions.start = _start;
		actions.repeat = _repeat;
		return actions;
	}

	private void OnDestroy() {
		this.destory = true;	
	}

	
}
