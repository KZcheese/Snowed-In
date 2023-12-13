using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleDoor : MonoBehaviour
{

    public Animator animator;
    public bool locked = false;
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
    public void toggle()
    {
        if (!locked)
        {
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
