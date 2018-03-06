using UL;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UL{
public class Balls : MonoBehaviour {

	public static Balls Instance { get { return _instance;} private set{}}
	private static Balls _instance;

	public int maximum_balls = 100;
	public int power;

	public float shooting_x = -0.2f;
	public float shooting_y = 0f;
	public float shooting_rate = 0.5f;

	public static int counter = 0;

	public Vector2 gravity;

	public GameObject[] shapes;

	private float nextAction = 0.0f; // Next Ball
	

	[HideInInspector]
	public Color type;

	public Color[] colors;

	public Vector2[] directions;

	void Awake ()
	{
		_instance = this;
	}

	// Use this for initialization
	void Start () {
		// Set Gravity
		Physics2D.gravity = gravity;
	}
	
	// Update is called once per frame
	void Update () {
		// Generate Balls
		if (Time.time > nextAction && ScoreController.total < maximum_balls) {
			print (ScoreController.total);
			nextAction += shooting_rate;
			Spawn ();
		}
	}

	// Spawn one Ball
	public void Spawn() {
		ScoreController.total++;

		int chosen_shape = UnityEngine.Random.Range (0, shapes.Length);
		GameObject chosen = shapes [chosen_shape];

		var circle = Instantiate (chosen) as GameObject;
		circle.transform.parent = transform;
		// Random Range Spawn
		circle.transform.position = new Vector3 (shooting_x, shooting_y, 0);

		// Random Color (Type)
		// int chosen_color = color_weight[UnityEngine.Random.Range (0, color_weight.Length)];
		int chosen_color = UnityEngine.Random.Range (0, colors.Length);
		type = colors [chosen_color];
		circle.GetComponent<Renderer> ().material.color = type;

		// Set Shape's Tag
		circle.tag = "ball";

		addForce (circle);

		// Check Shape is Targeted Characteristic
		checkColor(circle);
	}

	public void addForce(GameObject ball) {
		Vector2 force = directions[Random.Range(0, directions.Length)] * power;
		ball.GetComponent<Rigidbody2D> ().AddForce (force);
	}

	public void checkColor(GameObject circle) {
		if (circle.GetComponent<Renderer> ().material.color == Bucket.Instance.color) {
			ScoreController.totalValid++;
		}
	}
}
}
