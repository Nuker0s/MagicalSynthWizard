using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class Camscript : MonoBehaviour
{
    public PlayerInput pinput;
    private InputAction camswitch;
    public List<Transform> campositions = new List<Transform>();
    private bool switchcams = false;
    public float clickrange;
    public LayerMask leftlayers;
    public LayerMask rightlayers;
    public LayerMask placelayers;
    public Item pickedup;
    private InputAction rightclick;
    private InputAction leftclick;
    private InputAction cycleitempos;
    // Start is called before the first frame update
    void Start()
    {
        rightclick = pinput.actions.FindAction("Rightc");
        leftclick = pinput.actions.FindAction("Leftc");
        camswitch = pinput.actions.FindAction("Camswitch");
        cycleitempos = pinput.actions.FindAction("Cyclepos");
        StartCoroutine(camswitcher());

    }

    // Update is called once per frame
    void Update()
    {
        if (cycleitempos.WasPressedThisFrame())
        {
            if (pickedup!=null)
            {
                pickedup.triggerchangeplace();
            }
        }
        if (camswitch.WasPressedThisFrame())
        {
            //Debug.Log(1);
            switchcams=true;
        }

        if (rightclick.WasPressedThisFrame())
        {
            Rightclick();
        }

        if (leftclick.WasPressedThisFrame()) 
        {
            Leftclick();
        }
    }

    public void Rightclick()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, clickrange,rightlayers))
        {
            Clickable clicked = hit.collider.gameObject.GetComponent<Clickable>();
            if (clicked != null)
            {
                clicked.Rightclick();
            }
        }
    }
    public void Leftclick()
    {
        bool iteminteractet=false;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, clickrange, placelayers))
        {
            

            if (hit.collider.gameObject.tag == "ground" & pickedup != null)
            {
                if (pickedup.currentpos==pickedup.positions.Count-1)
                {
                    pickedup.place(hit.point, hit.normal, hit.transform);
                    //Debug.Log(hit.point);
                    pickedup = null;
                    iteminteractet = true;
                }

            }


        }
        if (Physics.Raycast(ray, out hit, clickrange,leftlayers))
        {
            Clickable clicked = hit.collider.gameObject.GetComponent<Clickable>();
            Item item = hit.collider.gameObject.GetComponent<Item>();
            if (clicked != null)
            {
                clicked.Leftclick();
            }
            if (item != null && iteminteractet ==false)
            {

                if (pickedup == null)
                {
                    Debug.Log(1);
                    item.pickup();
                    pickedup = item;
                    item.onground = false;
                }
            }

        }

    }
    public IEnumerator camswitcher() 
    {
        while (true) 
        {
           
            foreach (Transform item in campositions)
            {
                //Debug.Log(2);
                
                transform.position = item.position;
                transform.rotation = item.rotation;
                
                yield return new WaitUntil(() => switchcams == true);
                switchcams = false;

            }
            yield return null;
        }
    }
}
