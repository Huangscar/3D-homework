using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PhysisDISKFlyAction : SSAction
{
    public float gravity = -5;                                
    public Vector3 direction;
    public float speed;
    private PhysisDISKFlyAction() { }
    public static PhysisDISKFlyAction GetSSAction(Vector3 direction, float speed)
    {
       
        PhysisDISKFlyAction action = CreateInstance<PhysisDISKFlyAction>();
        action.direction = direction;
        action.speed = speed;
        return action;
    }

    public override void FixedUpdate()
    {
        
        if (this.transform.position.y < -10)
        {
            this.destroy = true;
            this.callback.SSActionEvent(this);
        }
    }
    public override void Update() { }
    public override void Start()
    {
        
        gameobject.GetComponent<Rigidbody>().velocity = speed * direction;
        gameobject.GetComponent<Rigidbody>().useGravity = true;
    }
}