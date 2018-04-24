using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionManagerAdapter : MonoBehaviour, IActionManager
{
    public FlyActionManager actionManager;
    public PhysisFlyActionManager phyActionManager;
    public void playDisk(GameObject disk, float speed, bool isPhy)
    {
        if (isPhy)
        {
            phyActionManager.playDisk(disk, speed);
        }
        else
        {
            actionManager.playDisk(disk, speed);
        }
    }
    // Use this for initialization
    void Start()
    {
        actionManager = gameObject.AddComponent<FlyActionManager>() as FlyActionManager;
        phyActionManager = gameObject.AddComponent<PhysisFlyActionManager>() as PhysisFlyActionManager;
    }

    // Update is called once per frame
    void Update()
    {

    }
}
