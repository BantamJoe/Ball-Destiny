using UL;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UL{
public class Bucket : MonoBehaviour {
	public Color32 color;
	public Vector2 position;

	public static Bucket Instance { get { return _instance;} private set{}}
	private static Bucket _instance;

	void Awake() {
		_instance = this;
	}

	// Use this for initialization
	void Start () {
		init ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	/**
	 * Initial Bucket's Color and Position
	 */
	void init() {
		// Set Bucket Color
		this.gameObject.GetComponent<Renderer> ().material.color = color;

		// Set Bucket Position
		this.gameObject.transform.position = new Vector3 (position.x, position.y, 0);
	}

	void OnCollisionEnter2D (Collision2D collision) {
		checkMatchShape (collision.gameObject);
		Destroy (collision.gameObject);
	}

	public void checkMatchShape(GameObject circle) {
		if (color == circle.GetComponent<Renderer>().material.color) {
			ScoreController.score++;
		} else {
			ScoreController.invalid++;
		}
	}

}
}