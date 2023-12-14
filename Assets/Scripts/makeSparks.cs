using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class makeSparks : Clickable
{
    public AudioClip soundClip;
    private AudioSource audioSource;
    
    // Start is called before the first frame update
    void Start()
    {

        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }
    }
    
    public override void onClick()
    {
        
        ParticleSystem sparks = gameObject.GetComponentInChildren<ParticleSystem>();
        if (!sparks.isPlaying || sparks.time > .2f)
        {
            // Play locked sound if the door is locked
            if (soundClip != null)
            {
                audioSource.PlayOneShot(soundClip);
            }
            sparks.time = 0.0f;
            gameObject.GetComponentInChildren<ParticleSystem>().Play();
        }

    }
}
