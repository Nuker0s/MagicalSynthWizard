using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Events;


public class Playcorder : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip clip;

    void Start()
    {

        audioSource = GetComponent<AudioSource>();
        audioSource.clip = clip;
    }

    public void Play()
    {
        //Debug.Log("play");
        if (audioSource.isPlaying)
        {
            //audioSource.Stop();
            audioSource.Pause();
            
        }
        else
        {
            audioSource.Play();
            
        }
        
    }

    public void Stop()
    {
        
    }

    public void Forward()
    {
        audioSource.time = math.clamp( audioSource.time + clip.length/5,0.01f,audioSource.clip.length - 0.01f); // Forward by 10 seconds (can adjust as needed)
    }

    public void Rewind()
    {
        audioSource.time = math.clamp(audioSource.time - clip.length / 5, 0.01f, audioSource.clip.length - 0.01f); // Rewind by 10 seconds (can adjust as needed)
    }
}
