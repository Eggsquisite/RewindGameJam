using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Torch : MonoBehaviour
{
    public delegate void TorchTriggered();
    public static event TorchTriggered onTrigger;

    public static bool firstTorch = true;
    Animator anim;
    Collider2D coll;

    private void OnEnable()
    {
        if (firstTorch && EndTrigger.torchNum > 0)
        {
            EndTrigger.torchNum = 0;
            firstTorch = false;
        }

        firstTorch = false;
        EndTrigger.torchNum++;
    }

    private void Start()
    {
        anim = GetComponent<Animator>();
        coll = GetComponent<Collider2D>();

        if (Player.checkpointReached)
            LightUp();
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
        coll.enabled = false;
    }
}
