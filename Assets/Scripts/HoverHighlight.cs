using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HoverHighlight : MonoBehaviour
{
    public Outline outline;

    void Start()
    {
        outline = GetComponent<Outline>();
        if (outline != null)
        {
            outline.enabled = false;
        }
    }

    public void TurnOnHighlight()
    {
        if (outline != null)
        {
            outline.enabled = true;
        }
    }
    public void TurnOffHighlight()
    {
        if (outline != null)
        {
            outline.enabled = false;
        }
    }
}
