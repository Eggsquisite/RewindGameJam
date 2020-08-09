using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EndTrigger : MonoBehaviour
{
    public delegate void TriggerAction(float delay);
    public static event TriggerAction onAction;
    public static int torchNum = 0;
    public static bool backtrackBegin = false;

    [SerializeField] float delay = 2f;

    GameObject cam;
    GameObject goal;
    Animator anim;
    int torches;
    bool inRange = false;
    bool endReady = false;

    // Start is called before the first frame update
    void Start()
    {
        backtrackBegin = false;

        if (cam == null)
            cam = Camera.main.gameObject;

        if (anim == null)
            anim = GetComponent<Animator>();

        if (torchNum == 0)
            TorchesLit();
    }

    private void OnEnable()
    {
        onAction += BeginRewind;
        Goal.acquireGoal += AcquireGoal;
        Torch.onTrigger += TorchesLit;
    }


    private void OnDisable()
    {
        onAction -= BeginRewind;
        Goal.acquireGoal -= AcquireGoal;
        Torch.onTrigger -= TorchesLit;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && inRange && endReady)
        {
            onAction?.Invoke(delay);
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

    private void BeginRewind(float delay)
    {
        Invoke("SetBacktrack", delay);
        StartCoroutine(LightUp());
    }

    private IEnumerator LightUp()
    {
        yield return new WaitForSeconds(.65f);
        anim.SetBool("lit", true);
    }

    private void SetBacktrack()
    {
        backtrackBegin = true;
        goal.SetActive(true);
        goal.GetComponent<Collider2D>().enabled = true;
        goal.GetComponent<Animator>().SetTrigger("fadeIn");
    }

    private void AcquireGoal(GameObject g)
    {
        goal = g;
    }

    private void TorchesLit()
    {
        torches++;

        if (torches >= torchNum)
        {
            Debug.Log("all torches lit");
            endReady = true;
            anim.SetTrigger("ready");
        }
    }
}
