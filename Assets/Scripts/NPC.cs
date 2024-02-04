using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;


public class NPC : MonoBehaviour
{
    public int octave = 0;
    public float beatspace= 0.6f;
    public List<int> notes = new List<int>();
    public List<int> playernotes = new List<int>();
    public UnityEvent OnGoodCode;
    public UnityEvent OnBadCode;
    void Start()
    {
        
    }
    public virtual void playerhit(int note, int octave) 
    {
        Debug.Log(note);
        playernotes.Add(note);
        if (playernotes.Count == notes.Count) 
        {
            bool good = true;
            for (int i = 0; i < notes.Count; i++)
            {
                if (playernotes[i] == notes[i])
                {

                }
                else { good = false; break; }
            }
            if (good) 
            {
                goodcode();
                playernotes.Clear();
            }
            else
            {
                wrongcode();
                playernotes.Clear();

            }

        }
    }
    public virtual void wrongcode() 
    {
        Debug.Log("wrong");
    }
    public virtual void goodcode() 
    {
        Debug.Log("good");
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}