using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleDoor : MonoBehaviour
{

    public Animator animator;
    private bool open = false;
    // Start is called before the first frame update
    void Start()
    {
        if (animator == null)
        {
            animator = gameObject.GetComponent<Animator>();
        }
    }

    // Update is called once per frame
    private void Update()
    {
       // gameObject.GetComponent<ToggleDoor>().toggle();
    }
    void toggle()
    {
        if (animator.GetCurrentAnimatorStateInfo(0).IsName("idle"))
        {
      
            if (!open)
            {
                animator.Play("door open",0,0.0f);
                open = true;

            }
            else
            {
                animator.Play("door close",0,0.0f);
                open = false;
            }

        }
    }
}
