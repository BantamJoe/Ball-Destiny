using UnityEngine;
using UnityEngine.UI;

namespace LR
{
    public class scoreController : MonoBehaviour
    {
        public static float score;

        Text text;

        private void Awake()
        {
            text = GetComponent<Text>();
            score = 0;
        }

        // Update is called once per frame
        void Update()
        {
            text.text = "Error: " + score;
        }
    }
}
