using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace LS
{
    public class gameController : MonoBehaviour
    {
        float timer_f = 0f;
        int timer_i = 0;

        public GameObject placeHolder;
        
        void Update(){
            timer_f += Time.deltaTime;
            timer_i = (int)timer_f;
            timerController.time = timer_i;
            Scoring();
        }

        void Scoring(){
            //Debug.Log("placeArea.area: " + placeArea.area);
            //Debug.Log("Percentage: " + 100* GetTotalArea(GetInsideTheArea())/placeArea.area);
            scoreController.percentage = (100 * GetTotalArea(GetInsideTheArea()) / placeArea.area);
        }

        private double GetTotalArea(List<GameObject> insidePlaceArea){
            double area = 0;
            //Debug.Log("Count " + insidePlaceArea.Count);
            scoreController.count = insidePlaceArea.Count;
            foreach (GameObject ins in insidePlaceArea){
                if (ins.GetComponent<CircleCollider2D>()) area += CalCircleArea(ins.GetComponent<CircleCollider2D>().radius);
                else if (ins.GetComponent<BoxCollider2D>()) area += CalSquareArea(ins.GetComponent<BoxCollider2D>().size);
                else if (ins.GetComponent<PolygonCollider2D>()) area += CalPolygonArea(ins.GetComponent<PolygonCollider2D>().points);
            }
            return area;
        }

        private List<GameObject> GetInsideTheArea(){
            string[] tags = {
                "circle",
                "square",
                "triangle"
            };
            
            List<GameObject> insidePlaceArea = new List<GameObject>();
            GameObject[] circle, square, triangle;
            circle = GameObject.FindGameObjectsWithTag("circle");
            square = GameObject.FindGameObjectsWithTag("square");
            triangle = GameObject.FindGameObjectsWithTag("triangle");

            foreach (GameObject sq in square)
                if (SquareInsideArea(sq.GetComponent<Collider2D>())) insidePlaceArea.Add(sq);
            foreach (GameObject tr in triangle)
                if (PolygonInsideArea(tr.GetComponent<Collider2D>())) insidePlaceArea.Add(tr);
            foreach (GameObject cr in circle)
                if (CircleInsideArea(cr.GetComponent<Collider2D>())) insidePlaceArea.Add(cr);

            return insidePlaceArea;
        }





        /*=====================================================================================*/
        private bool PolygonInsideArea(Collider2D item){
            bool inside = true;

            Vector2[] pts = item.GetComponent<PolygonCollider2D>().points;
            for (int i = 0; i < pts.Length; i++){
                Vector2 worldSpacePoint = item.transform.TransformPoint(pts[i]);
                Vector3 OnePt = new Vector3(worldSpacePoint.x, worldSpacePoint.y, placeHolder.transform.position.z);
                if (!IsInsideArea(OnePt))
                    inside = false;
            }
            return inside;
        }

        private bool SquareInsideArea(Collider2D item){
            bool inside = false;

            Vector2 size = item.GetComponent<BoxCollider2D>().bounds.size;
            Vector2 max = item.GetComponent<BoxCollider2D>().bounds.max;
            Vector2 min = item.GetComponent<BoxCollider2D>().bounds.min;

            Vector3 TopRight = new Vector3(max.x, max.y, placeHolder.transform.position.z);
            Vector3 BottomLeft = new Vector3(min.x, min.y, placeHolder.transform.position.z);
            Vector3 TopLeft = new Vector3(min.x, min.y + size.y, placeHolder.transform.position.z);
            Vector3 BottomRight = new Vector3(max.x, max.y - size.y, placeHolder.transform.position.z);

            if (IsInsideArea(TopRight) && IsInsideArea(BottomLeft) && IsInsideArea(TopLeft) && IsInsideArea(BottomRight))
                inside = true;
            return inside;
        }

        private bool CircleInsideArea(Collider2D item){
            bool inside = true;
            
            Vector2 ctr = item.GetComponent<CircleCollider2D>().bounds.center;
            float r = item.GetComponent<CircleCollider2D>().radius;
            float angle = (float)(Math.PI * 45 / 180.0);
            float len = (float)(r * Math.Sin(angle));
            Vector3[] points ={
                new Vector3(ctr.x+r,ctr.y, placeHolder.transform.position.z),
                new Vector3(ctr.x,ctr.y+r, placeHolder.transform.position.z),
                new Vector3(ctr.x-r,ctr.y, placeHolder.transform.position.z),
                new Vector3(ctr.x,ctr.y-r, placeHolder.transform.position.z),

                new Vector3(ctr.x+len,ctr.y+len, placeHolder.transform.position.z),
                new Vector3(ctr.x+len,ctr.y-len, placeHolder.transform.position.z),
                new Vector3(ctr.x-len,ctr.y-len, placeHolder.transform.position.z),
                new Vector3(ctr.x-len,ctr.y+len, placeHolder.transform.position.z)
            };
            for (int i = 0; i < points.Length; i++)
                if (!IsInsideArea(points[i]))
                    inside = false;
            return inside;
        }
        /*=====================================================================================*/





        /*=====================================================================================*/
        // Determine whether the point in the area based on Shape of Place Holder 
        private bool IsInsideArea(Vector3 point)
        {
            bool inside = false;
            if (placeHolder.GetComponent<CircleCollider2D>()) inside = IsPointInCircle(point, placeHolder.GetComponent<CircleCollider2D>().radius, placeHolder.GetComponent<CircleCollider2D>().bounds.center);
            else if (placeHolder.GetComponent<BoxCollider2D>()) inside = placeHolder.GetComponent<SpriteRenderer>().bounds.Contains(point);
            else if (placeHolder.GetComponent<PolygonCollider2D>()) inside = IsPointInPolygon(point, placeHolder.GetComponent<PolygonCollider2D>().points);
            return inside;
        }
        /*=====================================================================================*/





        /*=====================================================================================*/
        public bool IsPointInPolygon(Vector2 point, Vector2[] polygon)
        {
            int polygonLength = polygon.Length, i = 0;
            bool inside = false;

            float pointX = point.x, pointY = point.y;

            float startX, startY, endX, endY;
            Vector2 endPoint = polygon[polygonLength - 1];
            endX = endPoint.x;
            endY = endPoint.y;
            while (i < polygonLength)
            {
                startX = endX; startY = endY;
                endPoint = polygon[i++];
                endX = endPoint.x; endY = endPoint.y;

                inside ^= (endY > pointY ^ startY > pointY) && ((pointX - endX) < (pointY - endY) * (startX - endX) / (startY - endY));
            }
            return inside;
        }
        
        public bool IsPointInCircle(Vector2 point, float radius, Vector2 center)
        {
            bool inside = false;

            float x = (point.x - center.x) * (point.x - center.x);
            float y = (point.y - center.y) * (point.y - center.y);
            float L = x + y;
            float R = radius * radius;
            if (L < R)
                inside = true;
            return inside;
        }
        /*=====================================================================================*/





        /*=====================================================================================*/
        private float CalPolygonArea(Vector2[] pts){
            float area = 0;
            for (int i = 0; i < (pts.Length - 1); i++)
                area += ((pts[i + 1].x - pts[i].x) * (pts[i + 1].y + pts[i].y)) / 2;
            area += ((pts[0].x - pts[pts.Length - 1].x) * (pts[0].y + pts[pts.Length - 1].y)) / 2;
            return Math.Abs(area);
        }

        private double CalCircleArea(float radius){
            double area = 0;
            area = Math.PI * radius * radius;
            return area;
        }

        private double CalSquareArea(Vector2 pts){
            double area = 0;
            area = pts.x * pts.y;
            return area;
        }
        /*=====================================================================================*/
    }
}
