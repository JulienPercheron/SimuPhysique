using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateGO : MonoBehaviour
{
    private List<GameObject> goList = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        for(int x = 0; x < 1000; x++)
        {
            for (int z = 0; z < 100; z++)
            {
                GameObject sphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
                sphere.transform.position = new Vector3(x, 0, z);
                sphere.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
                goList.Add(sphere);
            }
        }
    }

    private void Update()
    {
        int time = Time.frameCount; 
        for(int i = 0; i < goList.Count;i++)
        {
            //goList[i].transform.position = new Vector3(goList[i].transform.position.x, /*Mathf.Sin(i*time)*/goList[i].transform.position.y, goList[i].transform.position.z);
        }
    }

}
