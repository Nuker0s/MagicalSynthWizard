using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class impactplayer : MonoBehaviour
{
    public AudioSource source;
    public AudioClip clip;
    public float treshold = 5f;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    private void OnCollisionEnter(Collision collision)
    {
        
        if (collision.impulse.magnitude > treshold)
        {
            AudioSource.PlayClipAtPoint(clip, collision.GetContact(0).point);
            AudioListener.volume = 0.5f;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
