using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class decisionTree_scoreController : MonoBehaviour {
    public static int score;

    Text text;

    private void Awake()
    {
        text = GetComponent<Text>();
        score = 0;
    }
	
	// Update is called once per frame
	void Update () {
        text.text = "SCORE: " + score;
	}
}
