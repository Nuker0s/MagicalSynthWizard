using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Item : Clickable
{
    public bool onground = true;
    public List<Transform> positions = new List<Transform>();
    public UnityEvent OnPlace;
    public UnityEvent OnPickUp;
    // Start is called before the first frame update
    
    public override void Leftclick()
    {
        pickup();

        
    }
    public void pickup() 
    {
        Transform cam = Camera.main.transform;
        transform.parent.position = cam.position + cam.forward * 1;
        transform.parent.rotation = Quaternion.LookRotation(-cam.forward);
        transform.parent.parent = cam;
        onground = false;
        aligntransforms(transform, positions[0]);
        OnPickUp.Invoke();
    }
    public void place(Vector3 pos,Vector3 normal,Transform surface)
    {
        transform.parent.position = pos;
        transform.parent.rotation = Quaternion.LookRotation(normal);
        Debug.Log(pos);
        //transform.parent.parent = surface;
        transform.parent.parent = surface;
        onground = true;
        OnPlace.Invoke();
    }
    public void aligntransforms(Transform t1,Transform target)
    {
        t1.position = target.position;
        t1.rotation = target.rotation;
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
