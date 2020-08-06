using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {
    const float GRAVITY = -1f;

    [SerializeField] private LayerMask platformLayerMask;
    [SerializeField] private LayerMask interactiveLayer;
    [SerializeField] float maxSpeed = 40.0f;
    [SerializeField] float jumpForce = 15.0f;

    private Rigidbody2D rb;
    private Collider2D feet;
    private Animator anim;
    private SpriteRenderer sp;
    private Vector3 movement;
    private GameObject projectileGO;

    private float horizontalInput;
    bool rewindEnabled = false;
    private bool jumpBool = false;
    private bool inAir = false;
    private bool isGrounded = false;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        feet = GetComponent<Collider2D>();
        anim = GetComponent<Animator>();
        sp = GetComponent<SpriteRenderer>();
    }

    // Check ALL keystrokes here
    private void Update() {
        horizontalInput = Input.GetAxis("Horizontal");
        if (!rewindEnabled) CheckRewindInput();
        if (IsGrounded() && Input.GetKeyDown(KeyCode.Space)) jumpBool = true;
        
    }
    
    //Do ALL Physics and movement stuff here
    private void FixedUpdate() {
        
        Move();
        if (RewindManager.isRewinding && IsOnProjectile()) AddProjectileForce();
    }

    public void CheckRewindInput() {
        if (Input.GetKeyDown(KeyCode.LeftShift)) RewindManager.isRewinding = true;
        else if (Input.GetKeyUp(KeyCode.LeftShift)) RewindManager.isRewinding = false;
    }
    
    private bool IsGrounded() {
        float extraHeightText = .1f;
        RaycastHit2D raycastHit = Physics2D.Raycast(feet.bounds.center, Vector2.down, feet.bounds.extents.y + extraHeightText, platformLayerMask);
        if (raycastHit.collider != null) isGrounded = true;
        else isGrounded = false;
        anim.SetBool("jump", !isGrounded);
        return isGrounded;
    }

    private bool IsOnProjectile() {
        float extraHeightText = .1f;
        RaycastHit2D hit = Physics2D.Raycast(feet.bounds.center, Vector2.down, feet.bounds.extents.y + extraHeightText, interactiveLayer);
        if (hit.collider != null) {
            //Debug.Log(hit.transform.gameObject.name);
            projectileGO = hit.transform.gameObject;
            return true;
        }
        else return false;
    }

    private void Move() {

        if (inAir && IsGrounded()) { //check for ground every frame while airborn
            anim.SetBool("jump", false);
            inAir = false;
            Debug.Log("We hit the ground");
        }
        
        if (jumpBool && IsGrounded()) {
            rb.AddForce(new Vector2(0f, jumpForce));
            anim.SetBool("jump", true);
            jumpBool = false;
            inAir = true;
            Debug.Log("We jumped");
        }
        else jumpBool = false;

        
        
        //if (horizontalInput != 0) {
            //Debug.Log(horizontalInput);
            if (Input.GetKey(KeyCode.A) && Input.GetKey(KeyCode.D)) horizontalInput = 0;

            if (horizontalInput == 0 && rb.velocity.x == 0) anim.SetBool("run", false);
            else anim.SetBool("run", true);
            
            if (horizontalInput > 0) sp.flipX = false;
            else if (horizontalInput < 0) sp.flipX = true;

            rb.velocity = new Vector2(horizontalInput*maxSpeed, rb.velocity.y);

        //}

        
    }

    private void Jump() {
        //rb.velocity = new Vector2(rb.velocity.x, 0f);
        rb.AddForce(new Vector2(0f, jumpForce), ForceMode2D.Impulse);
        anim.SetBool("jump", true);
    }

    private void AddProjectileForce() {
        Vector2 r = transform.position - projectileGO.transform.position;
        Vector2 r2 = new Vector2(r.y, -r.x);
        float angularVel = projectileGO.GetComponent<MoveableObject>().GetAngularVelocity();
        //Debug.Log(transform.position + "    " + projectileGO.transform.position + "    " + r);
        rb.AddForce(rb.mass*angularVel*angularVel*r*60f);
        rb.AddForce(rb.mass*angularVel*angularVel*r2*50f);
        Debug.Log(rb.mass*angularVel*angularVel*r);
    }

    private void Light() {
        if (!rewindEnabled) {
            anim.SetTrigger("light");
            rewindEnabled = true;
        }
    }

    public void LightRange(bool status) {
        if (status)
            EndTrigger.onAction += Light;
        else
            EndTrigger.onAction -= Light;
    }
}