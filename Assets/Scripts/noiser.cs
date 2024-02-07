using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class noiser : MonoBehaviour
{
    public LineRenderer lr;
    public int strokes = 50;
    public float radius = 1f;
    public bool generate = false;
    public float updatetime = 0.5f;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (generate)
        {
            generate = false;
            StartCoroutine(gen());
        }
    }
    public IEnumerator gen() 
    {
        while (true) 
        {
            List<Vector3> points = new List<Vector3>();
            for (int i = 0; i < strokes; i++)
            {
                points.Add((transform.position + Random.insideUnitSphere * radius));
                yield return null;
            }
            lr.positionCount = points.Count;
            lr.SetPositions(points.ToArray());
            yield return new WaitForSeconds(updatetime);
        }

    }
}
