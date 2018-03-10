using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace DT
{
    public class timerController : MonoBehaviour
    {
        public static int time;

        Text text;

        private void Awake()
        {
            text = GetComponent<Text>();
            time = 0;
        }

        // Update is called once per frame
        void Update()
        {
            text.text = time.ToString();
        }
    }
}
