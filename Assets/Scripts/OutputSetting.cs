using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OutputSetting : MonoBehaviour
{
    public Toggle bubbleDialog;
    public Toggle subtitleDialog;
    SMAFChatController chatController;
    SMAFChatControllerT chatControllerT;
    GameObject InstantiateSmaf;
    GameObject InstantiateSmafT;

    void Update()
    {
        InstantiateSmaf = GameObject.Find("SMAF_MyRoom(Clone)");
        InstantiateSmafT = GameObject.Find("SMAT_MyRoom(Clone)");
        if (InstantiateSmaf == null && InstantiateSmafT == null)
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
        if (InstantiateSmaf != null)
        {
            chatController = InstantiateSmaf.GetComponent<SMAFChatController>();
            if (chatController != null)
            {
                chatController.ToggleOutputMode(currentCheck);
                bubbleDialog.SetIsOnWithoutNotify(currentCheck);
                subtitleDialog.SetIsOnWithoutNotify(!currentCheck);
            }
        }
        else if (InstantiateSmafT != null)
        {
            chatControllerT = InstantiateSmafT.GetComponent<SMAFChatControllerT>();
            if (chatControllerT != null)
            {
                chatControllerT.ToggleOutputMode(currentCheck);
                bubbleDialog.SetIsOnWithoutNotify(currentCheck);
                subtitleDialog.SetIsOnWithoutNotify(!currentCheck);
            }
        }
    }
    public void TurnOnSubtitle(bool prevCheck)
    {
        bool currentCheck = !prevCheck;
        if (InstantiateSmaf != null)
        {
            chatController = InstantiateSmaf.GetComponent<SMAFChatController>();
            if (chatController != null)
            {
                chatController.ToggleOutputMode(!currentCheck);
                bubbleDialog.SetIsOnWithoutNotify(!currentCheck);
                subtitleDialog.SetIsOnWithoutNotify(currentCheck);
            }
        }
        else if (InstantiateSmafT != null)
        {
            chatControllerT = InstantiateSmafT.GetComponent<SMAFChatControllerT>();
            if (chatControllerT != null)
            {
                chatControllerT.ToggleOutputMode(!currentCheck);
                bubbleDialog.SetIsOnWithoutNotify(!currentCheck);
                subtitleDialog.SetIsOnWithoutNotify(currentCheck);
            }
        }
    }
}
