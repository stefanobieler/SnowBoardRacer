using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnowTrail : MonoBehaviour
{

    private ParticleSystem snowParticleTrail;
    private void Awake()
    {
        snowParticleTrail = GetComponent<ParticleSystem>();

    }
    private void Start()
    {
        GetComponentInParent<SnowBoardController>().PlayerOnGround += OnPlayerOnGround;
    }


    private void OnPlayerOnGround(bool onGround, Vector2 pos)
    {
        if (!snowParticleTrail) return;

        if (onGround)
        {
            transform.position = pos;
            if (!snowParticleTrail.isPlaying)
            {
                snowParticleTrail.Play();
            }
            return;
        }

        snowParticleTrail.Pause();
    }
}

