using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreRecorder {

	public Text scoreText;
	private int score = -1;

	public void resetScore() {
		score = -1;
	}

	public void addScore(int score) {
		Debug.Log("add 1");
		this.score += score;
		scoreText.text = "Score:" + this.score;
	}

	public void setDisActiove() {
		scoreText.text = "";
	}

	public void setActive() {
		scoreText.text = "Score:" + this.score;
	}
}
