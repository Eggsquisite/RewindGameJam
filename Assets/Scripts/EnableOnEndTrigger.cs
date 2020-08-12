using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnableOnEndTrigger : MonoBehaviour
{
    SpriteRenderer sp = null;
    Collider2D coll = null;
    ParticleSystem ps = null;

    private void OnEnable()
    {
        EndTrigger.BackTrackTriggered += Backtrack;
    }

    private void OnDisable()
    {
        EndTrigger.BackTrackTriggered -= Backtrack;
    }

    // Start is called before the first frame update
    void Start()
    {
        if (sp == null) sp = GetComponent<SpriteRenderer>();
        if (coll == null) coll = GetComponent<Collider2D>();
        if (ps == null) ps = GetComponent<ParticleSystem>();

        EnableGameObject(false);
    }

    private void Backtrack(float delay)
    {
        // delay is not used in this function
        EnableGameObject(true);
    }

    private void EnableGameObject(bool status)
    {
        if (!gameObject.activeSelf) gameObject.SetActive(true);
        if (sp != null) sp.enabled = status;
        if (coll != null) coll.enabled = status;
        if (ps != null) ps.enableEmission = status;
    }

}
