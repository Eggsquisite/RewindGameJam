using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndCollapse : MonoBehaviour
{

    [Range(0f, 1f)]
    [SerializeField] float fadeToColorAmount = 0f;

    [SerializeField] float transitionSpeed = 0.05f;

    SpriteRenderer sp;
    bool endTrigger = false;


    // Start is called before the first frame update
    void Start()
    {
        if (sp == null)
            sp = GetComponent<SpriteRenderer>();

        // Access color options
        Color color = sp.material.color;

        // Set initial values for R and B
        color.r = 1f;
        color.b = 1f;

        EndTrigger.onAction += StartFadeToRed;
    }

    private void OnDisable()
    {
        EndTrigger.onAction -= StartFadeToRed;
    }

    private void Begin()
    {
        StartCoroutine(FadeToRed());
    }

    IEnumerator FadeToRed()
    {
        for (float i = 1f; i >= fadeToColorAmount; i -= 0.05f)
        {
            Color c = sp.material.color;

            c.b = i;
            c.g = i;

            sp.material.color = c;

            yield return new WaitForSeconds(transitionSpeed);
        }
    }

    public void StartFadeToRed(float delay)
    {
        if (!endTrigger)
        {
            Debug.Log("starting collapse");
            Invoke("Begin", delay);
            endTrigger = true;
        }
    }
}
