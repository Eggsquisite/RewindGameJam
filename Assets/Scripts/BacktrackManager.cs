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
    }

    // Update is called once per frame
    void FixedUpdate() {
        if (EndTrigger.backtrackBegin) {
            for (int i = 0; i < backtrackGOs.Count; i++) {
                backtrackGOs[i].SetActive(true);
            }

            RewindManager.rewindAbilityDisabled = true;
            rm.GetComponent<RewindManager>().DisableForever();
            gameObject.SetActive(false); //disables this forever
        }
    }

    void AddGameObject(GameObject go) {
        backtrackGOs.Add(go);
    }
}
