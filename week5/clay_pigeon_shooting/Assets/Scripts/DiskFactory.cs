using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiskFactory : MonoBehaviour
{
    public GameObject disk = null;
    private List<DiskData> used = new List<DiskData>();
    private List<DiskData> free = new List<DiskData>();

    public GameObject GetDisk(int round)
    {
        float stY = -10f;

        disk = null;
        if (free != null && free.Count != 0)
        {
            disk = free[0].gameObject;
            free.Remove(free[0]);
        }

        if (disk == null)
        {
            disk = Instantiate(Resources.Load<GameObject>("Prefabs/disk1"), new Vector3(0, stY, 0), Quaternion.identity);
        }

        Color[] colors = { Color.black, Color.blue, Color.cyan, Color.gray, Color.green, Color.magenta, Color.red, Color.white, Color.yellow };
        Color color = colors[UnityEngine.Random.Range(0, 9)];
        disk.GetComponent<Renderer>().material.color = color;

        float rdX = Random.Range(-1f, 1f) < 0 ? -1 : 1;
        disk.GetComponent<DiskData>().direction = new Vector3(rdX, stY, 0);

        float size = UnityEngine.Random.Range(1f, 3f);
        disk.transform.localScale = new Vector3(size, 0.2f, size);
        used.Add(disk.GetComponent<DiskData>());

        Vector3 position = new Vector3(UnityEngine.Random.Range(-10f, 10f), 0, 0);
        disk.GetComponent<DiskData>().position = position;

        return disk;
    }

    public void FreeDisk(GameObject disk)
    {
        for (int i = 0; i < used.Count; i++)
        {
            if (disk.GetInstanceID() == used[i].gameObject.GetInstanceID())
            {
                used[i].gameObject.SetActive(false);
                free.Add(used[i]);
                used.Remove(used[i]);
                break;
            }
        }
    }
}
