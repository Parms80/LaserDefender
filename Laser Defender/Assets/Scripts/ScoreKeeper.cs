using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ScoreKeeper : MonoBehaviour {

	public static int currentScore = 0;
	private Text scoreText;

	void Start() {
		scoreText = GetComponent<Text> ();
		Reset ();
	}

	public void Score(int points) {
		currentScore += points;
		scoreText.text = currentScore.ToString();
	}

	public static void Reset() {
		currentScore = 0;
	}
}
