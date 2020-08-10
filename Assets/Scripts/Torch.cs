using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Torch : MonoBehaviour
{
    public delegate void TorchTriggered();
    public static event TorchTriggered onTrigger;

    Animator anim;
    Collider2D coll;

    private AudioSource audioSource;
    public AudioClip torchIgnite;
    public static bool firstTorch = true;

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
        audioSource = GameObject.Find("Music").GetComponent<AudioSource>();

        if (Player.checkpointReached)
            Invoke("LightUp", 0.1f);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
            LightUp();
    }

    private void LightUp()
    {
        onTrigger?.Invoke();
        coll.enabled = false;
        anim.SetBool("lit", true);
        audioSource.PlayOneShot(torchIgnite);
    }
}
