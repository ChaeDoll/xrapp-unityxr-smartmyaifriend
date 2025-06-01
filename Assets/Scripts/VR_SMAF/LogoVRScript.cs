using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LogoVRScript : MonoBehaviour
{
    public GameObject vrRoomLogo;
    public TutorialVRButton tutorialVRController;
    Transform eyePosition;
    void Start()
    {
        vrRoomLogo.SetActive(false);
        if (PlayerPrefs.GetInt("vrTutorialCompleted") != 1)
        {
            vrRoomLogo.SetActive(true);
            eyePosition = GameObject.Find("Interaction Camera Rig/TrackingSpace/CenterEyeAnchor").GetComponent<Transform>();
        }
    }
    void Update()
    {
        if (eyePosition != null)
        {
            vrRoomLogo.transform.position = eyePosition.position + eyePosition.forward * 0.3f;
            vrRoomLogo.transform.rotation = Quaternion.LookRotation(eyePosition.forward);
        }
    }
    public void StartTutorial()
    {
        vrRoomLogo.SetActive(false);
        tutorialVRController.CreateTutorial();
        PlayerPrefs.SetInt("vrTutorialCompleted", 1);
    }
}
