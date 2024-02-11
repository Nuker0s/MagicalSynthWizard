using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;

public class godanim : MonoBehaviour
{

    public Transform kingdeix;
    public float startheight;
    public float kingbobamount = 3;
    public float sandsoftime = 0;
    public Transform thorniculus;
    public float rotatespeed;
    public Transform unnamed;
    // Start is called before the first frame update
    void Start()
    {
        startheight = kingdeix.position.y;
    }

    // Update is called once per frame
    void Update()
    {
        kingdeix.position = new Vector3(kingdeix.position.x,startheight + math.sin(Time.time) * kingbobamount ,kingdeix.position.z);
        if (sandsoftime >= 360)
        {
            sandsoftime = 0;
        }
        //unnamed.rotation = Quaternion.LookRotation((Camera.main.transform.position - unnamed.position).normalized);
    }
    private void FixedUpdate()
    {
        thorniculus.Rotate(rotatespeed, rotatespeed, rotatespeed);

    }
}
