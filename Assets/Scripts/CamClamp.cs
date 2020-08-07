using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamClamp : MonoBehaviour
{
    [SerializeField] float min_X = 0f;
    [SerializeField] float max_X = 5f;
    [SerializeField] float min_Y = 0f;
    [SerializeField] float max_Y = 10f;

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(
            Mathf.Clamp(transform.position.x, min_X, max_X),
            Mathf.Clamp(transform.position.y, min_Y, max_Y),
            transform.position.z);
    }
}
