using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnableOnEndTrigger : MonoBehaviour
{
    SpriteRenderer sp = null;
    Collider2D coll = null;

    private void OnEnable()
    {
        EndTrigger.onAction += Backtrack;
    }

    private void OnDisable()
    {
        EndTrigger.onAction -= Backtrack;
    }

    // Start is called before the first frame update
    void Start()
    {
        if (sp == null) sp = GetComponent<SpriteRenderer>();
        if (coll == null) coll = GetComponent<Collider2D>();

        EnableGameObject(false);
    }

    private void Backtrack(float delay)
    {
        // delay is not used in this function
        EnableGameObject(true);
    }

    private void EnableGameObject(bool status)
    {
        sp.enabled = status;
        coll.enabled = status;
    }

}
