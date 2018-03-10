using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LS
{
    public class DrapandDropController : MonoBehaviour
    {  
        private void OnMouseDrag(){
            float distance = 10;
            Vector3 mousePosition = new Vector3(Input.mousePosition.x, Input.mousePosition.y, distance);
            Vector3 objPosition = Camera.main.ScreenToWorldPoint(mousePosition);
            transform.position = objPosition;
        }
    }
}
