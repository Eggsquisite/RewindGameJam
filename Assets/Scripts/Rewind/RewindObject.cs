using System.Collections.Generic;
using UnityEngine;

public class RewindObject : MonoBehaviour {
    
    private List<Vector3> recordedTransform = new List<Vector3>(1000);
    private int position = 0;
    

    //private List<float> recordedTime = new List<float>(1000);

// Start is called before the first frame update
    void Start() { }

// Update is called once per frame
    void FixedUpdate() {
        if (!RewindManager.isRewinding) { //only record positions when not rewinding
            if (position == recordedTransform.Count) {
                recordedTransform.Add(gameObject.transform.position);
                position++;
            }
            recordedTransform.Insert(position++, gameObject.transform.position);
            //position++;
        }
    }

    public Vector3 GetPreviousTransform() {
        if (position > 0) return recordedTransform[--position];
        else return recordedTransform[0];
    }
}