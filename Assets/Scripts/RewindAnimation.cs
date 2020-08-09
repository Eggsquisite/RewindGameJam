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
        else if (!RewindManager.IsRewinding() && rewinding)
        {
            anim.SetFloat("speed", 1f);
            rewinding = false;
        }
        else if (EndTrigger.backtrackBegin && !isEnemy)
        {
            anim.SetFloat("speed", -2f);
        }
    }

    private void OnEnable()
    {
        EndTrigger.onAction += EndGlitch;
    }

    private void OnDisable()
    {
        EndTrigger.onAction -= EndGlitch;
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
