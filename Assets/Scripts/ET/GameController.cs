using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ET{
public class GameController : MonoBehaviour {

	public GameObject[] ans_shapes;
	public Color[] ans_colors;
	public GameObject[] shapes;
	public Color[] colors;
	public GameObject[] scorebars;

	public Vector2[] positions;

	private static List<GameObject> balls;
	private static List<float> correctCounter;
	private static int length;

	public bool finished;
	private bool initialised;
	private bool updating;

	public static GameController Instance { get { return _instance;} private set{}}
	private static GameController _instance;

	void Awake() {
		balls = new List<GameObject> ();
		finished = false;
		initialised = false;
		updating = false;
		length = ans_shapes.Length;
		_instance = this;
	}

	// Use this for initialization
	void Start () {
		// Set Gravity
		Physics2D.gravity = new Vector2 (0, 0);
		correctCounter = new List<float> (new float[length]);

		// Spawn Balls
		do {
			finished=false;
			init ();
			updateScorebars ();
		} while(finished);
		initialised = true;

		for (int i = 0; i < correctCounter.Count; i++) {
				print (i + " ; " + correctCounter [i] + " ; " + scorebars [i].GetComponent<GUIBarScript> ().Value);
		}
	}
	
	// Update is called once per frame
	void Update () {
		//scorebars [0].GetComponent<GUIBarScript>().Value = ((Mathf.Sin (Time.time)/2f) + 0.5f) * 1.01f;
		updateScorebars ();
	}

	void init(){
		if (balls.Count > 0) {
			for (int i = 0; i < balls.Count; i++) {
				Destroy (balls [i]);
			}
		}
		for (int i = 0; i < length; i++) {
			scorebars [i].GetComponent<GUIBarScript> ().Value = 0.5f;
		}
		// Spawn Balls
		for (int i = 0; i < positions.Length; i++) {
			Spawn (positions [i]);
		}
	}

	public void Spawn(Vector2 position) {

		int chosen_shape = UnityEngine.Random.Range (0, shapes.Length);
		GameObject chosen = shapes [chosen_shape];

		var circle = Instantiate (chosen) as GameObject;
		circle.name = chosen.name;
		circle.transform.parent = transform;
		// Random Range Spawn
		circle.transform.position = new Vector3 (position.x, position.y, 0);

		// Random Color (Type)
		// int chosen_color = color_weight[UnityEngine.Random.Range (0, color_weight.Length)];
		int chosen_color = UnityEngine.Random.Range (0, colors.Length);
		circle.GetComponent<Renderer> ().material.color = colors [chosen_color];
		circle.GetComponent<Collider2D> ().enabled = false;

		// Set Shape's Tag
		circle.tag = "ball";

		// Add 

		// Add To Ball List
		balls.Add(circle);

		// Set Elemenet Controller
		circle.AddComponent<ElementController>();
	}

	void updateScore() {
		int bar = 0;
		int ballOrder = 0;
		int countCorrect = 0;
		for (int i = 0; i < balls.Count; i++) {
			if (i % scorebars.Length == 0) {
				if (!initialised) {
					print (countCorrect + " ; " + bar);
				}
			}
			if (i>0 && i % scorebars.Length == 0) {		// How Many Genes
				scorebars [bar].GetComponent<GUIBarScript> ().Value = (float) ((float)countCorrect / (float)length) * 1.01f; // Gene's Length
				bar++;
				ballOrder = 0;
				countCorrect = 0;
			}
			if (
				balls [i].GetComponent<Renderer> ().material.color == ans_colors [ballOrder]
				&&
				balls [i].name == ans_shapes [ballOrder].name) {
				countCorrect++;
					//print (balls [i].GetComponent<Renderer> ().material.color + " ; " + ans_colors [ballOrder]);
			}
			ballOrder++;
			if (countCorrect == length) {
				finished = true;
			}
		}
	}

	void updateScorebars() {
		for (int bar = 0; bar < length; bar++) {
			correctCounter [bar] = 0;
			for (int j = (bar * length); j < ((bar * length) + length); j++) {
				if (balls [j].GetComponent<Renderer> ().material.color == ans_colors [j % length]) {
					correctCounter [bar]++;
				}
				if (balls [j].name == ans_shapes [j % length].name) {
					correctCounter [bar]++;
				}
			}
			// scorebars [bar].GetComponent<GUIBarScript> ().Value = ((Mathf.Sin (Time.time)/2f) + 0.5f) * 1.01f;
			scorebars [bar].GetComponent<GUIBarScript> ().Value = (float) (correctCounter [bar] / (float) (length + length)) * 1.01f;
			//scorebars [bar].GetComponent<GUIBarScript> ().Value = (float) ((float)correctCounter [bar] / (float)length) * 1.01f; // Gene's Length
		}
	}

	public static int getBalls(GameObject ball) {
		return balls.FindIndex (obj => obj.transform.position == ball.transform.position);
	}

	public static int getTop(int pos){
		int row = pos / length;
		int col = pos % length;
		if (row == 0) {
			return -1;
		}
		int output = (row - 1) * length + col;
		return output;
	}

	public static int getBottom(int pos) {
		int row = pos / length;
		int col = pos % length;
		if (row == length - 1) {
			return -1;
		}
		int output = (row + 1) * length + col;
		return output;
	}

		// Crossover Genes
	public void swap(int x, int y){
		if (x >= 0 && y >= 0) {
			GameObject swapObject = balls [x];
			Vector2 swapper = new Vector2(balls [x].transform.position.x, balls [x].transform.position.y);
			balls [x].transform.position = Vector2.Lerp((Vector2) balls [x].transform.position, (Vector2) balls [y].transform.position, Time.fixedDeltaTime);
			balls [y].transform.position = Vector2.Lerp((Vector2) balls [y].transform.position, (Vector2) swapper, Time.fixedDeltaTime);

			balls [x] = balls [y];
			balls [y] = swapObject;
		}
	}
	// Mutating Genes
	public void mutation(int x){
		if (x >= 0) {
			int chosen_shape = UnityEngine.Random.Range (0, shapes.Length);
			GameObject chosen = shapes [chosen_shape];

			var circle = Instantiate (chosen) as GameObject;
			circle.name = chosen.name;
			circle.transform.parent = transform;
			circle.transform.position = new Vector3 (balls [x].transform.position.x, balls [x].transform.position.y, 0);

			// Random Color (Type)
			// int chosen_color = color_weight[UnityEngine.Random.Range (0, color_weight.Length)];
			int chosen_color = UnityEngine.Random.Range (0, colors.Length);
			circle.GetComponent<Renderer> ().material.color = colors [chosen_color];
			circle.GetComponent<Collider2D> ().enabled = false;

			// Set Shape's Tag
			circle.tag = "ball";
			Destroy (balls [x]);
			balls [x] = circle;
		}
	}

	void updatePosition() {
			
	}
}
}