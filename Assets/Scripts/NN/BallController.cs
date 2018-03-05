using NN;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NN{
public class BallController : MonoBehaviour {
	private Vector2 lastPos;
	// Use this for initialization
	void Start () {
		lastPos = this.gameObject.transform.position;
	}
	
	// Update is called once per frame
	void Update () {
		checkReverseDirection ();
	}
	
	// Destroy Object if it go to left
	void checkReverseDirection() {
		if (lastPos.x - this.gameObject.transform.transform.position.x > 0) {
			Destroy (this.gameObject);
		}
		lastPos = this.gameObject.transform.transform.position;
	}
}
}