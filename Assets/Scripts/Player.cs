using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    const float GRAVITY = -1f;

    [SerializeField] float moveSpeed = 10.0f;
    [SerializeField] float jumpForce = 400.0f;

    private Rigidbody2D rb;
    private Collider2D feet;
    private Vector2 movement;
    private bool m_Grounded;        // Whether or not the player is grounded

    private bool onGround = false;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        feet = GetComponent<Collider2D>();
    }

    // Update is called once per frame
    private void Update()
    {
        CheckGrounded();
        Move();
        Jump();
    }


    private void CheckGrounded()
    {
        if (feet.IsTouchingLayers(LayerMask.GetMask("Ground")) && !onGround)
        {
            Debug.Log("grounded");
            onGround = true;
        }
        else if (!feet.IsTouchingLayers(LayerMask.GetMask("Ground")) && onGround)
        {
            Debug.Log("ungrounded");
            onGround = false;
        }
    }

    private void Move()
    {
        Vector3 movement = new Vector3(Input.GetAxis("Horizontal") * moveSpeed, rb.velocity.y);
        rb.velocity = movement;
    }


    private void Jump()
    {
        if (Input.GetButtonDown("Jump") && onGround)
        {
            onGround = false;
            rb.AddForce(new Vector2(0f, jumpForce), ForceMode2D.Impulse);
        }
    }

}
