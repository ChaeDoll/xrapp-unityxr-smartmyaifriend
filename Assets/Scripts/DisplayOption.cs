using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisplayOption : MonoBehaviour
{
    public GameObject optionDialog;
    public Transform eyePosition;
    bool showOption = false;
    public MicSetting micSetting;
    // Start is called before the first frame update
    void Start()
    {
        optionDialog.SetActive(showOption);
    }

    public void ToggleOptionDisplay()
    {
        showOption = !showOption;
        optionDialog.SetActive(showOption);
        if (showOption)
        {
            micSetting.InitialDropdown();
            optionDialog.transform.position = eyePosition.position + eyePosition.forward * 0.4f;
            optionDialog.transform.rotation = Quaternion.LookRotation(eyePosition.forward);
        }
    }
}
