using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CSScoreText : MonoBehaviour {
    Text ScoreLabel;
    public static int Score;
	void Awake () {
        ScoreLabel=GetComponent<Text>();
	}
	
	// Update is called once per frame
	void Update () {
        ScoreLabel.text = "Score: "+CSScoreText.Score.ToString();
	}
}
