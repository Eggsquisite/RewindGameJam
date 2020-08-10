using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RewindUI : MonoBehaviour
{
    [SerializeField] GameObject uiImage = null;


    private void OnEnable()
    {
        RewindManager.enableUI += SetUI;
        EndTrigger.enableUI += SetUI;
    }

    private void OnDisable()
    {
        RewindManager.enableUI -= SetUI;
        EndTrigger.enableUI -= SetUI;
    }

    private void Start()
    {
        uiImage.SetActive(false);
    }

    private void SetUI(bool status)
    {
        uiImage.SetActive(status);
    }
}
