using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

namespace LS
{
    public class scoreController : MonoBehaviour
    {
        public static double percentage;
        public static int count;

        Text text;

        private void Awake()
        {
            text = GetComponent<Text>();
            percentage = 0;
            count = 0;
        }

        // Update is called once per frame
        void Update(){
            text.text = "Percentage: " + Math.Round(percentage, 2).ToString() + "%\nCount: " + count.ToString();
        }
    }
}
