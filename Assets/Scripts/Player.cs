using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    const float GRAVITY = -1f;

    [SerializeField] float moveSpeed = 10.0f;
    [SerializeField] float jumpVelocity = 100.0f;

    private Rigidbody2D rb;
    private Collider2D feet;
    private Vector2 movement;

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
        MoveInput();
        Jump();
    }

    void FixedUpdate()
    {
       MoveCharacter(movement);
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

    private void MoveInput()
    {
        movement = new Vector2(Input.GetAxis("Horizontal"), Mathf.Clamp(rb.velocity.y, -1, 0));
    }

    private void MoveCharacter(Vector2 direction)
    {
        rb.MovePosition((Vector2)transform.position + (direction * moveSpeed * Time.deltaTime));
        //rb.velocity = Vector2.right * moveSpeed * Time.deltaTime;
    }

    private void Jump()
    {
        if (Input.GetButtonDown("Jump") && onGround)
        {
            Debug.Log("jumping");
            rb.velocity = Vector2.up * jumpVelocity;
        }
    }

}
