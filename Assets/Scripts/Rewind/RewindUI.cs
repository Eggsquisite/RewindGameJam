using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RewindUI : MonoBehaviour {
    
    [SerializeField] GameObject uiTextGO = null;
    private Text uiText;
    private Color colorON;
    private Color colorOFF;
    private float timer;
    public float timeON = 1f;
    public float timeOFF = 0.5f;
    private bool enableUI = false;

    private void OnEnable() {
        Debug.Log("This was enabled");
        RewindManager.rewindEvent += EnableUI;
        EndTrigger.enableUI += EnableUI;
    }

    private void OnDisable()
    {
        Debug.Log("This was disabled");
        RewindManager.rewindEvent -= EnableUI;
        EndTrigger.enableUI -= EnableUI;
    }

    private void Start() {
        uiText = uiTextGO.GetComponent<Text>();
        colorON = uiText.color;
        colorOFF = new Color(colorON.r, colorON.g, colorON.b, 0f);
        uiTextGO.SetActive(false);
    }

    private void EnableUI(bool status) {
        uiTextGO.SetActive(status);
        enableUI = status;
    }

    public void Update() {
        if (enableUI) {
            timer += Time.deltaTime;
            //Debug.Log(uiText.color.a + "    " + timer);
            if (uiText.color.a == 1f) {
                if (timer >= timeON) {
                    timer = 0f;
                    uiText.color = colorOFF;
                    Debug.Log("Setting transparency to 0");
                }
            }
            else {
                if (timer >= timeOFF) {
                    timer = 0f;
                    uiText.color = colorON;
                    Debug.Log("Setting transparency to 1");
                }
            }
        }
    }
}
