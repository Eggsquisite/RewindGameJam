using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RewindAnimation : MonoBehaviour
{

    Animator anim;
    public bool isEnemy = false;
    private bool rewinding = false;

    private void Start()
    {
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        if (RewindManager.IsRewinding() && !rewinding)
        {
            anim.SetFloat("speed", -2f);
            rewinding = true;
        }
        else if (!RewindManager.IsRewinding() && rewinding) {
            //Debug.Log("Changing speed for anim " + gameObject.name);
            anim.SetFloat("speed", 1f);
            rewinding = false;
        }
        else if (EndTrigger.backtrackBegin && !isEnemy)
        {
            //Debug.Log("Changing speed for anim " + gameObject.name);
            anim.SetFloat("speed", -2f);
        }
    }

    private void OnEnable()
    {
        EndTrigger.BackTrackTriggered += EndGlitch;
    }

    private void OnDisable()
    {
        EndTrigger.BackTrackTriggered -= EndGlitch;
    }

    private void EndGlitch(float delay)
    {
        Invoke("GlitchAnim", delay);
    }

    private void GlitchAnim()
    { 
        if (!isEnemy)
            anim.SetTrigger("lit");
    }
}
