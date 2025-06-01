using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CallingVRSmaf : MonoBehaviour
{
    private GameObject instantiatedSmaf;
    SMAFMovement smafControl;
    public void CallSmaf()
    {
        instantiatedSmaf = GameObject.Find("SMAF_VrRoom(Clone)");
        if (instantiatedSmaf != null)
        {
            smafControl = instantiatedSmaf.GetComponent<SMAFMovement>();
            smafControl.CallSMAF();
        }
    }
}
