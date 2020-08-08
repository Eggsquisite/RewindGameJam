using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Torch : MonoBehaviour
{
    public delegate void TorchTriggered();
    public static event TorchTriggered onTrigger;

    Animator anim;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        EndTrigger.torchNum += 1;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
            LightUp();
    }

    private void LightUp()
    {
        anim.SetBool("lit", true);
        onTrigger?.Invoke();
    }
}
