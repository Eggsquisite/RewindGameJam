using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] int healthPos;

    private static int maxHealth = 0;
    Animator anim;

    private void OnEnable()
    {
        Player.setHealth += SetPlayerHealth;
        Player.playerDamaged += PlayerHurt;
    }

    private void OnDisable()
    {
        Player.setHealth -= SetPlayerHealth;
        Player.playerDamaged -= PlayerHurt;
    }

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    public void SetPlayerHealth(int val)
    {
        maxHealth = val;
    }

    public void PlayerHurt(int val)
    {
        if (healthPos == val)
            anim.SetTrigger("hurt");
    }
}
