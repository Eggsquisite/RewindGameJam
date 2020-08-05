using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EndTrigger : MonoBehaviour
{
    [SerializeField] GameObject doorTwo;
    [SerializeField] float endDelay = 1f;
    [SerializeField] bool endDoor = false;
    [SerializeField] GameObject endText = null;
    GameObject cam;

    // Start is called before the first frame update
    void Start()
    {
        if (cam == null)
            cam = Camera.main.gameObject;
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player" && !endDoor)
            BeginRewind();
        else if (collision.tag == "Player" && endDoor)
            EndLevel();
    }

    private void BeginRewind()
    {
        if (doorTwo != null)
        {
            cam.GetComponent<CamMovement>().Rewind(endDelay);
            doorTwo.SetActive(true);
            gameObject.SetActive(false);
        }
    }

    private void EndLevel()
    {
        endText.SetActive(true);
        endText.GetComponent<Text>().text = "Level Won!";
    }
}
