using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MicSetting : MonoBehaviour
{
    public TMP_Dropdown micList;
    MicTest recordController;
    MicTestT recordControllerT;
    public void InitialDropdown()
    {
        micList.ClearOptions();
        List<TMP_Dropdown.OptionData> optionList = new List<TMP_Dropdown.OptionData> ();
        foreach (var device in Microphone.devices)
        {
            optionList.Add (new TMP_Dropdown.OptionData (device));
        }
        micList.AddOptions (optionList);
        micList.value = 0;
    }
    public void OnMicSelected(int index)
    {
        recordController = GameObject.Find("SMAF_MyRoom(Clone)/Audio Sender").GetComponent<MicTest>();
        recordControllerT = GameObject.Find("SMAT_MyRoom(Clone)/Audio Sender").GetComponent<MicTestT>();
        if (recordController != null )
        {
            recordController.SelectMicDevice(Microphone.devices[index].ToString());
        }
        if (recordControllerT != null)
        {
            recordControllerT.SelectMicDevice(Microphone.devices[index].ToString());
        }
    }
}
