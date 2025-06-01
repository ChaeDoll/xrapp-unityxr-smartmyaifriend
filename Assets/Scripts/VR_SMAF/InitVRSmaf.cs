using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitVRSmaf : MonoBehaviour
{
    public GameObject vrSmafPrefab;
    GameObject instantiatedSmaf;
    SMAFChatController smafChatController;
    void Start()
    {
        instantiatedSmaf = Instantiate(vrSmafPrefab, Vector3.zero, Quaternion.identity);
        smafChatController = instantiatedSmaf.GetComponent<SMAFChatController>();
        smafChatController.HomeChat();
    }
}
