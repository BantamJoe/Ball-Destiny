using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BottomCollider : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnCollisionEnter2D (Collision2D collision) {
		checkMatchShape (collision.gameObject.GetComponent<Shape> ());
		Destroy (collision.gameObject);
		BallSpawner.Instance.Spawn (1);
	}

	public void checkMatchShape(Shape characteristic) {
		int origin_color = characteristic.color;
		int origin_shape = characteristic.shape;
		if (GameView.Instance.color == origin_color && GameView.Instance.shape == origin_shape) {
			GameView.countBall ();
		}
	}

}
