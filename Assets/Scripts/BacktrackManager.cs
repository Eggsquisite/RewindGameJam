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
        for (int i = 0; i < objs.Length; i++) {
            objs[i].SetActive(false);
            backtrackGOs.Add(objs[i]);
            Debug.Log("Added " + objs[i].name + " to the Backtrack Manager");
        }
        rm = GameObject.Find("RewindManager");

        EndTrigger.BackTrackTriggered += OnBacktrackTriggered;
    }

    void OnBacktrackTriggered(float delay) {
        for (int i = 0; i < backtrackGOs.Count; i++) {
            backtrackGOs[i].SetActive(true);
        }
    }
    
    void AddGameObject(GameObject go) {
        backtrackGOs.Add(go);
    }
}
