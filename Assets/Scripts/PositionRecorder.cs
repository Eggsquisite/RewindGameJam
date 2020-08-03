using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PositionRecorder : MonoBehaviour
{
    
    List<Transform> recorded = new List<Transform>(1000);

    private List<float> recorded_time = new List<float>(1000);
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        recorded.Add(gameObject.transform);
        recorded_time.Add(Time.deltaTime);
    }
}
