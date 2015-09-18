using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ScoreManagerScript : MonoBehaviour {

    public static int Score { get; set; }
	public Text scoreText;
	// Use this for initialization
	void Start () {
   
	}
	
	// Update is called once per frame
	void Update () {

		scoreText.text = Score.ToString();

	}


    int previousScore = -1;
}
