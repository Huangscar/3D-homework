using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SSDirector : Object {
    private static SSDirector _instance;
    public SceneController currentSceneController { get; set; }
    public bool running { get; set; }
    public static SSDirector getInstance()
    {
        if (_instance == null)
        {
            _instance = new SSDirector();
        }
        return _instance;
    }
    public int getFPS()
    {
        return Application.targetFrameRate;
    }
    //设置帧率
    public void setFPS(int fps)
    {
        Application.targetFrameRate = fps;
    }
    public void NextScene()
    {

    }
}