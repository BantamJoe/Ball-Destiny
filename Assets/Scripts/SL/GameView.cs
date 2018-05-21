using SL;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace SL{
    public class GameView : MonoBehaviour {

	    private static GameView _instance;

	    public static GameView Instance { get { return _instance; } private set {} }

	    public int color;
	    public int shape;
	    public float x_axis=5f;
	    public float y_axis=5f;

	    private static int totalBall = 0;
	    private static int totalValid = 0;
	    private static int totalInvalid = 0;

	    void Awake ()
	    {
		    _instance = this;
	    }

	    void Start () {
		    BallSpawner.Instance.setup ();

		    color = UnityEngine.Random.Range (0, BallSpawner.Instance.colors.Length);
		    shape = Random.Range (0, BallSpawner.Instance.shapes.Length);
		    GameObject chosen = BallSpawner.Instance.shapes [shape];

		    // setWeighting ();

		    // Initial Scene Balls
		    StartCoroutine(BallSpawner.Instance.SpawnCircles (BallSpawner.Instance.initial_ball));
		    BallSpawner.startCounting = true;
		    BallSpawner.counter = 0;

		    // Initialize the Indicator Object
		    var indicator = Instantiate (chosen) as GameObject;
		    indicator.transform.parent = transform;
		    indicator.GetComponent<Renderer> ().material.color = BallSpawner.Instance.colors[color];
		    indicator.transform.position = new Vector3 (x_axis, y_axis, 0);
		    indicator.GetComponent<Collider2D> ().enabled = false;
		    UnityEngine.Object.Destroy(indicator.GetComponent<Rigidbody2D>());
	    }

	    void setWeighting(){
		    // Color Weighting
		    int color_max = BallSpawner.Instance.colors.Length * (BallSpawner.Instance.colors.Length + 1);
		    BallSpawner.color_weight = new int[color_max];
		    int color_counter = 0;
		    for (int i = 0; i < BallSpawner.Instance.colors.Length; i++) {
			    if (color != i) {
				    for (int j = color_counter; j < BallSpawner.Instance.colors.Length; j++) {
					    BallSpawner.color_weight [j] = i;
					    color_counter++;
				    }
			    } else {
				    for (int j = color_counter; j < (BallSpawner.Instance.colors.Length * 2); j++) {
					    BallSpawner.color_weight [j] = i;
					    color_counter++;
				    }
			    }
		    }
		    // Shape Weighting
		    int shape_max = BallSpawner.Instance.shapes.Length * (BallSpawner.Instance.shapes.Length + 1);
		    BallSpawner.shape_weight = new int[shape_max];
		    int shape_counter = 0;
		    for (int i = 0; i < BallSpawner.Instance.shapes.Length; i++) {
			    if (i != shape) {
				    for (int j = shape_counter; j < BallSpawner.Instance.shapes.Length; j++) {
					    BallSpawner.shape_weight [j] = i;
					    shape_counter++;
				    }
			    } else {
				    for (int j = shape_counter; j < (BallSpawner.Instance.shapes.Length * 2); j++) {
					    BallSpawner.shape_weight [j] = i;
					    shape_counter++;
				    }
			    }
		    }

		    for (int i = 0; i < BallSpawner.shape_weight.Length; i++) {
			    print (BallSpawner.shape_weight [i]);
		    }
	    }

	    public static void countBall() {
		    totalBall++;
	    }

	    public static void countValid() {
		    totalValid++;
		    countBall ();
	    }

	    public static void countInvalid() {
		    totalInvalid++;
	    }

	    public void reporting() {
		    print ("Total: " + totalBall);
		    print ("Valid: " + totalValid);
		    print ("Invalid: " + totalInvalid);
	    }
    }
}