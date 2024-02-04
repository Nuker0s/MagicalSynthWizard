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
    // Start is called before the first frame update
    void Start()
    {
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
