using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindmillMovesPlatform : MonoBehaviour {
    
    public GameObject windmillGO;
    public Vector2 position1;
    public Vector2 position2;
    private MoveableObject windmill;
    private Rigidbody2D rb;
    private Vector2 dir;
    public float platformSpeed = 0.01f;
    
    void Start() {
        windmill = windmillGO.GetComponent<MoveableObject>();
        rb = windmillGO.GetComponent<Rigidbody2D>();
        dir = (position1 - position2).normalized;
    }

    void FixedUpdate() {

        if (!RewindManager.IsRewinding()) {
            //float angularVel = windmill.GetAngularVelocity();
            float angularVel = rb.angularVelocity * platformSpeed;

            if (angularVel != 0) {
                Vector2 newPos = (Vector2)(transform.position) + (angularVel * dir * Time.fixedDeltaTime);
                if (newPos.x < position1.x || newPos.x > position2.x) {} //do nothing
                else {
                    transform.Translate(angularVel * dir * Time.fixedDeltaTime);
                }
            }
        }
    }
}
