using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DT
{
    public class resultBlock : MonoBehaviour
    {
        public string shape;
        public Color32 color;
        public static int countDestroy;

        private void OnTriggerEnter2D(Collider2D other)
        {
            try
            {
                Debug.Log(other.gameObject.tag + "    " + shape + "      " + other.gameObject.GetComponent<SpriteRenderer>().material.color + "      " + color);
                if (other.gameObject.tag == shape && other.gameObject.GetComponent<SpriteRenderer>().material.color == color)
                {
                    scoreController.score++;
                    Debug.Log("Correct Shape and Color");
                }
                else
                {
                    scoreController.score--;
                }
            }
            finally
            {
                countDestroy++;
                Destroy(other.gameObject);
            }
        }
    }
}
