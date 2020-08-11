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

    private void OnEnable()
    {
        //Debug.Log("This was called once");
        //RewindManager.enableUI += SetUI;
        //EndTrigger.enableUI += SetUI;
    }

    private void OnDisable()
    {
        //Debug.Log("This was disabled");
        //RewindManager.enableUI -= SetUI;
        //EndTrigger.enableUI -= SetUI;
    }

    private void Start() {
        uiText = uiTextGO.GetComponent<Text>();
        colorON = uiText.color;
        colorOFF = new Color(colorON.r, colorON.g, colorON.b, 0f);
        RewindManager.enableUI += SetUI;
        EndTrigger.enableUI += SetUI;
        uiTextGO.SetActive(false);
    }

    private void SetUI(bool status) {
        //Debug.Log("THIS WAS CALLED!");
        gameObject.SetActive(status);
        /*if (EndTrigger.backtrackBegin)
            uiTextGO.SetActive(true);
        else
            uiTextGO.SetActive(status);*/
    }

    public void Update() {
        timer += Time.deltaTime;
        Debug.Log(uiText.color.a + "    " + timer);
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
