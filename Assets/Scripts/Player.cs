using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private LayerMask platformLayerMask;
    public RewindManager rm;
    [SerializeField] LevelManager lm;
    [SerializeField] float deathDelay = 2f;

    [Header("Movement Stats")]
    [SerializeField] float moveSpeed = 40.0f;
    [SerializeField] float jumpForce = 15.0f;

    [Header("Player Stats")]
    [SerializeField] int health = 3;
    [SerializeField] float recoveryDelay = 2f;

    private CamMovement cam;
    private Rigidbody2D rb;
    private Collider2D feet;
    private Animator anim;
    private SpriteRenderer sp;
    private Vector3 movement;

    float axisInput = 0f;
    float gravityScale;
    bool endTrigger = false;
    bool rewinding = false;
    bool recovery = false;
    bool death = false;

    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main.GetComponent<CamMovement>();
        rb = GetComponent<Rigidbody2D>();
        feet = GetComponent<Collider2D>();
        anim = GetComponent<Animator>();
        sp = GetComponent<SpriteRenderer>();
        gravityScale = rb.gravityScale;
    }

    // Update is called once per frame
    private void Update() {
        if (IsGrounded() && Input.GetKeyDown(KeyCode.Space) && !death)
            Jump();

        if (!endTrigger)
            RewindTime();

        if (rewinding)
            Stasis();
    }

    
    private void FixedUpdate() {
        if (!rewinding && !death)
            Move();
    }

    public void RewindTime() {
        if (Input.GetKeyDown(KeyCode.LeftShift)) {
            
            //rm.RewindTime();
            RewindManager.isRewinding = true;
            Debug.Log("REWINDING TRUE");

            rewinding = true;
        }
        else if (Input.GetKeyUp(KeyCode.LeftShift)) {
            RewindManager.isRewinding = false;
            Debug.Log("REWINDING FALSE");

            rewinding = false;
            rb.gravityScale = gravityScale;
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

    private void Stasis()
    {
        rb.gravityScale = 0;
        rb.velocity = Vector3.zero;
    }

    private void Light(float delay)
    {
        if (!endTrigger)
        {
            anim.SetTrigger("light");
            endTrigger = true;
        }
    }

    public void LightRange(bool status)
    {
        if (status)
            EndTrigger.onAction += Light;
        else
            EndTrigger.onAction -= Light;
    }

    public void Hurt(int dmg)
    {
        if (!recovery)
        {
            health -= dmg;
            recovery = true;
            Debug.Log("health: " + health);
            anim.SetTrigger("hurt");
            StartCoroutine(RecoveryDelay());
        }

        if (health == 0) 
            StartCoroutine(Death());
    }

    private IEnumerator RecoveryDelay()
    {
        yield return new WaitForSeconds(recoveryDelay);
        recovery = false;
    }

    public void OutOfBounds()
    {
        StartCoroutine(Death());
    }

    private IEnumerator Death()
    {
        cam.Pause(true);
        death = true;
        recovery = true;
        anim.SetTrigger("death");
        rb.velocity = Vector3.zero;

        yield return new WaitForSeconds(deathDelay);
        lm.RestartLevel();
    }
}
