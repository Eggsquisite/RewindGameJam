using System;
using UnityEngine;

public class MoveableObject : MonoBehaviour {
    [Header("Positions")] public bool enableAutoMovement = false;
    public Vector2 position1;
    public Vector2 position2;
    public float maxSpeed = 1f;
    public float waitTime = 2f;
    public bool recordPositions = true;

    [Header("Rotations")] public float maxAngularVelocity = 0f;
    public bool recordRotations = false;

    private float angularVelocity;
    private float timer, timer2;
    private float tol = 0.05f;
    private bool holdAndWait = false;
    private bool firstTimeRewinding = true;
    private float waitTimeReverse;
    private RewindableObject ro;
    private Vector2 moveDirection;
    private Vector3 rotVector = Vector3.forward * 2 * (float) (Math.PI);

    void Start() {
        if (recordPositions || recordRotations) ro = new RewindableObject(gameObject.transform, recordPositions, recordRotations);
        waitTimeReverse = waitTime / RewindManager.GetMaxRewindRate();
        moveDirection = (position2 - position1).normalized;
    }

    void FixedUpdate() {
        if (RewindManager.IsRewinding()) {
            if (recordPositions) {
                transform.position = ro.RewindPosition((int) (RewindManager.GetRewindRate()));
            }
            else {
                if (enableAutoMovement) {
                    if (firstTimeRewinding) {
                        if (holdAndWait) { //if we are in the middle of the timer
                            timer = waitTime - timer; //flip the timer
                            timer /= RewindManager.GetMaxRewindRate(); //scale to the max rewind rate
                        }
                        Flip();
                        firstTimeRewinding = false;
                    }

                    if (holdAndWait) {
                        timer += Time.fixedDeltaTime * RewindManager.GetRewindRate();
                        if (timer >= waitTimeReverse) holdAndWait = false;
                    }
                    else {
                        transform.Translate(Time.fixedDeltaTime * RewindManager.GetRewindRate() * moveDirection, Space.World);
                        if (((Vector2)(transform.position) - position2).magnitude <= tol) {
                            holdAndWait = true;
                            timer = 0f;
                            Flip();
                        }
                    }
                }
            }

            if (recordRotations) {
                transform.rotation = ro.RewindRotation((int) (RewindManager.GetRewindRate()));
            }
            else {
                angularVelocity = maxAngularVelocity * RewindManager.GetRewindRate() * -1f;
                transform.Rotate(rotVector * angularVelocity);
            }
        }
        else {
            if (enableAutoMovement) {
                firstTimeRewinding = true; //need to reset this variable here
                if (holdAndWait) {
                    timer += Time.fixedDeltaTime;
                    if (timer >= waitTime) {
                        holdAndWait = false;
                    }
                }
                else {
                    transform.Translate(Time.fixedDeltaTime * maxSpeed * moveDirection, Space.World);
                    //Debug.Log(((Vector2)(transform.position) - position2).magnitude);
                    if (((Vector2) transform.position - position2).magnitude <= tol) {
                        holdAndWait = true;
                        timer = 0f;
                        Flip();
                    }
                }
            }

            angularVelocity = maxAngularVelocity;
            transform.Rotate(rotVector * angularVelocity);

            if (recordPositions || recordRotations) ro.Add();
        }
    }

    public void Flip() {
        Vector2 temp = position2;
        position2 = position1;
        position1 = temp;
        moveDirection = (position2 - position1).normalized;
    }

    public float GetAngularVelocity() {
        return angularVelocity;
    }
}