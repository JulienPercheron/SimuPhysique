using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PolyBridge : MonoBehaviour
{

    class Line
    {
        public int pointA;
        public int pointB;
        public float longueurRepos;
        public float longueurCourante;

        public Line(int pointA, int pointB, float longueur)
        {
            this.pointA = pointA;
            this.pointB = pointB;
            this.longueurRepos = longueur;
            this.longueurCourante = longueur;
        }
    }

    class Point
    {
        public GameObject go;
        public bool immobile;
        public Vector3 vitesse = Vector3.zero;
        
        public Point(GameObject go, bool immobile)
        {
            this.go = go;
            this.immobile = immobile;
        }
    }

    private List<Point> points = new List<Point>();
    private List<Line> connections = new List<Line>();

    public float gravityForce = 1.0f;
    public float elasticForce = 0.5f;
    public float absorption = 0.5f;

    public GameObject prefabSphere;
    public GameObject prefabCube;

    private void Start()
    {
        int count = 0;
        for (int i = 0; i < 11; i+=1)
        {
            GameObject go;
            if (i % 4 == 0)
            {
                go = GameObject.Instantiate(prefabCube);
            }
            else
            {
                go = GameObject.Instantiate(prefabSphere);
            }
            go.name = "Point " + count++;
            if (i % 2 == 1)
            {
                go.transform.position = new Vector3(i, 1, 0);
            }
            else
            {
                go.transform.position = new Vector3(i, 0, 0);
            }
            if (i % 4 == 0)
            {
                points.Add(new Point(go, true));
            }
            else
            {
                points.Add(new Point(go, false));
            }
        }


        for (int i = 0; i < points.Count-1; i+=2)
        {
            connections.Add(new Line(i, i + 1, Vector3.Distance(points[i].go.transform.position, points[i + 1].go.transform.position)));

            connections.Add(new Line(i, i + 2, Vector3.Distance(points[i].go.transform.position, points[i + 2].go.transform.position)));

            connections.Add(new Line(i + 1, i + 2, Vector3.Distance(points[i + 1].go.transform.position, points[i + 2].go.transform.position)));

        }
        



    }

    // Update is called once per frame
    void Update()
    {
        for(int i = 0; i<points.Count;i++)
        {
            if (!points[i].immobile)
            {
                //Gravity
                Vector3 forces = new Vector3(0, - gravityForce, 0);

                //Update longueurCourante
                for(int j = 0; j<connections.Count;j++)
                {
                    if(connections[j].pointA == i)
                    {
                        connections[j].longueurCourante = Vector3.Distance(points[i].go.transform.position, points[connections[j].pointB].go.transform.position);
                        forces += elasticForce * (connections[j].longueurRepos - connections[j].longueurCourante) * (points[i].go.transform.position - points[connections[j].pointB].go.transform.position);
                    }
                    if (connections[j].pointB == i)
                    {
                        connections[j].longueurCourante = Vector3.Distance(points[connections[j].pointA].go.transform.position, points[i].go.transform.position);
                        forces += elasticForce * (connections[j].longueurRepos - connections[j].longueurCourante) * (points[i].go.transform.position - points[connections[j].pointA].go.transform.position);
                    }
                }

                points[i].vitesse += forces * Time.deltaTime * 5;
                points[i].vitesse *= absorption;
                points[i].go.transform.position += points[i].vitesse * Time.deltaTime;
            }
        }
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
                    Gizmos.DrawLine(points[connections[i].pointA].go.transform.position, points[connections[i].pointB].go.transform.position);
                }
                
            }
        }
    }
}
