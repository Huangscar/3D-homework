using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 

//积分器
public class ScoreController
{
    public int score = 0;
    public void record(DiskData disk)
    {
        score += Mathf.RoundToInt(disk.ruler.color.b +  disk.ruler.color.g + disk.ruler.color.r);
        score += Mathf.RoundToInt(disk.ruler.size);
        score += Mathf.RoundToInt(disk.ruler.speed);
    }
}

public class SceneController : MonoBehaviour {
    List<DiskData> diskList;
    DiskFactory diskFactory;
    public ScoreController scoreCtrl;
    public Text score;
    //public Text round1;
    private Text scoreText;
    //private Text roundText;
    public GameObject canvas;
    private GameObject canvasP;

    //UserGUI usergui;
    int round = 1;
    void Awake()
    {
        SSDirector director = SSDirector.getInstance();
        director.setFPS(60);
        director.currentSceneController = this;
        director.currentSceneController.LoadResources();
    }

    public void LoadResources()
    {
        diskList = new List<DiskData>();
        diskFactory = Singleton<DiskFactory>.Instance;
        scoreCtrl = new ScoreController();
    }

    // Use this for initialization
    void Start () {
        canvasP = Instantiate(canvas);
        scoreText = Instantiate(score, canvasP.transform);
        scoreText.transform.Translate(canvasP.transform.position);
        scoreText.text = "score: ";

        //roundText = Instantiate(score, canvasP.transform);
        //roundText.transform.Translate(canvasP.transform.position);
        //roundText.text = "round: ";

    }

    // Update is called once per frame
    float timeControl = 0;
    public int upperNum;
    int leftNum;
    public Camera cam;
    Color[] colors = { Color.black, Color.blue, Color.cyan, Color.gray, Color.green, Color.magenta, Color.red, Color.white, Color.yellow };
    void Update () {
        scoreText.text = "round: " + round + "   score: " + scoreCtrl.score;
        timeControl += Time.deltaTime;
        if (timeControl >= 2f)
        {
            if(scoreCtrl.score/(50*(round/10+1)) > round) {
                round++;
            }
            upperNum = round/5;
            int shootNum = UnityEngine.Random.Range(1, upperNum);
            //leftNum -= shootNum;
            float vMin = round%5 + 5 + round/5;
            float vMax = round%5 + 10 + round*round/5;
            for (int i = 0; i < shootNum; i++) {
                Color color = colors[UnityEngine.Random.Range(0, 9)];
                float size = UnityEngine.Random.Range(1f, 3f);
                Vector3 position = new Vector3(UnityEngine.Random.Range(-10f, 10f), 0, 0);
                float speed = UnityEngine.Random.Range(vMin, vMax);
                //起始位置
                Vector3 direction = new Vector3(
                    UnityEngine.Random.Range(-1000f, 1000f),
                    UnityEngine.Random.Range(0, 1000f),
                    UnityEngine.Random.Range(0, 1000f)
                    );
                direction.Normalize();
                Ruler ruler = new Ruler(color, size, position, speed, direction);
                diskList.Add(diskFactory.getDisk(ruler));
            }
            timeControl = 0;
        }
        for (int i = 0; i < diskList.Count; i++)
        {
            if (diskList[i].gameObject.transform.position.y < -5)
            {
                destoryDisk(diskList[i]);
            }
        }
        for (int i = 0; i < diskList.Count; i++)
        {
            runDisk(diskList[i]); 
        }
        if (Input.GetButtonDown("Fire1"))
        {
            Vector3 mousePosition = Input.mousePosition;
            Ray ray = cam.ScreenPointToRay(mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                scoreCtrl.record(hit.transform.gameObject.GetComponent<DiskData>());
                destoryDisk(hit.transform.gameObject.GetComponent<DiskData>());
                print(scoreCtrl.score);
                //scoreText.text = "score: " + scoreCtrl.score;
                //usergui.score = string.Format("%d", scoreCtrl.score);
            }
        }
    }

    void destoryDisk(DiskData disk)
    {
        disk.gameObject.transform.position = new Vector3(0, 0, -100);
        diskFactory.freeDisk(disk);
        diskList.Remove(disk);
    }
    public float gravity = 9.8f;
    void runDisk(DiskData disk)
    {
        float xt1 = disk.gameObject.transform.position.x + disk.vx * Time.deltaTime;
        float zt1 = disk.gameObject.transform.position.z + disk.vz * Time.deltaTime;
        disk.vy -= gravity * Time.deltaTime;
        float yt1 = disk.gameObject.transform.position.y + disk.vy * Time.deltaTime;
        disk.gameObject.transform.position = new Vector3(xt1, yt1, zt1);
    }
}