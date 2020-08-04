using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RewindMusic : MonoBehaviour
{
    // Start is called before the first frame update
    private bool trigger = false;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update() {
        if (RewindManager.isRewinding && !trigger) {
            trigger = true;
            GetComponent<AudioSource>().pitch = -1f;
        }
        else if (!RewindManager.isRewinding && trigger) {
            trigger = false;
            GetComponent<AudioSource>().pitch = 1f;
        }
    }
}
