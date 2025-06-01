using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LogoScript : MonoBehaviour
{
    public GameObject smafLogo;
    public TutorialButton tutorialController;
    Transform eyePosition;
    void Start()
    {
        smafLogo.SetActive(false);
        if (PlayerPrefs.GetInt("tutorialCompleted") != 1)
        {
            smafLogo.SetActive(true);
            eyePosition = GameObject.Find("Interaction Camera Rig/TrackingSpace/CenterEyeAnchor").GetComponent<Transform>();
        }
    }
    void Update()
    {
        if (eyePosition != null)
        {
            smafLogo.transform.position = eyePosition.position + eyePosition.forward * 0.3f;
            smafLogo.transform.rotation = Quaternion.LookRotation(eyePosition.forward);
        }
    }
    public void StartTutorial()
    {
        smafLogo.SetActive(false);
        tutorialController.CreateTutorial();
        PlayerPrefs.SetInt("tutorialCompleted", 1);
    }
}
