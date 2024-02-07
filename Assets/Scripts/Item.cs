using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using UnityEngine;
using UnityEngine.Events;

public class Item : Clickable
{
    public bool onground = true;
    public List<Transform> positions;
    public UnityEvent OnPlace;
    public UnityEvent OnPickUp;
    public int currentpos = 1;
    private bool changeplace = false;
    // Start is called before the first frame update
    

    public override void Leftclick()
    {
        //pickup();

        
    }
    public void triggerchangeplace() 
    {
        changeplace = true;
    }
    public virtual void pickup() 
    {
        Transform cam = Camera.main.transform;
        transform.parent.position = cam.position + cam.forward * 1;
        transform.parent.rotation = Quaternion.LookRotation(-cam.forward);
        transform.parent.parent = cam;
        Debug.Log(7);
        onground = false;

        Debug.Log(this.positions.Count);
        
        //aligntransforms(transform, this.positions[0]);
        StartCoroutine(fixpos());
        OnPickUp.Invoke();
    }
    public void place(Vector3 pos,Vector3 normal,Transform surface)
    {
        transform.parent.position = pos;
        transform.parent.rotation = Quaternion.LookRotation(normal);
        transform.rotation = transform.parent.rotation;
        Debug.Log(pos);
        //transform.parent.parent = surface;
        transform.parent.parent = null;
        onground = true;
        OnPlace.Invoke();
        aligntransforms(transform, this.positions[0]);
    }
    public IEnumerator fixpos()
    {
        while (currentpos != 0)
        {
            changeplace = true;
            yield return null;
        }
    }
    public void aligntransforms(Transform t1,Transform target)
    {
        t1.position = target.position;
        t1.rotation = target.rotation;
    }
    public IEnumerator placechanger() 
    {
        while (true)
        {
            for (int i = 0; i < positions.Count; i++)
            {
                yield return new WaitUntil(() => changeplace == true);
                aligntransforms(transform, positions[i]);
                currentpos = i;
                changeplace = false;
            }
            yield return null;
        }
    }
    void Start()
    {
        StartCoroutine(placechanger());  
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
