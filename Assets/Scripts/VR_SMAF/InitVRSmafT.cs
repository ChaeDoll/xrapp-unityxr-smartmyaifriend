using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitVRSmafT : MonoBehaviour
{
    public GameObject vrSmafPrefab;
    GameObject instantiatedSmaf;
    SMAFChatControllerT smafChatController;
    void Start()
    {
        instantiatedSmaf = Instantiate(vrSmafPrefab, Vector3.zero, Quaternion.identity);
        smafChatController = instantiatedSmaf.GetComponent<SMAFChatControllerT>();
        smafChatController.HomeChat();
    }
}
