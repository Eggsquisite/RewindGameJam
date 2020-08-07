using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goal : MonoBehaviour
{
    public delegate void GoalReached(GameObject g);
    public static event GoalReached loadLevel, acquireGoal;

    [SerializeField] float delay = 2f;

    private void Start()
    {
        acquireGoal(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            collision.GetComponent<Player>().Invincible(true);
            StartCoroutine(LoadNextLevel());
        }
    }

    IEnumerator LoadNextLevel()
    {
        yield return new WaitForSeconds(delay);
        loadLevel(null);
    }
}
