using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class makeSparks : Clickable
{
    public override void onClick()
    {
        ParticleSystem sparks = gameObject.GetComponentInChildren<ParticleSystem>();
        if (!sparks.isPlaying || sparks.time > .2f)
        {
            sparks.time = 0.0f;
            gameObject.GetComponentInChildren<ParticleSystem>().Play();
        }

    }
}
