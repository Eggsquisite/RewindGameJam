using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;
using Random = UnityEngine.Random;

public class EnemyAI : MonoBehaviour
{

    private Transform target;
    private Transform clone;
    public float accelMin = 250f;
    public float accelMax = 400f;
    public float nextWaypointDistanceMin = 1f;
    public float nextWayPointDistanceMax = 3f;
    public float distanceToDetectPlayer = 5f;

    Path path;
    float acceleration;
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
        acceleration = Random.Range(accelMin, accelMax);
        GameObject go = Instantiate(gameObject);
        clone = go.transform;
        go.SetActive(false);

        InvokeRepeating("UpdatePath", 0f, 0.5f);
    }

    void AcquirePlayer(Transform player)
    {
        target = player;
    }

    void UpdatePath() {
        if (seeker.IsDone() && inRangeOfPlayer && RewindManager.IsRewinding()) 
            seeker.StartPath(rb.position, target.position, OnPathComplete);

        else seeker.StartPath(rb.position, clone.position, OnPathComplete);
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

    private void CheckPlayerRange() {

        if (Vector2.Distance(rb.position, target.position) <= distanceToDetectPlayer)
            inRangeOfPlayer = true;
        else
            inRangeOfPlayer = false;
    }

    // Update is called once per frame
    void FixedUpdate() {
        //Debug.Log(currentWaypoint);
        if (path == null) return;
        if (currentWaypoint >= path.vectorPath.Count) return;
        
        
        Vector2 direction = ((Vector2)path.vectorPath[currentWaypoint] - rb.position);     // Vector that points from our position to the waypoint
        Vector2 force = direction.normalized * acceleration * Time.fixedDeltaTime;
        Vector2 dir = (Vector2) (clone.position) - rb.position;
        float dist = dir.magnitude;
        if (!inRangeOfPlayer && dist <= 1f) {
            //float dampingFactor = 2f;
            force -= rb.velocity*4f;
        }
        rb.AddForce(force);

        if (direction.magnitude < Random.Range(nextWaypointDistanceMin, nextWayPointDistanceMax))
            currentWaypoint++;


        if (force.x > 0f)
            transform.localScale = new Vector3(-1f, 1f, 1f);
        else 
            transform.localScale = new Vector3(1f, 1f, 1f);
    }
}
