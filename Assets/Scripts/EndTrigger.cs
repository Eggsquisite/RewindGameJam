using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EndTrigger : MonoBehaviour
{
    public delegate void TriggerAction(float delay);
    public static event TriggerAction onAction;


    public delegate void RewindUI(bool status);
    public static event RewindUI enableUI;

    public static int torchNum = 0;
    public static bool backtrackBegin = false;

    [SerializeField] float delay = 2f;

    GameObject cam;
    GameObject goal;
    Animator anim;
    Collider2D coll;
    Transform checkpointPosition;
    int torches;
    bool inRange = false;
    bool endReady = false;
    bool checkpoint = false;

    // Start is called before the first frame update
    void Start()
    {
        backtrackBegin = false;

        if (torchNum == 0) TorchesLit();
        if (cam == null) cam = Camera.main.gameObject;
        if (anim == null) anim = GetComponent<Animator>();
        if (coll == null)
        {
            coll = GetComponent<Collider2D>();
            coll.enabled = false;
        }
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
        if (endReady && Input.GetKeyDown(KeyCode.E) && inRange)
        {
            onAction?.Invoke(delay);
        }
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            var playerVar = collision.GetComponent<Player>();
            playerVar.SetCheckpoint(transform.position);
            playerVar.LightRange(true);

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
        enableUI?.Invoke(true);

        goal.SetActive(true);
        goal.GetComponent<Goal>().BonfireLit();
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
            coll.enabled = true;
            anim.SetTrigger("ready");
        }
    }
}
