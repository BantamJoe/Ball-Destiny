using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ET{
public class ElementController : MonoBehaviour {

	Vector2 firstPressPos;
	Vector2 secondPressPos;
	Vector2 currentSwipe;
	bool touched;

	// Use this for initialization
	void Start () {
		touched = false;
	}
	
	// Update is called once per frame
	void Update () {
		isTouch ();
		isClick ();
		if (touched) {
			MouseSwipe ();
		}
	}

	public void isTouch() {
		if (Input.touches.Length > 0) {
			if (Input.GetTouch (0).phase == TouchPhase.Began) {
				Vector3 p = Input.GetTouch (0).position;
				Vector3 t = Camera.main.ScreenToWorldPoint (p);
				var distance = Mathf.Pow (this.gameObject.transform.position.x - t.x, 2) + Mathf.Pow (this.gameObject.transform.position.y - t.y, 2);
				if (distance < 0.25f) {
					touched = true;
				}
			}
		}
	}

	public void isClick() {
		Vector2 p = new Vector2(Input.mousePosition.x,Input.mousePosition.y);
		if (Input.GetMouseButtonDown (0)) {
			Vector2 t = Camera.main.ScreenToWorldPoint (p);
			var distance = Mathf.Pow (this.gameObject.transform.position.x - t.x, 2) + Mathf.Pow (this.gameObject.transform.position.y - t.y, 2);
			if (distance < 0.25f) {
				touched = true;
			}
		}
	}

	public void MouseSwipe()
	{
		if(Input.GetMouseButtonDown(0))
		{
			//save began touch 2d point
			firstPressPos = new Vector2(Input.mousePosition.x,Input.mousePosition.y);
		}
		if(Input.GetMouseButtonUp(0))
		{
			//save ended touch 2d point
			secondPressPos = new Vector2(Input.mousePosition.x,Input.mousePosition.y);

			//create vector from the two points
			currentSwipe = new Vector2(secondPressPos.x - firstPressPos.x, secondPressPos.y - firstPressPos.y);

			//normalize the 2d vector
			currentSwipe.Normalize();

			//swipe upwards
			if(currentSwipe.y > 0 && currentSwipe.x > -0.5f && currentSwipe.x < 0.5f)
			{
				swapUp ();
				Debug.Log("up swipe");
			}
			//swipe down
			if(currentSwipe.y < 0 && currentSwipe.x > -0.5f && currentSwipe.x < 0.5f)
			{
				swapDown ();
				Debug.Log("down swipe");
			}
			//swipe left
			if(currentSwipe.x < 0 && currentSwipe.y > -0.5f && currentSwipe.y < 0.5f)
			{
				mutate ();
				Debug.Log("left swipe");

			}
			//swipe right
			if(currentSwipe.x > 0 && currentSwipe.y > -0.5f && currentSwipe.y < 0.5f)
			{
				Debug.Log("right swipe");
			}

			touched = false;
		}
	}

	public void TouchSwipe()
	{
		if(Input.touches.Length > 0)
		{
			Touch t = Input.GetTouch(0);
			if(t.phase == TouchPhase.Began)
			{
				//save began touch 2d point
				firstPressPos = new Vector2(t.position.x,t.position.y);
			}
			if(t.phase == TouchPhase.Ended)
			{
				//save ended touch 2d point
				secondPressPos = new Vector2(t.position.x,t.position.y);

				//create vector from the two points
				currentSwipe = new Vector3(secondPressPos.x - firstPressPos.x, secondPressPos.y - firstPressPos.y);

				//normalize the 2d vector
				currentSwipe.Normalize();

				//swipe upwards
				if(currentSwipe.y > 0 && currentSwipe.x > -0.5f && currentSwipe.x < 0.5f)
				{
					Debug.Log("up swipe");
				}
				//swipe down
				if(currentSwipe.y < 0 && currentSwipe.x > -0.5f && currentSwipe.x < 0.5f)
				{
					Debug.Log("down swipe");
				}
				//swipe left
				if(currentSwipe.x < 0 && currentSwipe.y > -0.5f && currentSwipe.y < 0.5f)
				{
					Debug.Log("left swipe");
				}
				//swipe right
				if(currentSwipe.x > 0 && currentSwipe.y > -0.5f && currentSwipe.y < 0.5f)
				{
					Debug.Log("right swipe");
				}
				touched = false;
			}
		}
	}

	public void getBall(){
		int current = GameController.getBalls (this.gameObject);
		int top = GameController.getTop (current);
		int bottom = GameController.getBottom (current);
		print (current + " ; " + top + " ; " + bottom);
	}

	public void swapUp() {
		int current = GameController.getBalls (this.gameObject);
		int top = GameController.getTop (current);
		GameController.Instance.swap (current, top);
	}

	public void swapDown() {
		int current = GameController.getBalls (this.gameObject);
		int bottom = GameController.getBottom (current);
		GameController.Instance.swap (current, bottom);
	}

	public void mutate() {
		int current = GameController.getBalls (this.gameObject);
		GameController.Instance.mutation (current);
	}
}
}