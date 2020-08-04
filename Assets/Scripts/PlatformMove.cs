using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformMove : MonoBehaviour {
    
    public float velocity = 1f;
    public float rewindVelocity = 3f;
    private RewindObject ro;

    void Start() {
        ro = gameObject.GetComponent<RewindObject>();
    }

    // Update is called once per frame
    void FixedUpdate() {
        if (RewindManager.isRewinding) {
            //Transform tf = gameObject.GetComponent<RewindObject>().GetPreviousTransform();
            Vector3 tf = transform.position; //ignore the RHS, it just keeps an initialized value
            for (int i = 0; i < rewindVelocity; i++) {
                tf = ro.GetPreviousTransform();
            }

            transform.position = tf;
            //transform.rotation = tf.rotation;
            Debug.Log("New position: " + transform.position);
        }
        else {
            transform.Translate(Vector3.up * Time.deltaTime * velocity);
        }
    }
    
    
}
