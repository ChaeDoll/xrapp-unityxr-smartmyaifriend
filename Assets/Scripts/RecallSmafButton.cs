using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecallSmafButton : MonoBehaviour
{
    RecallSmaf smafRecallController;
    RecallSmafT smafRecallControllerT;
    public void OnPressRecallSmaf()
    {
        smafRecallController = GameObject.Find("SMAF_MyRoom(Clone)")?.GetComponent<RecallSmaf>();
        smafRecallControllerT = GameObject.Find("SMAT_MyRoom(Clone)")?.GetComponent<RecallSmafT>();
        if (smafRecallController != null)
        {
            smafRecallController.TurnBackSmafHome();
        }
        if (smafRecallControllerT != null)
        {
            smafRecallControllerT.TurnBackSmafHome();
        }
    }
}
