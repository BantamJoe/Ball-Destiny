using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LR
{
    public class shape : MonoBehaviour { 
    
        public Color[] colors = new Color[] { };
        public GameObject[] shapes = new GameObject[] { };
        public int numOfShape = 2;

        public Vector2 maxPos;
        public Vector2 minPos;

        public static List<Vector2> posList;

        void Start(){
            if (posList == null) posList = new List<Vector2>();
            for (int i = 0; i < numOfShape; i++)
                CreateObject(i);
        }

        public void CreateObject(int num){
            var ran_shape = Random.Range(0, shapes.Length);
            var ran_color = Random.Range(0, colors.Length);

            var offsetX = Random.Range(-1, 1);
            var offsetY = Random.Range(0, maxPos.y);
            var RanX = minPos.x + num + offsetX;
            var RanY = minPos.y + (num/2) + offsetY;
            
            posList.Add(new Vector2(RanX, RanY));

            GameObject item = Instantiate(this.shapes[ran_shape]) as GameObject;
            item.transform.parent = transform;
            item.transform.position = new Vector3(RanX, RanY, 0);

            item.GetComponent<Rigidbody2D>().gravityScale = 0;
            item.GetComponent<Collider2D>().isTrigger = true;
            item.GetComponent<SpriteRenderer>().material.color = colors[ran_color];
        }
    }
}
