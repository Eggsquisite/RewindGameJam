using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BacktrackManager : MonoBehaviour {

    private List<GameObject> backtrackGOs = new List<GameObject>(10);
    private GameObject rm;

    private bool once = true;
    // Start is called before the first frame update
    void Start() {
        GameObject[] objs = GameObject.FindGameObjectsWithTag("BacktrackObject");
        Debug.Log("In the Backtrack Manager, we found EndTrigger.backtrackBegin to be: " + EndTrigger.backtrackBegin);

        for (int i = 0; i < objs.Length; i++) {
            if (!EndTrigger.backtrackBegin) objs[i].SetActive(false);
            backtrackGOs.Add(objs[i]);
            Debug.Log("Added " + objs[i].name + " to the Backtrack Manager");
        }
        rm = GameObject.Find("RewindManager");

        EndTrigger.BackTrackTriggered += OnBacktrackTriggered;
    }

    void OnBacktrackTriggered(float delay) {
        Debug.Log("Enabling backtrack objects");
        for (int i = 0; i < backtrackGOs.Count; i++) {
            backtrackGOs[i].SetActive(true);
        }
    }
    
    void AddGameObject(GameObject go) {
        backtrackGOs.Add(go);
    }
}
