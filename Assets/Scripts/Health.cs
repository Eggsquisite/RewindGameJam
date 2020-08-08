using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] int healthPos;

    private static int maxHealth = 0;
    Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    public static void SetPlayerHealth(int val)
    {
        maxHealth = val;
    }

    public static void PlayerHurt(int val)
    {

    }
}
