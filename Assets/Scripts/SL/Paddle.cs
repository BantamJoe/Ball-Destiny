using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paddle : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnCollisionEnter2D (Collision2D collision) {
		checkCorrectShape (collision.gameObject.GetComponent<Shape> ());
		Destroy (collision.gameObject);
		BallSpawner.Instance.Spawn (1);
	}

	/**
	 * Check for Correct Shape
	 */
	void checkCorrectShape(Shape characteristic) {
		int color = characteristic.color;
		int shape = characteristic.shape;
		if (GameView.Instance.color == color && GameView.Instance.shape == shape) {
			GameView.countValid ();
			ScoreController.score++;
			widenPaddle ();
		} else {
			ScoreController.score--;
			GameView.countInvalid ();
		}
	}

	void widenPaddle() {
		this.transform.localScale += new Vector3(0.01F, 0, 0);
	}
}
