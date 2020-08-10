using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartTrigger : MonoBehaviour
{
    public delegate void TriggerAction(float delay);
    public static event TriggerAction onStart;

    [SerializeField] float delay = 2f;

    Animator anim;
    SpriteRenderer sp;

    // Start is called before the first frame update
    void Start()
    {

        anim = GetComponent<Animator>();
        sp = GetComponent<SpriteRenderer>();

        if (Player.checkpointReached) sp.enabled = false;

        onStart?.Invoke(delay);
        Invoke("SpawnOut", delay);
    }

    private void SpawnOut()
    {
        anim.SetTrigger("fadeOut");
    }
}
