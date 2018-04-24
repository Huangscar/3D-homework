using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstController : MonoBehaviour, ISceneController, IUserAction
{
    public IActionManager actionManager;
    public DiskFactory diskFactory;
    public UserGUI userGui;
    public ScoreRecorder scoreRecorder;
    public bool isPhy = false;

    private Queue<GameObject> diskQueue = new Queue<GameObject>();
    private List<GameObject> diskBeforeShot = new List<GameObject>();
    private int round = 1;
    private float intervalTime = 2f;
    private bool playingGame = false;
    private bool gameStart = false;

    void Start()
    {
        SSDirector director = SSDirector.GetInstance();
        director.CurrentScenceController = this;
        diskFactory = Singleton<DiskFactory>.Instance;
        scoreRecorder = Singleton<ScoreRecorder>.Instance;
        actionManager = gameObject.AddComponent<ActionManagerAdapter>() as IActionManager;
        userGui = gameObject.AddComponent<UserGUI>() as UserGUI;
    }

    void Update()
    {
        if (gameStart)
        {
            if (!playingGame)
            {
                InvokeRepeating("LoadResources", 1f, intervalTime);
                playingGame = true;
            }
            //发送飞碟
            SendDisk();
            //回合升级
            if (scoreRecorder.score >= round * 50 * (round / 10 + 1))
            {
                round++;
                scoreRecorder.round = round;
                intervalTime = intervalTime - 0.1f;
                if (intervalTime < 1f)
                {
                    intervalTime = 1f;
                }
                CancelInvoke("LoadResources");
                playingGame = false;
            }

        }
    }

    public void LoadResources()
    {
        diskQueue.Enqueue(diskFactory.GetDisk(round));
    }

    private void SendDisk()
    {

        if (diskQueue.Count != 0)
        {
            GameObject disk = diskQueue.Dequeue();
            diskBeforeShot.Add(disk);
            disk.SetActive(true);
            Vector3 direction = new Vector3(
                    UnityEngine.Random.Range(-1000f, 1000f),
                    UnityEngine.Random.Range(0, 1000f),
                    UnityEngine.Random.Range(0, 1000f)
                    );
            direction.Normalize();

            disk.GetComponent<DiskData>().direction = direction;
            disk.transform.position = disk.GetComponent<DiskData>().position;
            float vMin = round % 5 + 5 + round / 5;
            float vMax = round % 5 + 10 + round * round / 5;
            float speed = UnityEngine.Random.Range(vMin, vMax);
            disk.GetComponent<DiskData>().speed = speed;
            actionManager.playDisk(disk, speed, isPhy);
        }

        for (int i = 0; i < diskBeforeShot.Count; i++)
        {
            GameObject temp = diskBeforeShot[i];

            if ((temp.transform.position.y < -10 || temp.transform.position.y > 9) && temp.gameObject.activeSelf == true)
            {
                diskFactory.FreeDisk(diskBeforeShot[i]);
                diskBeforeShot.Remove(diskBeforeShot[i]);


            }
        }
    }

    public void Hit(Vector3 pos)
    {
        Ray ray = Camera.main.ScreenPointToRay(pos);
        RaycastHit[] hits;
        hits = Physics.RaycastAll(ray);
        bool not_hit = false;
        for (int i = 0; i < hits.Length; i++)
        {
            RaycastHit hit = hits[i];

            if (hit.collider.gameObject.GetComponent<DiskData>() != null)
            {

                for (int j = 0; j < diskBeforeShot.Count; j++)
                {
                    if (hit.collider.gameObject.GetInstanceID() == diskBeforeShot[j].gameObject.GetInstanceID())
                    {
                        not_hit = true;
                    }
                }
                if (!not_hit)
                {
                    return;
                }
                diskBeforeShot.Remove(hit.collider.gameObject);

                scoreRecorder.Record(hit.collider.gameObject);


                DestroyDisk(hit, diskFactory, hit.collider.gameObject);
                //StartCoroutine(WaitingParticle(0.08f, hit, diskFactory, hit.collider.gameObject));
                break;
            }
        }
    }

    public int GetScore()
    {
        return scoreRecorder.score;
    }

    public int GetRound()
    {
        return scoreRecorder.round;
    }



    public void DestroyDisk(RaycastHit hit, DiskFactory diskFactory, GameObject obj)
    {
        hit.collider.gameObject.transform.position = new Vector3(0, -9, 0);
        diskFactory.FreeDisk(obj);
    }
    public void BeginGame()
    {
        gameStart = true;
    }
}