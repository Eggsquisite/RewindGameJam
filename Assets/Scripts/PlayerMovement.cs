using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {
    
    [SerializeField] float moveSpeed = 40.0f;
    [SerializeField] float jumpForce = 15.0f;
    private Rigidbody2D rb;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

    }

    // Update is called once per frame
    private void Update() {
        if (Input.GetKeyDown(KeyCode.Space) && (rb.velocity.y == 0f)) Jump();
        RewindTime();
    }

    
    private void FixedUpdate() {
        Move();
        
    }

    public void RewindTime() {
        if (Input.GetKeyDown(KeyCode.LeftShift)) {
            
            //rm.RewindTime();
            RewindManager.isRewinding = true;
            Debug.Log("REWINDING TRUE");
        }
        else if (Input.GetKeyUp(KeyCode.LeftShift)) {
            RewindManager.isRewinding = false;
            Debug.Log("REWINDING FALSE");
        }
    }




    private void Move() {
       float axisInput = Input.GetAxis("Horizontal");

        if (Input.GetKey(KeyCode.A) && Input.GetKey(KeyCode.D))
            axisInput = 0;

        if (axisInput == 0) {
            rb.velocity = new Vector3(0f, rb.velocity.y);
        }
        else {
            rb.velocity = new Vector3(axisInput * moveSpeed, rb.velocity.y);
        }
    }

    private void Jump()
    {
        rb.velocity = new Vector2(rb.velocity.x, 0f);
        rb.AddForce(new Vector2(0f, jumpForce), ForceMode2D.Impulse);
        //anim.SetBool("jump", true);
    }

}
