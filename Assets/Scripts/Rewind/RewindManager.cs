using System;
using System.Collections.Generic;
using UnityEngine;

public class RewindManager : MonoBehaviour {
    public delegate void RewindEvent(bool status);

    public static event RewindEvent rewindEvent;

    [SerializeField] private static bool isRewinding = false;
    [SerializeField] private static float maxRewindRate = 8f; //SET THIS
    [SerializeField] private static float rewindAccel = 20f;

    private static bool trigger = false;
    private static float rewindRate;
    private static float maxRewindTime = 3f;
    private static float maxRewindTimer;
    private static AudioSource rewindFX;
    public static bool rewindAbilityDisabled = false;
    public bool disablePlayerRewind;

    public void Start() {
        rewindFX = GameObject.Find("Rewind Effect").GetComponent<AudioSource>();
        rewindAbilityDisabled = disablePlayerRewind;
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
        rewindEvent?.Invoke(true);
        isRewinding = true;
        trigger = true;
        rewindFX.Play();
    }

    public static void DisableRewind() {
        rewindEvent?.Invoke(false);

        trigger = false;
        rewindFX.SetScheduledEndTime(0.5);
    }

    public static void DisablePlayerRewindAbility() {
        rewindAbilityDisabled = true;
    }

    public void DisableForever() {
        gameObject.SetActive(false);
    }

    private void Update() {
        if (!rewindAbilityDisabled) {
            // Take out !EndTrigger.backtrackBegin if you want player to rewind during backtrack
            if (Input.GetKeyDown(KeyCode.LeftShift) && !Player.death && !EndTrigger.backtrackBegin) EnableRewind();
            else if (Input.GetKeyUp(KeyCode.LeftShift) || Player.death) DisableRewind();
            //else if (EndTrigger.backtrackBegin) EnableRewind();
        }
    }

    //Allows for acceleration and decelleration of time
    void FixedUpdate() {
        if (trigger) {
            if (rewindRate != maxRewindRate) {
                rewindRate += rewindAccel * Time.fixedDeltaTime; // v = u + at
                if (rewindRate >= maxRewindRate) rewindRate = maxRewindRate;
            }
        }
        else {
            if (rewindRate != 0f) {
                rewindRate -= rewindAccel * Time.fixedDeltaTime; // v = u - at
                if (rewindRate <= 0f) {
                    rewindRate = 0f;
                    isRewinding = false;
                }
            }
        }
    }
}