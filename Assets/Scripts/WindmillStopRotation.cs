using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindmillStopRotation : MonoBehaviour {
    
    
    public bool noCW = false;
    public bool noCCW = false;

    private Rigidbody2D rb;
    // Start is called before the first frame update
    void Start() {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate() {

        if (noCW && rb.angularVelocity > 0) {
            rb.angularVelocity = 0f;
        }
        
        else if (noCCW && rb.angularVelocity < 0) {
            rb.angularVelocity = 0f;
        }
        
    }
}
