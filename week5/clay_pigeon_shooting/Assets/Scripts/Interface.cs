using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public interface IActionManager
{
    void playDisk(GameObject disk, float speed, bool isPhy);
}

public interface ISceneController
{

    void LoadResources();
}

public interface IUserAction
{

    void Hit(Vector3 pos);

    int GetScore();

    int GetRound();

    void BeginGame();
}
public enum SSActionEventType : int { Started, Competeted }
public interface ISSActionCallback
{
    void SSActionEvent(SSAction source, SSActionEventType events = SSActionEventType.Competeted,
        int intParam = 0, string strParam = null, Object objectParam = null);
}