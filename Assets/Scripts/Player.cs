﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    const float GRAVITY = -1f;

    [SerializeField] private LayerMask platformLayerMask;
    [SerializeField] float moveSpeed = 40.0f;
    [SerializeField] float jumpForce = 15.0f;

    public RewindManager rm;
    private Rigidbody2D rb;
    private Collider2D feet;
    private Animator anim;
    private SpriteRenderer sp;
    private Vector3 movement;

    float axisInput = 0f;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        feet = GetComponent<Collider2D>();
        anim = GetComponent<Animator>();
        sp = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    private void Update() {
        if (IsGrounded() && Input.GetKeyDown(KeyCode.Space))
            Jump();

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
            rb.velocity = new Vector3(0f, 0f, 0f);
        }
        else if (Input.GetKeyUp(KeyCode.LeftShift)) {
            RewindManager.isRewinding = false;
            Debug.Log("REWINDING FALSE");
        }
    }
    


    private bool IsGrounded()
    {
        float extraHeightText = .1f;
        RaycastHit2D raycastHit = Physics2D.Raycast(feet.bounds.center, Vector2.down, feet.bounds.extents.y + extraHeightText, platformLayerMask);
        Color rayColor;

        if (raycastHit.collider != null)
        {
            rayColor = Color.green;
        }
        else {
            rayColor = Color.red;
        }
        Debug.DrawRay(feet.bounds.center, Vector2.down * (feet.bounds.extents.y + extraHeightText), rayColor);
        //Debug.Log(raycastHit.collider);

        anim.SetBool("jump", !(raycastHit.collider != null));

        return raycastHit.collider != null;
    }

    private void Move()
    {
        axisInput = Input.GetAxis("Horizontal");

        if (Input.GetKey(KeyCode.A) && Input.GetKey(KeyCode.D))
            axisInput = 0;

        if (axisInput == 0) {
            if (anim.GetBool("run"))
                anim.SetBool("run", false);

            movement = new Vector3(0f, rb.velocity.y);
        }
        else {
            if (axisInput > 0)
                sp.flipX = false;
            else if (axisInput < 0)
                sp.flipX = true;

            if (!anim.GetBool("run"))
                anim.SetBool("run", true);

            movement = new Vector3(axisInput * moveSpeed, rb.velocity.y);
        } 
        
        rb.velocity = movement;
    }

    private void Jump()
    {
        //rb.velocity = new Vector2(rb.velocity.x, 0f);
        rb.AddForce(new Vector2(0f, jumpForce), ForceMode2D.Impulse);
        anim.SetBool("jump", true);
    }

    private void Light()
    {
        anim.SetTrigger("light");
    }

    public void LightRange(bool status)
    {
        if (status)
            EndTrigger.onAction += Light;
        else
            EndTrigger.onAction -= Light;
    }
}
