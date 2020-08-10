using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goal : MonoBehaviour
{
    public delegate void GoalReached(GameObject g);
    public static event GoalReached loadLevel, acquireGoal;

    [SerializeField] float delay = 2f;
    [SerializeField] Transform setPosition;

    private Transform player;
    private Animator anim;
    private SpriteRenderer sp;
    private Collider2D coll;
    private bool spawningOut = false;

    private void Start()
    {
        acquireGoal(gameObject);

        if (anim == null) anim = GetComponent<Animator>();
        if (sp == null) sp = GetComponent<SpriteRenderer>();
        if (coll == null) coll = GetComponent<Collider2D>();
    }

    private void Update()
    {
        if (spawningOut)
            player.position = new Vector2(setPosition.position.x, setPosition.position.y);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            spawningOut = true;
            player = collision.transform;
            collision.GetComponent<Player>().SpawnOut();
            StartCoroutine(LoadNextLevel());
        }
    }

    public void BonfireLit()
    {
        coll.enabled = true;
        sp.enabled = true;
        anim.SetTrigger("fadeIn");
    }

    IEnumerator LoadNextLevel()
    {
        yield return new WaitForSeconds(delay);
        loadLevel(null);
    }
}
