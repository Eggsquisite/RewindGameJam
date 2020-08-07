using System.Collections.Generic;
using UnityEngine;

public class RewindManager : MonoBehaviour {

    private static bool isRewinding = false;
    private static float rewindRate = 4f;
    private static float maxRewindRate = 6f;

    //TODO:  Make smoothing function
    public static float GetRewindRate() {
        return maxRewindRate;
    }

    public static float GetMaxRewindRate() {
        return maxRewindRate;
    }

    public static bool IsRewinding() {
        return isRewinding;
    }

    public static void EnableRewind() {
        isRewinding = true;
    }

    public static void DisableRewind() {
        isRewinding = false;
    }
    
    void Update() {

        //if (Input.GetKeyDown(KeyCode.LeftShift)) RewindManager.isRewinding = true;
        //else if (Input.GetKeyUp(KeyCode.LeftShift)) RewindManager.isRewinding = false;
    }
}
