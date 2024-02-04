using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class Camscript : MonoBehaviour
{
    public PlayerInput pinput;
    private InputAction camswitch;
    public List<Transform> campositions = new List<Transform>();
    // Start is called before the first frame update
    void Start()
    {
        camswitch = pinput.actions.FindAction("Camswitch");

    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public IEnumerator camswitcher() 
    {
        while (true) 
        {
            foreach (var item in campositions)
            {
                yield return new WaitUntil(() => camswitch.WasPressedThisFrame() == true);
                transform.position = item.transform.position;
                transform.rotation = item.transform.rotation;
            }
            yield return null;
        }
    }
}
