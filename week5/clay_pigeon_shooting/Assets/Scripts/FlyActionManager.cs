using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyActionManager : SSActionManager
{

    public DISKFlyAction fly;


    public void DISKFly(GameObject disk, float speed)
    {
        fly = DISKFlyAction.GetSSAction(disk.GetComponent<DiskData>().direction, speed);
        this.RunAction(disk, fly, this);
    }

    public void playDisk(GameObject disk, float speed)
    {
        DISKFly(disk, speed);
    }
}