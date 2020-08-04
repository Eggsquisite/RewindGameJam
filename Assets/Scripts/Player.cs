using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    const float GRAVITY = -1f;

    [SerializeField] private LayerMask platformLayerMask;
    [SerializeField] float moveSpeed = 40.0f;
    [SerializeField] float jumpForce = 15.0f;

    private Rigidbody2D rb;
    private Collider2D feet;
    public RewindManager rm;
    private Vector3 movement;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        feet = GetComponent<Collider2D>();
        
    }

    // Update is called once per frame
    private void Update() {
        //Move();
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
        }
        else if (Input.GetKeyUp(KeyCode.LeftShift)) {
            RewindManager.isRewinding = false;
            Debug.Log("REWINDING FALSE");
        }
    }
    


    private bool IsGrounded()
    {
        /*
        if (feet.IsTouchingLayers(LayerMask.GetMask("Ground")) && !onGround)
        {
            Debug.Log("grounded");
            return true;
        }
        else if (!feet.IsTouchingLayers(LayerMask.GetMask("Ground")) && onGround)
        {
            Debug.Log("ungrounded");
            return false;
        }

        return false;
        */

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
        return raycastHit.collider != null;
    }

    private void Move()
    {
        if (Input.GetAxis("Horizontal") == 0) {
            movement = new Vector3(0f, rb.velocity.y);
        }
        else { 
            movement = new Vector3(Input.GetAxis("Horizontal") * moveSpeed, rb.velocity.y);
        } 

        rb.velocity = movement;
    }


    private void Jump()
    {
        rb.velocity = new Vector2(rb.velocity.x, 0f);
        rb.AddForce(new Vector2(0f, jumpForce), ForceMode2D.Impulse);
    }

}
