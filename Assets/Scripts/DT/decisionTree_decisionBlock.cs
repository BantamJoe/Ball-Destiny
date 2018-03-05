using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class decisionTree_decisionBlock : MonoBehaviour
{
    private bool isTrue;

    private void Start()
    {
        //this.isTrue = true;
        //this.GetComponent<SpriteRenderer>().material.color = colors[1];
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        /*var pos = this.gameObject.transform.position;
        var x = pos.x;
        var y = pos.y;
        var z = pos.z;
        if (this.isTrue) other.gameObject.transform.position = new Vector3(x+3, y ,z);
        else other.gameObject.transform.position = new Vector3(x-3, y, z);*/
        /*if (other.gameObject.tag == shape)
        {
            Debug.Log("Checked");
            Destroy(other.gameObject);
        }*/
    }

    private void Update() {}

    void OnMouseDown(){
        transform.rotation = transform.rotation == Quaternion.Euler(0, 0, 45) ? Quaternion.Euler(0, 0, -45) : Quaternion.Euler(0, 0, 45);
        //isTrue = !isTrue;
        //transform.position = isTrue ? new Vector3(transform.position.x-1, transform.position.y, transform.position.z) : new Vector3(transform.position.x+1, transform.position.y, transform.position.z);
        //this.GetComponent<SpriteRenderer>().material.color = isTrue ? colors[0] : colors[1];
    }
}
