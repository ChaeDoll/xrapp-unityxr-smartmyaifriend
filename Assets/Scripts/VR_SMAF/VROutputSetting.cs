using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VROutputSetting : MonoBehaviour
{
    public Toggle bubbleDialog;
    public Toggle subtitleDialog;
    SMAFChatController chatController;
    GameObject InstantiateSmaf;

    void Update()
    {
        InstantiateSmaf = GameObject.Find("SMAF_VrRoom(Clone)");
        if (InstantiateSmaf == null)
        {
            bubbleDialog.interactable = false;
            subtitleDialog.interactable = false;
        }
        else
        {
            bubbleDialog.interactable = true;
            subtitleDialog.interactable = true;
        }
    }
    public void TurnOnBubble(bool prevCheck)
    {
        bool currentCheck = !prevCheck;
        chatController = InstantiateSmaf.GetComponent<SMAFChatController>();
        if (chatController != null)
        {
            chatController.ToggleOutputMode(currentCheck);
            bubbleDialog.SetIsOnWithoutNotify(currentCheck);
            subtitleDialog.SetIsOnWithoutNotify(!currentCheck);
        }
    }
    public void TurnOnSubtitle(bool prevCheck)
    {
        bool currentCheck = !prevCheck;
        chatController = InstantiateSmaf.GetComponent<SMAFChatController>();
        if (chatController != null)
        {
            chatController.ToggleOutputMode(!currentCheck);
            bubbleDialog.SetIsOnWithoutNotify(!currentCheck);
            subtitleDialog.SetIsOnWithoutNotify(currentCheck);
        }
    }
}
