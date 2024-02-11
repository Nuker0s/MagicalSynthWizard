using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;


public class NPC : Clickable
{
    public int octave = 0;
    public float beatspace= 0.6f;
    //public List<int> notes = new List<int>();
    public List<int> playernotes = new List<int>();
    public float notetime = 1;
    public Transform anim;
    public List<Transform> animpoints = new List<Transform>();
    public UnityEvent OnGoodCode;
    public UnityEvent OnBadCode;
    private bool istalking = false;
    private int lastpos = 0;
    public ChuckSubInstance chucksub;
    public int level = 0;
    [SerializeField]
    public List<Codecont> codes = new List<Codecont>();


    [System.Serializable] // Optional, but can be useful for nested classes
    public class Codecont
    {
        [SerializeField]
        public List<int> notes;
    }
    void Start()
    {
        chucksub = GetComponent<ChuckSubInstance>();
    }
    public virtual void playerhit(int note, int octave) 
    {
       // Debug.Log(note);
        playernotes.Add(note);
        if (playernotes.Count == codes[level].notes.Count) 
        {
            bool good = true;
            for (int i = 0; i < codes[level].notes.Count; i++)
            {
                if (playernotes[i] == codes[level].notes[i])
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
    public override void Leftclick() 
    {
        if (!istalking)
        {
            StartCoroutine(playsequence());
            playernotes.Clear();
        }
    }
    public virtual IEnumerator playsequence() 
    {
        
        istalking= true;
        foreach (int note in codes[level].notes)
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
        OnGoodCode.Invoke();
    }

    public virtual void goodcode() 
    {
        level++;
        if (level >= codes.Count)
        {
            level = codes.Count;
        }
        chucksub.RunFile("goodcode.ck");
        Debug.Log("good");
        StartCoroutine(uhhuh());
        OnGoodCode.Invoke();
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
        float waittime = 0.3f;
        anim.Rotate(30, 0, 0);
        yield return new WaitForSeconds(waittime);
        anim.Rotate(-30, 0, 0);
        yield return new WaitForSeconds(waittime);
        anim.Rotate(30, 0, 0);
        yield return new WaitForSeconds(waittime);
        anim.Rotate(-30, 0, 0);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}