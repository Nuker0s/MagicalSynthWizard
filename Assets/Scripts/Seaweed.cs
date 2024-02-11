using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Seaweed : MonoBehaviour
{
    public LineRenderer lr;
    public float height = 5f;
    public int segments = 5;
    public float range = 0.5f;
    public bool regen = true;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void gen() 
    {
        lr.positionCount = segments;
        float sheight = height / segments;
        for (int i = 0; i < segments; i++)
        {
            lr.SetPosition
        }
    }
}
