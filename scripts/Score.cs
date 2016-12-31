using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Score : MonoBehaviour {

	static int score = 0;
	static int highScore = 0;
	Text  text;

	static public void AddPoint(){
		score++;

		if (score > highScore)
			highScore = score;
	}

	void Start(){
		text = GetComponent<Text> ();
		score = 0;
		highScore = PlayerPrefs.GetInt ("highscore", 0);
	}

	void OnDestroy(){
		PlayerPrefs.SetInt ("highscore", highScore);
	}


	// Update is called once per frame
	void Update () {
		text.text = "Score: " + score + "\nHighScore: " + highScore;
	}
}
