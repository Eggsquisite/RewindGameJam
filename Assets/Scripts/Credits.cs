using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Credits : MonoBehaviour
{
    [SerializeField] GameObject credits, title;

    public void CreditsScreen()
    {
        credits.SetActive(true);
        title.SetActive(false);
    }

    public void TitleScreen()
    {
        credits.SetActive(false);
        title.SetActive(true);
    }
}
