using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RewindUISingleton : MonoBehaviour
{
    
    public static RewindUISingleton Instance { get; private set; }
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
