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
    public float animspeed = 1;
    // Start is called before the first frame update
    void OnValidate()
    {
        // Your code to run when the prefab is placed in edit mode
        gen();
    }
    IEnumerator Start()
    {
        gen();
        while (true) 
        {
            
            if (regen)
            {
                gen();
            }
            yield return new WaitForSeconds(animspeed);

        }
    }

    // Update is called once per frame
    void Update()
    {
        /*if (regen)
        {
            regen = false;
            gen();
        }*/
    }
    public void gen() 
    {
        lr.positionCount = segments;
        float sheight = height / segments;
        float currheight = 0;
        lr.SetPosition(0, transform.position);
        for (int i = 1; i < segments; i++)
        {
            currheight += sheight;
            lr.SetPosition(i, transform.position + new Vector3(Random.Range(-range,range),currheight, Random.Range(-range, range)));
            
        }
    }
    
}
