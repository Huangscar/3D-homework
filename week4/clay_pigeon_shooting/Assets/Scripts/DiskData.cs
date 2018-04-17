using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//飞碟的规格
public class Ruler
{
    public Color color;
    public float size;
    public Vector3 position;
    public float speed;
    public Vector3 direction; // 单位向量

    public Ruler(Color color, float size, Vector3 position, float speed, Vector3 direction)
    {
        this.color = color;
        this.size = size;
        this.position = position;
        this.speed = speed;
        this.direction = direction;
    }
}


//设置飞碟的规格
public class DiskData : MonoBehaviour {
    Ruler _ruler;
    public Ruler ruler {
        get
        {
            return _ruler;
        }
        set
        {
            _ruler = value;
            gameObject.GetComponent<Renderer>().material.color = value.color;
            //自身缩放
            gameObject.transform.localScale = new Vector3(value.size, 0.2f, value.size);
            gameObject.transform.position = value.position;
            //将速度分解
            vx = value.speed * value.direction.x;
            vy = value.speed * value.direction.y;
            vz = value.speed * value.direction.z;
        }
    }
    public float vx, vy, vz;
    // Use this for initialization
    void Start () {
        
    }

    // Update is called once per frame
    void Update () {

    }
}