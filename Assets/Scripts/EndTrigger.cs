﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EndTrigger : MonoBehaviour
{
    [SerializeField] GameObject goal = null;
    [SerializeField] GameObject endText = null;

    public delegate void TriggerAction();
    public static event TriggerAction onAction;

    GameObject cam;
    Animator anim;
    bool inRange = false;

    // Start is called before the first frame update
    void Start()
    {
        if (cam == null)
            cam = Camera.main.gameObject;

        if (anim == null)
            anim = GetComponent<Animator>();
    }

    private void OnEnable()
    {
        onAction += BeginRewind;
    }

    private void OnDisable()
    {
        onAction -= BeginRewind;
    }

    private void Update()
    {
        if (inRange && Input.GetKeyDown(KeyCode.E))
        { 
            onAction();
            Debug.Log("E pressed");        
        }
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            collision.gameObject.GetComponent<Player>().LightRange(true);
            if (!inRange)
                inRange = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            collision.gameObject.GetComponent<Player>().LightRange(false);
            if (inRange)
                inRange = false;
        }
    }

    private void BeginRewind()
    {
        StartCoroutine(LightUp());
        goal.SetActive(true);
    }

    private IEnumerator LightUp()
    {
        yield return new WaitForSeconds(1f);
        anim.SetBool("lit", true);
    }
}
