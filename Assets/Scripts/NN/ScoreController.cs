using NN;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace NN{
public class ScoreController : MonoBehaviour {
	public static int total;
	public static int totalValid;
	public static int score;
	public static int invalid;
	private static int test10 = 0;
	private static int last = 0;

	public Text text;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (total % 10 == 0 && total != last) {
			test10++;
			last = total;
			print ("Passed: " + (test10 * 10));
		}

		text.text = "Score : " + score; 
	}
}
}