using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CallingVRSmafT : MonoBehaviour
{
    private GameObject instantiatedSmaf;
    SMAFMovement smafControl;
    public void CallSmaf()
    {
        instantiatedSmaf = GameObject.Find("SMAT_VrRoom(Clone)");
        if (instantiatedSmaf != null)
        {
            smafControl = instantiatedSmaf.GetComponent<SMAFMovement>();
            smafControl.CallSMAF();
        }
    }
}
