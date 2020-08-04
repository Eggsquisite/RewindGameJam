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

    public void Rewind(float delay) {
        StartCoroutine(EndStart(delay));
    }

    private IEnumerator EndStart(float delay)
    {
        pause = true;
        yield return new WaitForSeconds(delay);
        pause = false;
        rewindFlag = true;
    }
}
