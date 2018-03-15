using System.Linq;
using System.Collections.Generic;
using UnityEngine;

namespace LR{
    public class Line : MonoBehaviour{

        public LineRenderer lineRenderer;
        public EdgeCollider2D edgeCol;
        List<Vector2> points;
        public static List<Vector2> SharePoints = new List<Vector2>();

        public void UpdateLine(Vector2 mousePos){
            if(points == null){
                points = new List<Vector2>();
                SetPoint(mousePos);
                return;
            }

            if (Vector2.Distance(points.Last(), mousePos) > .1f)
                SetPoint(mousePos);
        }

        void SetPoint(Vector2 point){
            points.Add(point);
            SharePoints.Add(point);

            lineRenderer.positionCount = points.Count;
            lineRenderer.SetPosition(points.Count - 1, point);

            if (points.Count > 1)
                edgeCol.points = points.ToArray();
        }

        public void DrawStraightLine(){
            Vector2 lastPos = points.Last();
            Vector2 firstPos = points[0];
            Vector2 currentPos = firstPos;
            
            for (int i = 0; i < points.Count; i++){
                if (currentPos.x <= lastPos.x){
                    lineRenderer.SetPosition(i, currentPos);
                    currentPos.x++;
                    currentPos.y = EquationGetY(firstPos, lastPos, currentPos.x);
                }
                else
                    lineRenderer.SetPosition(i, lastPos);
            }
        }

        private float EquationGetY(Vector2 PosA, Vector2 PosB, float PosX){
            float slope = (PosB.y - PosA.y) / (PosB.x - PosA.x);
            return PosB.y - (slope * (PosB.x - PosX));
        }

        public bool IsXDependent(){
            bool fitline = true;
            for (int i = 0; i < points.Count; i++)
                if (i != 0 && i != (points.Count - 1))
                    if (points[i + 1].x < points[i].x)
                        fitline = false;
            return fitline;
        }

        public bool IsEnoughPoints(){
            return points.Count > 10 ? true : false;
        }
    }
}
