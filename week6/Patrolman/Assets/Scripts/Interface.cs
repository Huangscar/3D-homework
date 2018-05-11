using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum SSAtionEventType : int {STARTED, COMPLETED}
public interface ISceneController
{
    void LoadResources();
}

public interface ISSActionCallback {
	void SSEventAction(SSAction source, SSAtionEventType events = SSAtionEventType.COMPLETED, int intParam = 0, string strParam = null, Object objParam = null);
}

public enum ActorState {ENTER_AREA, DEATH}

public interface Publish {
    void notify(ActorState actorState, int pos, GameObject gameObject);
    void add(Observer observer);
}

public interface Observer {
    void notified(ActorState actorState, int pos, GameObject gameObject);
}

