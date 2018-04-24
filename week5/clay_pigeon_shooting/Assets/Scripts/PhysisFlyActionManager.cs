using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysisFlyActionManager : SSActionManager
{

    public PhysisDISKFlyAction fly;

    protected void Start()
    {
    }

    public void DISKFly(GameObject disk, float speed)
    {
        fly = PhysisDISKFlyAction.GetSSAction(disk.GetComponent<DiskData>().direction, speed);
        this.RunAction(disk, fly, this);
    }

    public void playDisk(GameObject disk, float speed)
    {
        DISKFly(disk, speed);
    }
}