using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public delegate void PlayerDeath();
    public static event PlayerDeath restartLevel;

    public delegate void Target(Transform target);
    public static event Target playerTarget;

    [SerializeField] private LayerMask platformLayerMask;
    [SerializeField] private LayerMask interactiveLayer;
    [SerializeField] float deathDelay = 2f;

    [Header("Movement Stats")]
    [SerializeField] float maxSpeed = 10.0f;
    [SerializeField] float jumpForce = 800.0f;

    [Header("Player Stats")]
    [SerializeField] int health = 3;
    [SerializeField] float recoveryDelay = 2f;

    private CamMovement cam;
    private Rigidbody2D rb;
    private Collider2D feet;
    private Animator anim;
    private SpriteRenderer sp;
    private Vector3 movement;
    private GameObject projectileGO;

    float horizontalInput;
    float gravityScale;
    bool endTrigger = false;
    bool invincible = false;
    bool recovery = false;
    bool spawning = false;
    bool death = false;
    bool rewindEnabled = false;
    private bool jumpBool = false;
    private bool inAir = false;
    private bool isGrounded = false;

    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main.GetComponent<CamMovement>();
        rb = GetComponent<Rigidbody2D>();
        feet = GetComponent<Collider2D>();
        anim = GetComponent<Animator>();
        sp = GetComponent<SpriteRenderer>();
        gravityScale = rb.gravityScale;
        if (playerTarget != null)   playerTarget(this.transform);
    }

    private void OnEnable()
    {
        StartTrigger.onStart += Spawn;
    }

    private void OnDisable()
    {
        StartTrigger.onStart -= Spawn;
    }


    // Check ALL keystrokes here
    private void Update() {

        // disallows player movement when spawning in or dying
        if (!death && !spawning) 
            horizontalInput = Input.GetAxis("Horizontal");

       // disallows player jumping when dying
        if (IsGrounded() && Input.GetKeyDown(KeyCode.Space) && !death && !spawning)
            jumpBool = true;

        if (!endTrigger)
            CheckRewindInput();

        //if (RewindManager.isRewinding)
        //    Stasis();
    }

    
    private void FixedUpdate() {
        Move();
        if (RewindManager.IsRewinding() && IsOnProjectile()) AddProjectileForce();
    }

    public void CheckRewindInput() {
        if (Input.GetKeyDown(KeyCode.LeftShift)) {
            RewindManager.EnableRewind();
        }
        else if (Input.GetKeyUp(KeyCode.LeftShift)) {
            RewindManager.DisableRewind();
            rb.gravityScale = gravityScale;
        }
    }

    private void Spawn(float delay)
    {
        spawning = true;
        Invoke("SpawnReady", delay / 2);
    }

    private void SpawnReady()
    {
        spawning = false;
        sp.enabled = true;
    }

    private bool IsGrounded() {
        RaycastHit2D raycastHit = Physics2D.Raycast(feet.bounds.center, Vector2.down, feet.bounds.extents.y + 0.2f, platformLayerMask);
        if (raycastHit.collider != null) isGrounded = true;
        else isGrounded = false;
        anim.SetBool("jump", !isGrounded);
        return isGrounded;
    }
    
    private bool IsOnProjectile() {
        float extraHeightText = .2f;
        RaycastHit2D hit = Physics2D.Raycast(feet.bounds.center, Vector2.down, feet.bounds.extents.y + extraHeightText, interactiveLayer);
        if (hit.collider != null) {
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

        if (Input.GetKey(KeyCode.A) && Input.GetKey(KeyCode.D)) horizontalInput = 0;

        if (horizontalInput == 0 && rb.velocity.x == 0) anim.SetBool("run", false);
        else anim.SetBool("run", true);

        if (horizontalInput > 0) sp.flipX = false;
        else if (horizontalInput < 0) sp.flipX = true;

        rb.velocity = new Vector2(horizontalInput * maxSpeed, rb.velocity.y);
    }
    
    private void AddProjectileForce() {
        Vector2 r = transform.position - projectileGO.transform.position;
        Vector2 r2 = new Vector2(r.y, -r.x); //orthogonal
        float angularVel = projectileGO.GetComponent<MoveableObject>().GetAngularVelocity();
        //Debug.Log(transform.position + "    " + projectileGO.transform.position + "    " + r);
        rb.AddForce(rb.mass * angularVel * angularVel * r * 60f); //centrifugal
        rb.AddForce(rb.mass * angularVel * angularVel * r2 * 50f); //orthogonal (CW direction)
        Debug.Log("Adding Projectile Force");
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
        if (recovery || invincible)
            return;

        health -= dmg;
        recovery = true;
        Debug.Log("health: " + health);
        anim.SetTrigger("hurt");
        StartCoroutine(RecoveryDelay());

        if (health <= 0) 
            StartCoroutine(Death());
    }

    public void Invincible(bool status)
    {
        invincible = status;
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
        restartLevel();
    }
}
