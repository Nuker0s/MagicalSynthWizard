using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;


public class NPC : MonoBehaviour
{
    public int octave = 0;
    public float beatspace= 0.6f;
    public List<int> notes = new List<int>();
    public List<int> playernotes = new List<int>();
    public float notetime = 1;
    public Transform anim;
    public List<Transform> animpoints = new List<Transform>();
    public UnityEvent OnGoodCode;
    public UnityEvent OnBadCode;
    private bool istalking = false;
    private int lastpos = 0;
    public ChuckSubInstance chucksub;

    void Start()
    {
        chucksub = GetComponent<ChuckSubInstance>();
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
    private void OnMouseDown()
    {
        playerclick();
    }
    public virtual void playerclick() 
    {
        if (!istalking)
        {
            StartCoroutine(playsequence());
        }
    }
    public virtual IEnumerator playsequence() 
    {
        
        istalking= true;
        foreach (int note in notes)
        {
            int randpos = Random.Range(1, animpoints.Count);
            while (randpos==lastpos)
            {
                randpos = Random.Range(1, animpoints.Count);
            }
            
            anim.position = animpoints[randpos].position;
            anim.rotation = animpoints[randpos].rotation;
            lastpos = randpos;
            playnote(note, 0);
            yield return new WaitForSeconds(notetime);
        }
        istalking = false;
        anim.position = animpoints[0].position;
        anim.rotation = animpoints[0].rotation;
    }
    public virtual void playnote(int note,int octave) 
    {
        chucksub.RunFile("hcrab.ck", ""+note);
    }
    public virtual void wrongcode() 
    {
        Debug.Log("wrong");
        chucksub.RunFile("badcode.ck");
        StartCoroutine(nuhuh());
    }

    public virtual void goodcode() 
    {
        chucksub.RunFile("goodcode.ck");
        Debug.Log("good");
        StartCoroutine(uhhuh());
    }
    public virtual IEnumerator nuhuh()
    {
        anim.Rotate(0, -45, 0);
        yield return new WaitForSeconds(0.5f);
        anim.Rotate(0, 45, 0);
        anim.Rotate(0, 45, 0);
        yield return new WaitForSeconds(0.5f);
        anim.Rotate(0, -45, 0);
    }
    public virtual IEnumerator uhhuh()
    {
        anim.Rotate(30, 0, 0);
        yield return new WaitForSeconds(0.5f);
        anim.Rotate(-30, 0, 0);
        yield return new WaitForSeconds(0.5f);
        anim.Rotate(30, 0, 0);
        yield return new WaitForSeconds(0.5f);
        anim.Rotate(-30, 0, 0);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}