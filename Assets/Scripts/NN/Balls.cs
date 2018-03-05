using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Balls : MonoBehaviour {

	public static Balls Instance { get { return _instance;} private set{}}
	private static Balls _instance;

	public GameObject shape1;
	public GameObject shape2;
	public GameObject shape3;

	public static int counter = 0;
	private static int total = 0;

	public static GameObject[] shapes;

	[HideInInspector]
	public Color type;

	public static Color[] colors = new Color[] {
		new Color32 (88, 180, 214, 255),
		new Color32 (214, 49, 212, 255),
		new Color32 (255, 215, 56, 255)
	} ;

	void Awake ()
	{
		_instance = this;
	}

	// Use this for initialization
	void Start () {
		// Generate Balls
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	// Initializing Ball
	void init() {
		
	}

	// Spawn one Ball
	void Spawn() {
		total++;
	}
}
