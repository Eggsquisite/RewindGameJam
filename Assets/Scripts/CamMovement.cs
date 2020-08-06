using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamMovement : MonoBehaviour
{
    // This can be used to scroll the map as well
    [SerializeField] float scrollSpeed = 2.0f;
    [SerializeField] float rewindFactor = 1.5f;

    private bool rewindFlag = false;
    private bool pause = false;


    private void OnEnable()
    {
        EndTrigger.onAction += End;
        StartTrigger.onStart += Begin;
    }

    private void OnDisable()
    {
        EndTrigger.onAction -= End;
        StartTrigger.onStart -= Begin;
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

    public void Pause(bool status)
    { 
        pause = status;
    }

    public void Begin(float delay)
    {
        StartTrigger.onStart -= Begin;
        Pause(true);
        Invoke("BeginStart", delay);
    }

    private void BeginStart()
    {
        Pause(false);
    }


    public void End(float delay) {
        EndTrigger.onAction -= End;
        Pause(true);
        Invoke("EndStart", delay);
    }


    private void EndStart()
    {
        Pause(false);
        rewindFlag = true;
    }
}
