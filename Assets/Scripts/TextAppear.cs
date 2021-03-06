﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextAppear : MonoBehaviour
{
    [SerializeField] GameObject text = null;


    private void Start()
    {
        text.SetActive(false);
    }

    private void OnEnable() {
        EndTrigger.BackTrackTriggered += ShowText;
    }

    private void OnDisable() {
        EndTrigger.BackTrackTriggered -= ShowText;
    }

    private void ShowText(float delay) {
        text.SetActive(true);
    }
}