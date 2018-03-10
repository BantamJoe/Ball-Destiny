using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LS
{
    public class shape : MonoBehaviour
    {

        public Color[] colors = new Color[] { };
        public GameObject[] shapes = new GameObject[] { };
        public int numOfShape = 2;
        private GameObject placeArea;
        
        void Start(){
            placeArea = GameObject.Find("placeArea");
            for (int i = 0; i < numOfShape; i++)
                CreateObject();
        }
        
        public void CreateObject(){
            var ran_shape = Random.Range(0, shapes.Length);
            var ran_color = Random.Range(0, colors.Length);
            var posOfPlace = placeArea.transform.position;
            var max = placeArea.GetComponent<SpriteRenderer>().bounds.max;
            var min = placeArea.GetComponent<SpriteRenderer>().bounds.min;
            var Xrange = ( max.x - min.x ) / 2;
            var Yrange = ( max.y - min.y ) / 2;
            var ran_location_x = Random.Range(posOfPlace.x+Xrange+1, 8);
            var ran_location_y = Random.Range(-4, 4);

            GameObject item = Instantiate(this.shapes[ran_shape]) as GameObject;
            item.transform.parent = transform;
            item.transform.position = new Vector3((posOfPlace.x + ran_location_x), (posOfPlace.y + ran_location_y), 0);
            //Debug.Log("colors[ran_color]:  " + colors[ran_color]);
            item.AddComponent<DrapandDropController>();
            item.GetComponent<Rigidbody2D>().gravityScale = 0;
            item.GetComponent<SpriteRenderer>().material.color = colors[ran_color];
        }
    }
}
