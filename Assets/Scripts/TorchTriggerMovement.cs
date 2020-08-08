using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class TorchTriggerMovement : MonoBehaviour {
    public GameObject objectToMove;
    private MoveableObject mo;
    
    // Start is called before the first frame update
    void Start() {
        mo = objectToMove.GetComponent<MoveableObject>();
    }

    // Update is called once per frame
    private void OnTriggerEnter2D(Collider2D collision) {


        Debug.Log("ENABLED");
            mo.enableAutoMovement = true;
            Debug.Log("Check:  " + mo.enableAutoMovement);
            gameObject.GetComponent<TorchTriggerMovement>().enabled = false;
        
    }
}
