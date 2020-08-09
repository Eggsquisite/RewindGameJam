using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamMovement : MonoBehaviour
{
    // This can be used to scroll the map as well
    [SerializeField] float scrollSpeed = 2.0f;
    [SerializeField] float rewindFactor = 1.5f;
    [SerializeField] float smoothSpeed = 1f;

    [Header("Clamp Values")]
    //[SerializeField] float min_X = 0f;
    [SerializeField] float max_X = 0f;
    [SerializeField] float min_Y = 0f;
    [SerializeField] float max_Y = 0f;

    private Transform target;
    private Transform goalPos;
    private bool rewindFlag = false;
    private bool pause = false;

    private void OnEnable()
    {
        EndTrigger.onAction += End;
        Goal.acquireGoal += AcquireGoal;
        //StartTrigger.onStart += Begin;
        Player.playerTarget += AcquirePlayer;
    }

    private void OnDisable()
    {
        EndTrigger.onAction -= End;
        Goal.acquireGoal -= AcquireGoal;
        //StartTrigger.onStart -= Begin;
        Player.playerTarget -= AcquirePlayer;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (!pause)
        {
            if (rewindFlag)
            {
                transform.Translate(Vector3.left * Time.deltaTime * scrollSpeed * rewindFactor);
                transform.position = new Vector3(Mathf.Clamp(transform.position.x, goalPos.position.x, max_X), Mathf.Clamp(target.position.y, min_Y, max_Y), transform.position.z);
                //transform.position = new Vector3(Mathf.Clamp(target.position.x, min_X, max_X), Mathf.Clamp(target.position.y, min_Y, max_Y), transform.position.z);
            }
            else
                transform.position = new Vector3(Mathf.Clamp(target.position.x, goalPos.position.x, max_X), Mathf.Clamp(target.position.y, min_Y, max_Y), transform.position.z);
        }
    }

    public void Pause(bool status)
    { 
        pause = status;
    }

    public void Begin(float delay)
    {
        //StartTrigger.onStart -= Begin;
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
        Debug.Log("Camera backtracking");
    }

    private void AcquirePlayer(Transform player)
    {
        target = player;
    }

    private void AcquireGoal(GameObject g)
    {
        goalPos = g.transform;
    }
}
