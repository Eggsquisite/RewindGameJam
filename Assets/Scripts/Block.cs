using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{

    private Transform t;
    private BoxCollider2D coll;
    private Animator anim;

    // How long the object should shake for.
    public float shakeDuration = 0f;

    // Amplitude of the shake. A larger value shakes the camera harder.
    public float shakeAmount = 0.7f;
    public float decreaseFactor = 1.0f;

    Vector3 originalPos;
    bool playerEnter = false;

    void Awake()
    {
        if (t == null)
            t = GetComponent(typeof(Transform)) as Transform;

        if (coll == null)
            coll = GetComponent<BoxCollider2D>();

        if (anim == null)
            anim = GetComponent<Animator>();
    }

    void OnEnable()
    {
        originalPos = t.localPosition;
    }

    void Update()
    {
        if (playerEnter)
            Shake();
    }

    private void Shake()
    {
        if (shakeDuration > 0)
        {
            t.localPosition = originalPos + Random.insideUnitSphere * shakeAmount;

            shakeDuration -= Time.deltaTime * decreaseFactor;
        }
        else
        {
            shakeDuration = 0f;
            t.localPosition = originalPos;
            Exit();
        }
    }

    private void Exit()
    {
        coll.enabled = false;
        anim.SetBool("disappear", true);
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            playerEnter = true;
        }
    }
}
