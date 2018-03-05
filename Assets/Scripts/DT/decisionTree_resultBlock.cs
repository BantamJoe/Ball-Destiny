using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class decisionTree_resultBlock : MonoBehaviour {
    public string shape;
    public Color32 color;
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        try{
            Debug.Log(other.gameObject.tag + "    " + shape + "      " + other.gameObject.GetComponent<SpriteRenderer>().material.color + "      " + color);
            if (other.gameObject.tag == shape && other.gameObject.GetComponent<SpriteRenderer>().material.color == color) {
                decisionTree_scoreController.score++;
                Debug.Log("Correct Shape and Color");
            }
            else
            {
                decisionTree_scoreController.score--;
            }
        }
        finally{
            Destroy(other.gameObject);
        }
    }
}
