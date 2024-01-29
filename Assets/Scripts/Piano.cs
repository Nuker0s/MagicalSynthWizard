using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEngine.Rendering.DebugUI;

public class Piano : MonoBehaviour
{
    public PlayerInput pinput;
    private List<InputAction> actions = new List<InputAction>();
    private InputActionMap map;
    public Transform keyparent;
    public List<int> pressedkeys = new List<int>();
    public float keydepth;
    public float keyspeed;
    private float increment;
    public float frequency = 220;
    public float gain = 0.01f;
    public float sampfreq;
    private float phase;
    public float time;
    private void OnAudioFilterRead(float[] data, int channels)
    {

        increment = frequency * 2 * Mathf.PI / sampfreq;
        for (int i = 0; i < data.Length; i += channels)
        {

            phase += increment;

            
            
                data[i] = Mathf.Sin(frequency*time) * gain;
            


            if (channels == 2)
            {
                data[i + 1] = data[i];
            }
        }
        if (phase > 30000)
        {
            phase = increment;
        }
        /*frequency += deltatime * 20;
        if (frequency>880)
        {
            frequency = 440;
        }*/


    }
    // Start is called before the first frame update
    void Start()
    {
        map = pinput.actions.FindActionMap("Keyboard");
        foreach (var action in map)
        {
            action.Enable();
            //Debug.Log(action.name + " phase: " + action.phase);
        }
        //keyboard = pinput.actions.FindAction("Keyboard");
    }

    // Update is called once per frame
    void Update()
    {
        if (map != null)
        {
            for (int i = 0; i < map.actions.Count; i++)
            {
                InputAction action = map.actions[i];
                if (action.WasPressedThisFrame())
                {
                    StartCoroutine(presskey(i));
                }
                if (action.WasReleasedThisFrame())
                {
                    StartCoroutine(releasekey(i));
                }

            }

        }
        foreach (Transform key in keyparent)// Update
        {
            if (pressedkeys.Contains(key.GetSiblingIndex()))
            {
                key.position = new Vector3(key.position.x,math.lerp(key.position.y, 
                    key.parent.position.y - keydepth,keyspeed) ,key.position.z);
            }
            else
            {
                key.position = new Vector3(key.position.x, math.lerp(key.position.y, 
                    key.parent.position.y, keyspeed), key.position.z);
            }
        }

    }
    public IEnumerator presskey(int key)
    {
        pressedkeys.Add(key);    
        yield return null;
    }
        
    public IEnumerator releasekey(int key)
    {
        
        pressedkeys.Remove(key);
        yield return null;
    }
}
