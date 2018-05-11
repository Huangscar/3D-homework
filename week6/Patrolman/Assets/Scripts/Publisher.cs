using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Publisher : Publish {

	private delegate void ActionUpdate(ActorState state, int pos, GameObject gameObject);
	private ActionUpdate updateList;

	private static Publisher _instance;

	public static Publisher getInstance() {
		if(_instance == null) {
			_instance = new Publisher();
		}
		return _instance;
	}

	public void notify(ActorState actorState, int pos, GameObject gameObject) {
		if(updateList != null) {
			//Debug.Log("this enter area");
			updateList(actorState, pos, gameObject);
		}
	}

	public void add(Observer observer) {
		updateList += observer.notified;
	}

	public void delete(Observer observer) {
		updateList -= observer.notified;
	}
}
