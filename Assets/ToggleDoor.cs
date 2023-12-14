using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleDoor : Clickable
{

    public Animator animator;
    public bool locked = false;
    public AudioClip doorSoundClip;
    private AudioSource audioSource;
    // Start is called before the first frame update
    void Start()
    {
        if (animator == null)
        {
            animator = gameObject.GetComponent<Animator>();
        }

        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }
    }

    // Update is called once per frame
    private void Update()
    {
       // gameObject.GetComponent<ToggleDoor>().toggle();
    }
    public override void onClick()
    {
        toggle();
    }

    private void toggle()
    {
        if (locked){
            // Play locked sound if the door is locked
            if (doorSoundClip != null)
            {
                audioSource.PlayOneShot(doorSoundClip);
            }
        }
        else{
            if (doorSoundClip != null)
            {
                audioSource.PlayOneShot(doorSoundClip);
            }

            if (animator.GetCurrentAnimatorStateInfo(0).IsName("closed"))
            {

                animator.Play("door open", 0, 0.0f);

            }
            else if (animator.GetCurrentAnimatorStateInfo(0).IsName("open"))
            {
                animator.Play("door close", 0, 0.0f);

            }
        }
    }
}
