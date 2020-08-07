using System;
using System.Collections.Generic;
using UnityEngine;

public class RewindManager : MonoBehaviour {

    [SerializeField] private static bool isRewinding = false;
    [SerializeField] private static float maxRewindRate = 8f; //SET THIS
    [SerializeField] private static float rewindAccel = 20f;

    private static bool trigger = false;
    private static float rewindRate;
    private static float cooldown = 1f;
    private static float cooldownTimer;
    private static float maxRewindTime = 3f;
    private static float maxRewindTimer;
    private static AudioSource rewindFX;

    public void Start() {
        rewindFX = GameObject.Find("Rewind Effect").GetComponent<AudioSource>();
    }

    //TODO:  Make smoothing function
    public static float GetRewindRate() {
        return rewindRate;
    }

    public static float GetMaxRewindRate() {
        return maxRewindRate;
    }

    public static bool IsRewinding() {
        return isRewinding;
    }

    public static void EnableRewind() {
        if (cooldownTimer <= 0) {
            isRewinding = true;
            trigger = true;
            rewindFX.Play();
        }
    }

    public static void DisableRewind() {
        trigger = false;
        cooldownTimer = cooldown;
        rewindFX.SetScheduledEndTime(0.5);
    }

    private void Update() {
        if (Input.GetKeyDown(KeyCode.LeftShift)) EnableRewind();
        else if (Input.GetKeyUp(KeyCode.LeftShift)) DisableRewind();
    }

    void FixedUpdate() {

        if (trigger) {
            if (rewindRate != maxRewindRate) {
                rewindRate += rewindAccel * Time.fixedDeltaTime;
                if (rewindRate >= maxRewindRate) rewindRate = maxRewindRate;
            }
        }
        else {
            if (rewindRate != 0f) {
                rewindRate -= rewindAccel * Time.fixedDeltaTime;
                if (rewindRate <= 0f) {
                    rewindRate = 0f;
                    isRewinding = false;
                }
            }
        }
        //if (rewindRate != 0) Debug.Log(rewindRate);


        if (cooldownTimer >= 0) cooldownTimer -= Time.fixedDeltaTime;
        //Debug.Log(cooldownTimer);
    }
}
