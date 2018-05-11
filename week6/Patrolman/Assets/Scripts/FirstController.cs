using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FirstController : MonoBehaviour, Observer, ISceneController {

	public Text scoreText;
	public Text centerText;

	public Camera main_camera;

	public GameObject player;

	private ScoreRecorder scoreRecorder;
	private UIController uIController;
	private ObjectFactory objectFactory;

	private float[] posX = {-5, 7, -5, 5};
	private float[] posZ = {-5, -7, 5, 5};


	// Use this for initialization
	void Start () {
		SSDirector director = SSDirector.GetInstance();
		director.CurrentScenceController = this;
		scoreRecorder = new ScoreRecorder();
		scoreRecorder.scoreText = scoreText;
		uIController = new UIController();
		uIController.centerText = centerText;
		objectFactory = Singleton<ObjectFactory>.Instance;
		Publish publish = Publisher.getInstance();
		publish.add(this);
		LoadResources();
	}

	public void LoadResources() {
		player = Instantiate(Resources.Load("prefab/Ami"), new Vector3(5.5f, 0, 3), Quaternion.Euler(new Vector3(0, 100, 0))) as GameObject;
		for(int i = 0; i < 4; i++) {
			GameObject patrol = objectFactory.setObjectPosition(new Vector3(posX[i], 0, posZ[i]), Quaternion.Euler(new Vector3(0, 100, 0)));
			patrol.name = "Patrol" + (i + 1);
		}
		
		
		main_camera.transform.parent = player.transform;
	}

	public void notified(ActorState actorState, int pos, GameObject gameObject) {
		if(actorState == ActorState.ENTER_AREA) {
			scoreRecorder.addScore(1);
		}
		else {
			uIController.loseGame();
			Debug.Log("lose game");
		}
	}

	
	
}
