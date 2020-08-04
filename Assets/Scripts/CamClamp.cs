using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamClamp : MonoBehaviour
{
    [SerializeField] float min_X = 0f;
    [SerializeField] float max_X = 5f;

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(
            Mathf.Clamp(transform.position.x, min_X, max_X),
            transform.position.y,
            transform.position.z);
    }
}
