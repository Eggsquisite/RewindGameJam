using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamMovement : MonoBehaviour
{
    [SerializeField] float scrollSpeed = 2.0f;

    // Update is called once per frame
    void LateUpdate()
    {
        transform.Translate(Vector3.right * Time.deltaTime * scrollSpeed);
    }
}
