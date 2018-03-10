using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DT
{
    public class decisionBlock : MonoBehaviour
    {
        private bool isTrue;
        
        private void OnTriggerEnter2D(Collider2D other) { }

        private void Update() { }

        void OnMouseDown()
        {
            transform.rotation = transform.rotation == Quaternion.Euler(0, 0, 45) ? Quaternion.Euler(0, 0, -45) : Quaternion.Euler(0, 0, 45);
        }
    }
}
