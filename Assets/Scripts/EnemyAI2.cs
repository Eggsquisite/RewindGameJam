using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI2 : MonoBehaviour {
    
    public GameObject playerGO;
    public float maxSpeed = 5f;
    public float acceleration = 0.1f;
    private float speed;
    
    private Transform player;

    private Vector3 startingPosition;
    private float tol = 0.01f;

    public float playerDetectDist = 8f;
    private Vector3 dir;

    private bool playerInRange = false;
    
    
    // Start is called before the first frame update
    void Start() {
        GameObject clone = Instantiate(gameObject);
        clone.SetActive(false);
        startingPosition = clone.transform.position;
        player = playerGO.transform;

        
        
        InvokeRepeating("UpdatePath", 0f, 0.5f);
    }


    void UpdatePath() {
        if (( dir = (player.position - transform.position)).magnitude <= playerDetectDist) {
            playerInRange = true;
            
            dir.Normalize();
        }
        else playerInRange = false;
    }

    private void FixedUpdate() {
        if (playerInRange) {
            //float zRot = Quaternion.LookRotation(dir).eulerAngles.z;
            //transform.rotation.
            transform.LookAt(player);
            if (speed != maxSpeed) {
                speed += acceleration * Time.fixedDeltaTime;
                if (speed >= maxSpeed) speed = maxSpeed;
            }

            //transform.position += transform.forward * speed * Time.fixedDeltaTime;
        }
        else if ((transform.position - startingPosition).magnitude >= tol) {
            Vector3 returnVect = (startingPosition - transform.position).normalized;
        }
    }
}
