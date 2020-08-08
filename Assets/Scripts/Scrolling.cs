using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scrolling : MonoBehaviour
{
    [SerializeField] float scrollSpeed = 2f;

    // Update is called once per frame
    void Update()
    {
        if (EndTrigger.backtrackBegin)
            transform.Translate(Vector3.right * Time.deltaTime * scrollSpeed);
        else
            transform.Translate(Vector3.left * Time.deltaTime * scrollSpeed);
    }
}
