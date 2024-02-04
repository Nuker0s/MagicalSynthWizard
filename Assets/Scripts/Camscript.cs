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
    private InputAction rightclick;
    private InputAction leftclick;
    // Start is called before the first frame update
    void Start()
    {
        rightclick = pinput.actions.FindAction("Rightc");
        leftclick = pinput.actions.FindAction("Leftc");
        camswitch = pinput.actions.FindAction("Camswitch");
        StartCoroutine(camswitcher());

    }

    // Update is called once per frame
    void Update()
    {
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
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, clickrange,leftlayers))
        {
            Clickable clicked = hit.collider.gameObject.GetComponent<Clickable>();
            if (clicked != null)
            {
                clicked.Leftclick();
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
