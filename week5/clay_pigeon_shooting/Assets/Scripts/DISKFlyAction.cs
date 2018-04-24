using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DISKFlyAction : SSAction
{
    public float gravity = 10;                                 

    public float vx, vy, vz;

    private DISKFlyAction() { }
    public static DISKFlyAction GetSSAction(Vector3 direction, float speed)
    {
        
        DISKFlyAction action = CreateInstance<DISKFlyAction>();
        action.vx = speed*direction.x;
        action.vy = speed*direction.y;
        action.vz = speed*direction.z;

        return action;
    }

    public override void Update()
    {
        float xt1 = transform.position.x + vx * Time.deltaTime;
        float zt1 = transform.position.z + vz * Time.deltaTime;
        vy -= gravity * Time.deltaTime;
        float yt1 =transform.position.y + vy * Time.deltaTime;
        transform.position = new Vector3(xt1, yt1, zt1);

        if (this.transform.position.y < -10 || this.transform.position.y > 9)
        {
            this.destroy = true;
            this.callback.SSActionEvent(this);
        }
    }
    public override void FixedUpdate() { }
    public override void Start() { }
}