using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SubtitleInit : MonoBehaviour
{
    public GameObject subtitleCanvas;
    public TMP_Text subtitleText;
    // Start is called before the first frame update
    void Start()
    {
        subtitleCanvas.SetActive(false);
    }
}
