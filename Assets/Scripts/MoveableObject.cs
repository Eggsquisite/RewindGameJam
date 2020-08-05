using System;
using UnityEngine;

public class MoveableObject : MonoBehaviour {

    public Vector3 velocity = new Vector3(0f,1f,0f);
    public float maxAngularVelocity = 0.25f;
    //public float reverseAngularVelocity = 1f;
    public bool recordPositions = true;
    public bool recordRotations = false;
    public bool infiniteRotations = true;

    private float angularVelocity;

    private RewindableObject ro;
    private Vector3 rotVector = Vector3.forward * 2 * (float) (Math.PI);

    void Start() {
        if (recordPositions || recordRotations) ro = new RewindableObject(gameObject.transform, recordPositions, recordRotations);
    }

    void FixedUpdate() {
        if (RewindManager.isRewinding) {
            if (recordPositions) {
                transform.position = ro.RewindPosition(10);
            }

            if (recordRotations) {
                transform.rotation = ro.RewindRotation(6);
            }

            if (infiniteRotations) {
                angularVelocity = maxAngularVelocity * RewindManager.rewindRate * -1f;
                transform.Rotate(rotVector * angularVelocity);
            }
        }
        else {
            transform.Translate(Time.deltaTime * velocity, Space.World);
            angularVelocity = maxAngularVelocity;
            transform.Rotate(rotVector*angularVelocity);
            if (recordPositions || recordRotations) ro.Add();
        }
    }

    public float GetAngularVelocity() {
        return angularVelocity;
    }
}
