using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

namespace LS
{
    public class placeArea : MonoBehaviour
    {
        public static Vector3 position;
        public static double area;

        // Use this for initialization
        void Start()
        {
            position = transform.position;
            if (GetComponent<CircleCollider2D>()) area = CalCircleArea(GetComponent<CircleCollider2D>().radius);
            else if (GetComponent<BoxCollider2D>()) area = CalSquareArea(GetComponent<BoxCollider2D>().size);
            else if (GetComponent<PolygonCollider2D>()) area = CalPolygonArea(GetComponent<PolygonCollider2D>().points);
        }

        private float CalPolygonArea(Vector2[] pts)
        {
            float area = 0;
            for (int i = 0; i < (pts.Length - 1); i++)
                area += ((pts[i + 1].x - pts[i].x) * (pts[i + 1].y + pts[i].y)) / 2;
            area += ((pts[0].x - pts[pts.Length - 1].x) * (pts[0].y + pts[pts.Length - 1].y)) / 2;
            return Math.Abs(area);
        }

        private double CalCircleArea(float radius)
        {
            double area = 0;
            area = Math.PI * radius * radius;
            return area;
        }

        private double CalSquareArea(Vector2 pts)
        {
            double area = 0;
            area = pts.x * pts.y;
            return area;
        }

        void OnTriggerStay2D(Collider2D other) { }
    }
}
