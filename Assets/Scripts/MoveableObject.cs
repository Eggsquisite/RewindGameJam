using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveableObject : MonoBehaviour {
    
    public Vector3 velocity = new Vector3(0f,1f,0f);
    public Vector3 rotation = new Vector3(0f,0f,0f);
    private RewindableObject ro;
    public bool recordPositions = true;
    public bool recordRotations = false;
    public int rewindVelocity = 3;
    public float rewindAccel = 2f;

    void Start() {
        if (recordPositions || recordRotations) ro = new RewindableObject(gameObject.transform, recordPositions, recordRotations);
        //ro = gameObject.GetComponent<RewindObject>();
    }

    /*private void OnEnable() {
        if ()
    }*/

    // Update is called once per frame
    void FixedUpdate() {
        if (RewindManager.isRewinding) {
            if (recordPositions) {
                transform.position = ro.RewindPosition(10);
            }

            if (recordRotations) {
                transform.rotation = ro.RewindRotation(6);
            }
            //Transform tf = gameObject.GetComponent<RewindObject>().GetPreviousTransform();
            /*Vector3 tf = transform.position; //ignore the RHS, it just keeps an initialized value
            for (int i = 0; i < rewindVelocity; i++) {
                tf = ro.GetPreviousPosition();
            }

            transform.position = tf;*/
            //transform.rotation = tf.rotation;
            Debug.Log("New position: " + transform.position);
        }
        else {
            transform.Translate(Time.deltaTime * velocity, Space.World);
            transform.Rotate(rotation);
            if (recordPositions || recordRotations) ro.Add();
        }
    }
    
    
}
