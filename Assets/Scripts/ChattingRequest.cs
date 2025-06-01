using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class ChattingRequest : MonoBehaviour
{
    private string serverUrl;
    private bool isSending = false;
    public SMAFChatController textUpdater;
    public SMAFChatControllerT textUpdaterT;

    void Start()
    {
        LoadConfig();
    }
    void LoadConfig()
    {
        TextAsset configTextAsset = Resources.Load<TextAsset>("config");
        Config config = JsonUtility.FromJson<Config>(configTextAsset.text);
        serverUrl = config.serverUrl;
    }
    public void RequestSMAFChat(string characterName)
    {
        if (!isSending)
        {
            isSending = true;
            if (characterName == "SMAF")
            {
                StartCoroutine(RequestToServer("f"));
            }
            else if (characterName == "SMAT")
            {
                StartCoroutine(RequestToServer("t"));
            }
        }
    }
    IEnumerator RequestToServer(string mode)
    {
        UnityWebRequest www = new UnityWebRequest(serverUrl+ "/conversation/start/" + mode, "POST");
        www.downloadHandler = new DownloadHandlerBuffer();
        www.SetRequestHeader("Content-Type", "application/json");
        yield return www.SendWebRequest();
        if (www.result != UnityWebRequest.Result.Success)
        {
            Debug.LogError("전송 실패: " + www.error);
            if (mode == "f")
            {
                textUpdater.UpdateMessage("지금은 피곤해서 나중에 말 해줄게 미안해..");
            }
            else if (mode == "t")
            {
                textUpdaterT.UpdateMessage("지금은 피곤해.. 나중에 다시 물어봐");
            }
        }
        else
        {
            MyResponseData responseData = JsonUtility.FromJson<MyResponseData>(www.downloadHandler.text);
            Debug.Log("전송 성공: " + responseData.result);
            if (mode == "f")
            {
                textUpdater.UpdateMessage(responseData.result);
            }
            else if (mode == "t")
            {
                textUpdaterT.UpdateMessage(responseData.result);
            }
        }
        isSending = false;
    }
}
