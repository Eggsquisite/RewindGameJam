using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextAppearOnRewind : MonoBehaviour {
    [SerializeField] GameObject text = null;

    private void Start() {
        text.SetActive(false);
    }

    private void OnEnable() { RewindManager.rewindEvent += ShowText; }

    private void OnDisable() { RewindManager.rewindEvent -= ShowText; }

    private void ShowText(bool meh) {
        text.SetActive(true);
    }
}
