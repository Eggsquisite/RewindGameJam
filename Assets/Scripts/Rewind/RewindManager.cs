using System.Collections.Generic;
using UnityEngine;

public class RewindManager : MonoBehaviour {

    public static bool isRewinding = false;
    public static float rewindRate = 4f;
    
    void Update() {

        if (Input.GetKeyDown(KeyCode.LeftShift)) RewindManager.isRewinding = true;
        else if (Input.GetKeyUp(KeyCode.LeftShift)) RewindManager.isRewinding = false;
    }
}
