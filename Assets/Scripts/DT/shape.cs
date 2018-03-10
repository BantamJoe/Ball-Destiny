using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DT
{
    public class shape : MonoBehaviour
    {
        float timer_f = 0f;
        int timer_i = 0;
        int round = 1;
        int roundTime = 10;

        GameObject currentItem;
        GameObject NextItem;

        public Color[] colors = new Color[] { };
        public GameObject[] shapes = new GameObject[] { };

        // Use this for initialization
        void Start()
        {
            CreateObject();
        }

        private void Update()
        {
            timer_f += Time.deltaTime;
            timer_i = (int)timer_f;
            Debug.Log(timer_i);
            Debug.Log("Round " + round);
            if (roundTime - timer_i >= 0) timerController.time = roundTime - timer_i;
            if (timer_i == (roundTime) && round < 10)
            {
                round++;
                roundTime = timer_i + (10 / round);
                CreateObject();
            }
            //if (Input.GetMouseButtonDown(0)) CreateObject();
        }

        public void CreateObject()
        {
            var ran_shape = Random.Range(0, shapes.Length);
            var ran_color = Random.Range(0, colors.Length);

            currentItem = NextItem;
            if (currentItem)
            {
                currentItem.transform.position = new Vector3(0, 8, 0);
                currentItem.AddComponent<Rigidbody2D>();
            }
            //GameObject NextItem = Instantiate(this.shapes[ran_shape]) as GameObject;
            NextItem = Instantiate(this.shapes[ran_shape]);
            NextItem.transform.parent = transform;
            NextItem.transform.position = new Vector3(8, 5, 0);
            Destroy(NextItem.GetComponent<Rigidbody2D>());
            //NextItem.tag = ShapeTags[ran_shape];
            NextItem.GetComponent<SpriteRenderer>().material.color = colors[ran_color];
        }
    }
}