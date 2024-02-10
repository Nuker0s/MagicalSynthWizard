using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Timeline;

public class Piano : Clickable
{
    public Transform pianoffset;
    public PlayerInput pinput;
    //private List<InputAction> actions = new List<InputAction>();
    private InputActionMap map;
    private InputAction octavekey;

    public Transform keyparent;
    public List<long> pressedkeys = new List<long>();
    public float keydepth;
    public float keyspeed;
    public float gain = 0.01f;
    public float keydebugtimer = 1;
    public Spellcaster spellcaster;
    List<int> octaves = new List<int> { -2, -1, 0, 1, 2 };
    public ChuckSubInstance chucksub;
    public bool switchoctave;
    public int currentoctave = 0;
    public float radius;
    public float maxdist;
    public LayerMask soundhitlayer;
    public bool equipped;
    

    // Start is called before the first frame update
    void Start()
    {
        octavekey = pinput.actions.FindAction("octave");
        foreach (var octave in octaves) 
        {
            //Debug.Log(octave);
        }
        StartCoroutine(octaveswitcher());
        
        chuckkeyboardinit();


        map = pinput.actions.FindActionMap("Keyboard");
        foreach (var action in map)
        {
            action.Enable();
          
        }

    }
    
    // Update is called once per frame
    void Update()
    {
        if (octavekey.WasPressedThisFrame())
        {
            switchoctave = true;
            //Debug.Log("octoswitch");
        }
        chucksub.SetIntArray("keys", pressedkeys.ToArray());
        chucksub.SetFloat("gains", gain);
        keydetector();
        keyanim();
        if (keydebugtimer < 0 )
        {

        }
        else
        {
            if (keydebugtimer<-1)
            {

            }
            else keydebugtimer -= Time.deltaTime;
        }

    }
    public void chuckkeyboardinit() 
    {
        for (int i = 0; i < 5; i++)
        {
            pressedkeys.Add(0);
        }
        chucksub = GetComponent<ChuckSubInstance>();
        
        for (int i = 0; i < 5; i++)
        {
            chucksub.RunFile("chucktest.ck", "" + i);
        }
        chucksub.SetIntArray("keys", pressedkeys.ToArray());
        chucksub.SetInt("octave", currentoctave);
    }
    public IEnumerator octaveswitcher() 
    {
        
        while (true)
        {
            foreach (int octave in octaves)
            {
                currentoctave = octave;
                chucksub.SetInt("octave", octave);
                Debug.Log(octave);
                
                yield return new WaitUntil(() => switchoctave == true);
                switchoctave = false;
            }
            yield return null;
        }
        
    }
    public void keydetector() 
    {
        if (map != null & transform.parent != null)
        {
            for (int i = 0; i < map.actions.Count; i++)
            {
                InputAction action = map.actions[i];
                if (action.WasPressedThisFrame())
                {
                    StartCoroutine(presskey(i));
                    
                }
                if (action.IsPressed())
                {
                    keydebugtimer = 1;
                }
                if (action.WasReleasedThisFrame())
                {
                    StartCoroutine(releasekey(i));
                    
                    

                }

            }

        }
    }
    public void keyanim() 
    {
        foreach (Transform key in keyparent)// Update
        {
            if (pressedkeys[key.GetSiblingIndex()] == 1)
            {
                key.position = new Vector3(key.position.x, math.lerp(key.position.y,
                    key.parent.position.y - keydepth, keyspeed), key.position.z);
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
        pressedkeys[key] = 1;

        soundcast(key);
        spellcaster.keypressed(key);
        
        yield return null;
    }
        
    public IEnumerator releasekey(int key)
    {   
        pressedkeys[key] = 0;
        yield return null;
    }
    public void soundcast(int key)
    {

        RaycastHit[] hits = Physics.SphereCastAll(transform.position, radius, transform.forward, maxdist, soundhitlayer);
        foreach (RaycastHit item in hits)
        {
            NPC npc = item.collider.GetComponent<NPC>();
            if (npc) 
            {
                npc.playerhit(key, 0);
                //Debug.Log(item.collider.gameObject.name);
            }
            
            
        }
    }
    public override void Leftclick()
    {
        if (transform.parent != pianoffset)
        {
            transform.parent = pianoffset;
            transform.position = pianoffset.position;
            transform.rotation = pianoffset.rotation;
            transform.localScale = Vector3.one;
            RenderSettings.fog = false;
            equipped = true;
        }
    }
    private void OnApplicationQuit()
    {
        //chucksub.RunCode(@"Machine.crash();");
        //Debug.Log("quit");
    }
}
