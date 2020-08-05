﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamMovement : MonoBehaviour
{
    // This can be used to scroll the map as well
    [SerializeField] float scrollSpeed = 2.0f;
    [SerializeField] float rewindFactor = 1.5f;
    [SerializeField] float rewindDelay = 1f;

    private bool rewindFlag = false;
    private bool pause = false;


    private void OnEnable()
    {
        EndTrigger.onAction += Rewind;
    }

    private void OnDisable()
    {
        EndTrigger.onAction -= Rewind;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (!pause)
        {
            if (rewindFlag)
                transform.Translate(Vector3.left * Time.deltaTime * scrollSpeed * rewindFactor);
            else
                transform.Translate(Vector3.right * Time.deltaTime * scrollSpeed);
        }
    }

    public void Rewind() {
        EndTrigger.onAction -= Rewind;
        StartCoroutine(EndStart());
    }

    private IEnumerator EndStart()
    {
        pause = true;
        yield return new WaitForSeconds(rewindDelay);
        pause = false;
        rewindFlag = true;
    }
}
