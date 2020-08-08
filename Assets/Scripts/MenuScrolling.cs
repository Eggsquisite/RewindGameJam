using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuScrolling : MonoBehaviour
{
    // This can be used to scroll the map as well
    [SerializeField] float scrollSpeed = 2.0f;

    // Update is called once per frame
    void LateUpdate()
    {
        transform.Translate(Vector3.right * Time.deltaTime * scrollSpeed);
    }
}
