using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scrolling : MonoBehaviour
{
    [SerializeField] float scrollSpeed = 2f;
    [SerializeField] float rewindFactor = 2f;

    // Update is called once per frame
    void Update()
    {
        if (EndTrigger.backtrackBegin || RewindManager.IsRewinding())
            transform.Translate(Vector3.right * rewindFactor * Time.deltaTime * scrollSpeed);
        else
            transform.Translate(Vector3.left * Time.deltaTime * scrollSpeed);
    }
}
