using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlideController : MonoBehaviour {
    public GameObject Panel;
    protected int currentLV;

    private void Start()
    {
        currentLV = 1;
    }

    public void Slide(string action)
    {
        float Index = 0;
        float distance = 0;

        Transform[] Levels = GetAllLevels();
        
        if(Levels.Length > 4)
        {
            distance = Vector3.Distance(Levels[1].transform.position, Levels[3].transform.position);    // Get the distance between two panel Level button

            if (action == "prev" && currentLV > 1) { Index = distance; currentLV--; }
            else if (action == "next" && currentLV < (Levels.Length - 1) / 2) { Index = -distance; ; currentLV++; }
            else Index = 0;

            if (Index != 0)
            {
                for (int i = 1; i < Levels.Length; i++)
                {
                    Vector3 pos = Levels[i].transform.position;
                    if (i % 2 == 1)
                        Levels[i].position = new Vector3(pos.x + Index, pos.y, pos.z);
                }
            }
        }
    }

    private Transform[] GetAllLevels(){
        return Panel.GetComponentsInChildren<Transform>();
    }
}
