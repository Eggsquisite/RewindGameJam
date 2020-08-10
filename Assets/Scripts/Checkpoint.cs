using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    public delegate void CheckpointReached();
    public static event CheckpointReached onTrigger;


    // Start is called before the first frame update
    void Start()
    {
        
    }


    private void SetCheckpoint(float nothing)
    { 
        
    }
}
