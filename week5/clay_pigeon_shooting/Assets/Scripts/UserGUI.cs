using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UserGUI : MonoBehaviour
{
    private IUserAction action;


    private bool gameStatus = false;

    public GameObject canvas;
    //public Text score;
    private GameObject canvasP;
    private Text scoreText;

    void Start()
    {
        action = SSDirector.GetInstance().CurrentScenceController as IUserAction;

        canvasP = Instantiate(Resources.Load<GameObject>("Prefabs/Canvas"));
        scoreText = Instantiate(Resources.Load<Text>("Prefabs/Text"), canvasP.transform);
        scoreText.transform.Translate(canvasP.transform.position);
        scoreText.text = "score: ";
    }

    void OnGUI()
    {


        if (gameStatus)
        {
            //用户射击
            if (Input.GetButtonDown("Fire1"))
            {
                Vector3 pos = Input.mousePosition;
                action.Hit(pos);
            }

            scoreText.text = "round: " + action.GetRound() + "   score: " + action.GetScore();


        }
        else
        {

            if (GUI.Button(new Rect(Screen.width / 2 - 50, Screen.height / 2 - 20, 100, 50), "START"))
            {
                gameStatus = true;
                action.BeginGame();
            }
        }
    }

}
