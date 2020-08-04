using System.Collections.Generic;
using UnityEngine;

public class RewindManager : MonoBehaviour {
    // Start is called before the first frame update

    public static bool isRewinding = false;
    
    List<GameObject> rewindableObjects = new List<GameObject>();
    void Start() {
        GameObject[] objects = GameObject.FindGameObjectsWithTag("RewindableObject");
        for (int i = 0; i < objects.Length; i++) {
            rewindableObjects.Add(objects[i]);
        }
    }

    // Update is called once per frame
    void Update() {
        
    }
}
