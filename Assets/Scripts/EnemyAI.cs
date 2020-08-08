using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class EnemyAI : MonoBehaviour
{

    private Transform target;
    public float speedMin = 200f;
    public float speedMax = 250f;
    public float nextWaypointDistanceMin = 1f;
    public float nextWayPointDistanceMax = 3f;
    public float distanceToDetectPlayer = 5f;

    Path path;
    float speed;
    int currentWaypoint = 0;
    bool inRangeOfPlayer = false;
    bool reachedEndOfPath = false;

    Seeker seeker;
    Rigidbody2D rb;

    private void OnEnable()
    {
        Player.playerTarget += AcquirePlayer;
    }

    private void OnDisable()
    {
        Player.playerTarget -= AcquirePlayer;
    }

    // Start is called before the first frame update
    void Start()
    {
        seeker = GetComponent<Seeker>();
        rb = GetComponent<Rigidbody2D>();
        speed = Random.Range(speedMin, speedMax);

        InvokeRepeating("UpdatePath", 0f, 0.5f);
    }

    void AcquirePlayer(Transform player)
    {
        target = player;
    }

    void UpdatePath()
    {
        if (seeker.IsDone() && inRangeOfPlayer && EndTrigger.backtrackBegin)
            seeker.StartPath(rb.position, target.position, OnPathComplete);
    }

    void OnPathComplete(Path p)
    {
        if (!p.error)
        {
            path = p;
            currentWaypoint = 0;
        }
    }

    private void Update()
    {
        if (target != null)
            CheckPlayerRange();
    }

    private void CheckPlayerRange()
    {
        float distanceToPlayer = Vector2.Distance(rb.position, target.position);
        if (distanceToPlayer <= distanceToDetectPlayer && !inRangeOfPlayer)
        {
            inRangeOfPlayer = true;
        }
        else if (distanceToPlayer > distanceToDetectPlayer && inRangeOfPlayer)
        {
            inRangeOfPlayer = false;
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (path == null)
            return;

        if (currentWaypoint >= path.vectorPath.Count)
        {
            reachedEndOfPath = true;
            return;
        }
        else {
            reachedEndOfPath = false;
        }

        Vector2 direction = ((Vector2)path.vectorPath[currentWaypoint] - rb.position).normalized;     // Vector that points from our position to the waypoint
        Vector2 force = direction * speed * Time.deltaTime;

        rb.AddForce(force);

        float distanceToWaypoint = Vector2.Distance(rb.position, path.vectorPath[currentWaypoint]);
        if (distanceToWaypoint < Random.Range(nextWaypointDistanceMin, nextWayPointDistanceMax))
        {
            currentWaypoint++;
        }

      

        if (rb.velocity.x >= 0.01f)
            transform.localScale = new Vector3(-1f, 1f, 1f);
        else if (rb.velocity.x <= -0.01f)
            transform.localScale = new Vector3(1f, 1f, 1f);
    }
}
