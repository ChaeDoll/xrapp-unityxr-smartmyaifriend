using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SMAFMenuTutorial : MonoBehaviour
{
    public GameObject tutorialCloud;
    private bool tutorialActivate = true;

    public void ToggleSMAFTutorial()
    {
        tutorialActivate = !tutorialActivate;
        tutorialCloud.SetActive(tutorialActivate);
    }
}
