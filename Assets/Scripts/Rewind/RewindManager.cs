using System;
using System.Collections.Generic;
using UnityEngine;

public class RewindManager : MonoBehaviour {

    [SerializeField] private static bool isRewinding = false;
    [SerializeField] private static float maxRewindRate = 8f; //SET THIS
    [SerializeField] private static float rewindAccel = 20f;

    private static bool trigger = false;
    private static float rewindRate;
    private static float cooldown = 0f;
    private static float cooldownTimer;
    private static float maxRewindTime = 3f;
    private static float maxRewindTimer;
    private static AudioSource rewindFX;
    public static bool rewindDisabledForever = false;

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
        if (!rewindDisabledForever) return isRewinding;
        else return false;
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

    public void DisableForever() {
        gameObject.SetActive(false);
    }
    private void Update() {
        if (Input.GetKeyDown(KeyCode.LeftShift) && !Player.death && !EndTrigger.backtrackBegin) EnableRewind();
        else if (Input.GetKeyUp(KeyCode.LeftShift) || Player.death) DisableRewind();
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
