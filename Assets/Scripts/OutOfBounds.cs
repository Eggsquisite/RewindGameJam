﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OutOfBounds : MonoBehaviour
{
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            //collision.GetComponent<Player>().OutOfBounds();
            collision.GetComponent<Player>().Hurt(999);
        }
    }
}
