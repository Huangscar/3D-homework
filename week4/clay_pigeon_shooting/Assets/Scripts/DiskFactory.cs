using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//飞碟工厂
public class DiskFactory : MonoBehaviour {
    public DiskData prefab;
    List<DiskData> used = new List<DiskData>();
    List<DiskData> free = new List<DiskData>();

    //获取飞碟发射
    public DiskData getDisk(Ruler ruler)
    {
        DiskData a_disk;
        if (free.Count > 0)
        {
            a_disk = free[0];
            //从闲置列表里删除
            free.RemoveAt(0);
        }
        else
        {
            a_disk = Instantiate(prefab);
        }
        a_disk.ruler = ruler;
        used.Add(a_disk);
        return a_disk;
    }

    //将一飞碟变成闲置飞碟
    public void freeDisk(DiskData disk)
    {
        free.Add(disk);
        if (!used.Remove(disk))
        {
            throw new System.Exception();
        }
    }

    // Use this for initialization
    void Start () {

    }

    // Update is called once per frame
    void Update () {

    }
}