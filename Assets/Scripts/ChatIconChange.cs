using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChatIconChange : MonoBehaviour
{
    public GameObject chatIcon;
    public GameObject recIcon;
    public GameObject loadingIcon;
    private void Start()
    {
        chatIcon.SetActive(true);
        recIcon.SetActive(false);
        loadingIcon.SetActive(false);
    }

    public void ChangeToRecord()
    {
        chatIcon.SetActive(false);
        loadingIcon.SetActive(false);
        recIcon.SetActive(true);
    }
    public void ChangeToLoading()
    {
        chatIcon.SetActive(false);
        recIcon.SetActive(false);
        loadingIcon.SetActive(true);
    }
    public void ChangeToChat()
    {
        recIcon.SetActive(false);
        loadingIcon.SetActive(false);
        chatIcon.SetActive(true);
    }
}
