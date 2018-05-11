using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SSActionManager : MonoBehaviour {

	private Dictionary <int, SSAction> dictionary = new Dictionary<int, SSAction>();
	private List<SSAction> waitingAddAction = new List<SSAction>();
	private List<int> waitingDelete = new List<int>();

	// Use this for initialization
	protected void Start () {
		
	}
	
	// Update is called once per frame
	protected void Update () {
		foreach(SSAction ac in waitingAddAction) {
			dictionary[ac.GetInstanceID()] = ac;
		}
		waitingAddAction.Clear();
		foreach(KeyValuePair<int, SSAction> dic in dictionary) {
			SSAction ac = dic.Value;
			if(ac.destory) {
				waitingDelete.Add(ac.GetInstanceID());
			}
			else if(ac.enable) {
				ac.Update();
			}
		}
		foreach (int id in waitingDelete) {
			SSAction ac = dictionary[id];
			dictionary.Remove(id);
			DestroyObject(ac);
		}
		waitingDelete.Clear();
	}

	public void runAction(GameObject gameObject, SSAction action, ISSActionCallback actionCallback) {
		action.gameObject = gameObject;
		action.transform = gameObject.transform;
		action.actionCallback = actionCallback;
		waitingAddAction.Add(action);
		action.Start();
	}
}
