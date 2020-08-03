using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformMove : MonoBehaviour {
    
    public float velocity = 1f;

    // Update is called once per frame
    void Update() {
        transform.Translate(Vector3.up * Time.deltaTime * velocity);
    }
    
    
}
