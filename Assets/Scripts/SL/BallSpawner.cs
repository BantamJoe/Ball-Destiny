using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

namespace SL{
public class BallSpawner : MonoBehaviour {

	public int initial_ball = 5;            // Initial Ball Counting
	public int increment_ball = 1;          // Each Level add one more ball
	public int passLevel = 10;              // 50 Correct Ball will add one more ball
	public float initial_gravity = 0.6f;    // Initial Mass
	public float increment_gravity = 0.01f; // Mass Increment per Level
	public int maximum_block = 500;

	public static BallSpawner Instance { get { return _instance;} private set{}}
	private static BallSpawner _instance;

	public float minX = -3.2f;
	public float maxX = 3.2f;
	public float minY = 5.4f;

	private static float current_gravity = 0.6f;
	public static int counter = 0;
	private static int total = 0;
	public static bool startCounting = false;
	private static bool finished = false;

	public GameObject[] shapes;

	public static int[] shape_weight;
	public static int[] color_weight;

    public GameObject EndingPanel;
	[HideInInspector]
	public Color type;

	public Color[] colors = new Color[] {
		new Color32 (88, 180, 214, 255),
		new Color32 (214, 49, 212, 255),
		new Color32 (255, 215, 56, 255)
	} ;

	void Awake ()
	{
		_instance = this;
        total = 0;
        counter = 0;
    }

	void Start () {

    }

	void Update (){
        End.title = SceneManager.GetActiveScene().name;     // Set the title of ending panel
        End.message = "Score: " + ScoreController.score;
    }

	public void Spawn (int cnt)
	{
		StartCoroutine(SpawnCircles (cnt));
	}

	public void setup() {
		// Set Initial Gracity
		current_gravity = initial_gravity;
		Physics2D.gravity = new Vector2 (0, -current_gravity);
	}

	public IEnumerator SpawnCircles (int cnt)
	{
		// Initially Create All Object
		var i = 0;
		while (i < cnt) 
		{
			SpawnCircle();
			i++;
			yield return new WaitForSeconds(0.05f);
		}
	}

	void SpawnCircle ()
	{
		total++;
            
		if (total < maximum_block) {
			// int chosen_shape = shape_weight[UnityEngine.Random.Range (0, shape_weight.Length)];
			int chosen_shape = UnityEngine.Random.Range (0, shapes.Length);
			GameObject chosen = shapes [chosen_shape];
            
			var circle = Instantiate (chosen) as GameObject;
			circle.transform.parent = transform;
			// Random Range Spawn
			circle.transform.position = new Vector3 (Random.Range (minX, maxX), minY, 0);

			// Random Color (Type)
			// int chosen_color = color_weight[UnityEngine.Random.Range (0, color_weight.Length)];
			int chosen_color = UnityEngine.Random.Range (0, colors.Length);
			type = colors [chosen_color];
			circle.GetComponent<Renderer> ().material.color = type;

			// Set Shape's Characteristic
			circle.AddComponent<Shape> ();
			circle.GetComponent<Shape> ().color = chosen_color;
			circle.GetComponent<Shape> ().shape = chosen_shape;

			// Set Shape's Tag
			circle.tag = "ball";

			// Count for adding Level
			if (startCounting) {
				counter++;
				if (counter == passLevel) {
					print ("Passed Level");
					counter = 0;
					SpawnCircle ();
				}
			}
		} else {
			startCounting = false;
			finished = true;

			int remainingObject = GameObject.FindGameObjectsWithTag("ball").Length;
			// Finished
			if (remainingObject == 1){
                EndingPanel.SetActive(finished);

                GameView.Instance.reporting();
            }

		}
	}
}
}