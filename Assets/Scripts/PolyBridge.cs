using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PolyBridge : MonoBehaviour
{

    struct Line
    {
        public int pointA;
        public int pointB;
        public float longueur;

        public Line(int pointA, int pointB, float longueur)
        {
            this.pointA = pointA;
            this.pointB = pointB;
            this.longueur = longueur;
        }
    }

    struct Point
    {
        GameObject go;
        bool immobile;
        
    }

    private List<GameObject> points = new List<GameObject>();
    private List<Line> connections = new List<Line>();

    public GameObject prefab;

    private void Start()
    {
        int count = 0;
        for (int i = 0; i < 11; i+=1)
        {
            GameObject go = GameObject.Instantiate(prefab);
            go.name = "Point " + count++;
            if (i % 2 == 1)
            {
                go.transform.position = new Vector3(i, 1, 0);
            }
            else
            {
                go.transform.position = new Vector3(i, 0, 0);
            }
            points.Add(go);
        }


        for (int i = 0; i < points.Count-1; i+=2)
        {
            connections.Add(new Line(i, i + 1, Vector3.Distance(points[i].transform.position, points[i + 1].transform.position)));

            connections.Add(new Line(i, i + 2, Vector3.Distance(points[i].transform.position, points[i + 2].transform.position)));

            connections.Add(new Line(i + 1, i + 2, Vector3.Distance(points[i + 1].transform.position, points[i + 2].transform.position)));

        }
        



    }

    // Update is called once per frame
    void Update()
    {
        //points[0].transform.position = new Vector3(points[0].transform.position.x , points[0].transform.position.y + 0.001f, points[0].transform.position.z);
    }

    private void OnDrawGizmos()
    {
        
        if (connections != null && points != null)
        {
            if (points.Count > 0)
            {
                Gizmos.color = Color.blue;
                for (int i = 0; i < connections.Count; i+=1)
                {
                    //Gizmos.DrawSphere(listePoints[i], 0.1f);
                    Debug.Log(connections[i].longueur);
                    Gizmos.DrawLine(points[connections[i].pointA].transform.position, points[connections[i].pointB].transform.position);
                }
                Gizmos.color = Color.yellow;
                foreach (GameObject go in points)
                {
                    //Gizmos.DrawSphere(go.transform.position, 0.1f);
                }
            }
        }
    }
}
