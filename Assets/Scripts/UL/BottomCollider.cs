﻿using UL;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UL{
public class BottomCollider : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnCollisionEnter2D (Collision2D collision) {
		Destroy (collision.gameObject);
	}

}
}