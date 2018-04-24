using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreRecorder : MonoBehaviour
{
    public int score;
    public int round;
    void Start()
    {
        score = 0;
        round = 1;
    }

    public void Record(GameObject disk)
    {
        score += disk.GetComponent<DiskData>().score;
        score += Mathf.RoundToInt(-disk.GetComponent<DiskData>().scale.x + 3);
        score += Mathf.RoundToInt((disk.GetComponent<DiskData>().color.r + disk.GetComponent<DiskData>().color.g + disk.GetComponent<DiskData>().color.b) / 255);
        score += Mathf.RoundToInt(disk.GetComponent<DiskData>().speed);
    }

}
