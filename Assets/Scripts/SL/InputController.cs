using UnityEngine;
using System.Collections;

public class InputController : MonoBehaviour { 
	public float minX, maxX;

	void Update () {
		//if (Input.touchCount > 0) {
			MoveWithMouse ();
		//}
	}

	void MoveWithMouse () {
		Vector3 paddlePos = new Vector3 (0.5f, this.transform.position.y, 0f);
		float mousePosInBlocks = Input.mousePosition.x / Screen.width * 16 - maxX; // - maxX is for the screen -3f to 3f
		paddlePos.x = Mathf.Clamp(mousePosInBlocks, minX, maxX);
		this.transform.position = paddlePos;
	}
}

// 1.18 14.8