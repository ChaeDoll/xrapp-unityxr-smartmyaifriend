using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CallingSmaf : MonoBehaviour
{
    private GameObject instantiatedSmaf;
    SMAFMovement smafControl;
    public void CallSmaf()
    {
        instantiatedSmaf = GameObject.Find("SMAF_MyRoom(Clone)");
        if (instantiatedSmaf == null)
        {
            instantiatedSmaf = GameObject.Find("SMAT_MyRoom(Clone)");

        }
        if (instantiatedSmaf != null)
        {
            smafControl = instantiatedSmaf.GetComponent<SMAFMovement>();
            smafControl.CallSMAF();
        }
    }
}
