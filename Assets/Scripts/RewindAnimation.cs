using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RewindAnimation : MonoBehaviour
{

    Animator anim;

    private void Start()
    {
        anim = GetComponent<Animator>();
    }

    private void OnEnable()
    {
        EndTrigger.onAction += EndGlitch;
    }

    private void OnDisable()
    {
        EndTrigger.onAction -= EndGlitch;
    }

    private void Rewind(float rewindFactor)
    {
        anim.SetFloat("rewind", rewindFactor);
    }

    private void EndGlitch(float delay)
    {
        Invoke("GlitchAnim", delay);
    }

    private void GlitchAnim()
    { 
        anim.SetTrigger("glitch");
    }
}
