using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartTrigger : MonoBehaviour
{
    public delegate void TriggerAction(float delay);
    public static event TriggerAction onStart;

    [SerializeField] float delay = 2f;

    Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();

        onStart?.Invoke(delay);
        Invoke("SpawnOut", delay);
    }

    private void SpawnOut()
    {
        anim.SetTrigger("fadeOut");
    }
}
