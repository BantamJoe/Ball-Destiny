using UL;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace UL{
public class ScoreController : MonoBehaviour {
	public static int total;
	public static int totalValid;
	public static int score;
	public static int invalid;
	private static int last = 0;
	public Text text;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		text.text = "Score : " + score; 
	}
}
}