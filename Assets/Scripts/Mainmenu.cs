using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mainmenu : MonoBehaviour
{
    public bool starter = false;
    public Rigidbody prb;
    public Piano piano;
    [SerializeField]
    public List<Ncontainer> notes = new List<Ncontainer>();

    [System.Serializable] // Optional, but can be useful for nested classes
    public class Ncontainer
    {
        [SerializeField]
        public Vector3 keydurrspace;
    }
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(menutheme());
    }

    // Update is called once per frame
    void Update()
    {
        if (starter) 
        {
            
            starter = false;
            prb.isKinematic = false;
        }
    }
    public IEnumerator menutheme() 
    {
        yield return new WaitForSeconds(3);
        starter = true;
        while (true)
        {
            foreach (Ncontainer note in notes)
            {
                if (piano.equipped)
                {
                    break;
                }
                if (note.keydurrspace.y <= 0)
                {
                    note.keydurrspace.y = 0.1f;
                }
                if (note.keydurrspace.z <= 0)
                {
                    note.keydurrspace.z = 0.1f;
                }
                Debug.Log(note.keydurrspace.x);
                StartCoroutine(piano.presskey((int)note.keydurrspace.x));
                yield return new WaitForSeconds(note.keydurrspace.y);
                StartCoroutine(piano.releasekey((int)note.keydurrspace.x));
                yield return new WaitForSeconds (note.keydurrspace.z);
            }
            if (piano.equipped)
            {
                break;
            }
        }
        yield return null;
        
        
    }
    
}

