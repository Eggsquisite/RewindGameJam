using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RewindMusic : MonoBehaviour
{
    AudioSource audioSource;
    private bool trigger = false;
    private bool paused = false;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update() {
        if (!paused && EscapeMenu.isPaused)
        {
            paused = true;
            audioSource.Pause();
        }
        else if (paused && !EscapeMenu.isPaused)
        {
            paused = false;
            audioSource.Play();
        }
        

        if (RewindManager.IsRewinding() && !trigger) {
            trigger = true;
            GetComponent<AudioSource>().pitch = -1f;
        }
        else if (!RewindManager.IsRewinding() && trigger) {
            trigger = false;
            GetComponent<AudioSource>().pitch = 1f;
        }
    }
}
