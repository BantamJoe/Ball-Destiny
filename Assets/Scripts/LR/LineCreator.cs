using UnityEngine;

namespace LR
{
    public class LineCreator : MonoBehaviour
    {
        public GameObject linePrefab;
        public static Line activeLine;
        private int once = 0;
        public static bool FinishDraw = false;

        void Update(){
            if (Input.GetMouseButtonDown(0) && once == 0){
                GameObject lineGo = Instantiate(linePrefab);
                lineGo.tag = "Line";
                activeLine = lineGo.GetComponent<Line>();
            }

            if (Input.GetMouseButtonUp(0)){
                if (activeLine.IsXDependent() && activeLine.IsEnoughPoints()){
                    activeLine.DrawStraightLine();
                    FinishDraw = true;
                    activeLine = null;
                    once++;
                }
                else
                    Destroy(GameObject.FindGameObjectWithTag("Line"));
            }

            if (activeLine != null){
                Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                activeLine.UpdateLine(mousePos);
            }
        }
    }
}
