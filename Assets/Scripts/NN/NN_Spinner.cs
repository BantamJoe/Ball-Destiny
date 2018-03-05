using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spinner : MonoBehaviour {
	private Vector2 delta = Vector2.zero;
	private Vector2 lastPos = Vector2.zero;
	private bool touched = false;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		spinWithMouse ();
	}

	private void spinWithMouse() {
		Vector2 mousePosition = new Vector2(Input.mousePosition.x, Input.mousePosition.y);

		if (Input.GetMouseButtonDown (0)) {
			// Check is it touching the Object
			if (TouchingMe (mousePosition)) {
				lastPos = mousePosition;
				touched = true;
			} else {
				touched = false;
			}
		}
		else if ( Input.GetMouseButton(0) && touched == true ) {
			delta = mousePosition - lastPos;
			Vector2 object_pos = Camera.main.WorldToScreenPoint(this.gameObject.transform.position);
			mousePosition.x = mousePosition.x - object_pos.x;
			mousePosition.y = mousePosition.y - object_pos.y;
			float angle = Mathf.Atan2(mousePosition.y, mousePosition.x) * Mathf.Rad2Deg;
			this.gameObject.transform.rotation = Quaternion.Euler (0, 0, angle);   
		}
	}

	private void spinWithTouch() {
		Vector2 touchPosition = new Vector2 (Input.GetTouch (0).position.x, Input.GetTouch (0).position.y);
		if (Input.GetTouch (0).phase == TouchPhase.Began) {
			// Check is it touching the Object
			if (TouchingMe (touchPosition)) {
				lastPos = touchPosition;
				touched = true;
			} else {
				touched = false;
			}
		} else if (Input.GetTouch (0).phase == TouchPhase.Moved && touched == true) {
			delta = touchPosition - lastPos;
			Vector2 object_pos = Camera.main.WorldToScreenPoint(this.gameObject.transform.position);
			touchPosition.x = touchPosition.x - object_pos.x;
			touchPosition.y = touchPosition.y - object_pos.y;
			float angle = Mathf.Atan2(touchPosition.y, touchPosition.x) * Mathf.Rad2Deg;
			this.gameObject.transform.rotation = Quaternion.Euler (0, 0, angle);   
		}
	}

	private bool TouchingMe (Vector2 point) {
		Vector3 p = Camera.main.ScreenToWorldPoint (point);
		var distance = Mathf.Pow (this.gameObject.transform.position.x - p.x, 2) + Mathf.Pow (this.gameObject.transform.position.y - p.y, 2);
		if (distance < 0.25f) {
			return true;
		} else {
			return false;
		}
	}
}
