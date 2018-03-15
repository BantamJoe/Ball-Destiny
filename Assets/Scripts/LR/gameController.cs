using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace LR
{
    public class gameController : MonoBehaviour
    {
        public int weighting;
        private int ShareNumOfShape;
        
        void Update(){
            scoring();  // Calculate the final score
        }
        
        void scoring(){
            List<Vector2> nodeList = shape.posList;         // List of the position of nodes
            List<Vector2> points = Line.SharePoints;        // List of the position of Line points
            ShareNumOfShape = shape.ShareNumOfShape;
            if (points.Count > 2 && GameObject.FindGameObjectWithTag("Line")){
                Vector2 lastPot = points.Last();
                Vector2 firstPot = points[0];
                float error = 0;

                if (points.Count > 0)
                    for (int i = 0; i < nodeList.Count; i++)
                        error += GetError(nodeList[i], firstPot, lastPot);
                int FinalScore = weighting * (ShareNumOfShape - shape.ExistedShape) + ((100 - (int)error)*2);
                scoreController.score = FinalScore;
            }
            else
                scoreController.score = 0;
        }

        float GetError(Vector2 node, Vector2 firstPot, Vector2 lastPot){
            return Math.Abs(node.y - EquationGetY(firstPot, lastPot, node.x));
        }

        private float EquationGetY(Vector2 PosA, Vector2 PosB, float PosX){
            float slope = (PosB.y - PosA.y) / (PosB.x - PosA.x);
            return PosB.y - (slope * (PosB.x - PosX));
        }

    }
}
