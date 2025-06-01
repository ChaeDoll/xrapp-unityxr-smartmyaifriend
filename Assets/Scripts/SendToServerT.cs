using Oculus.Interaction.Input;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class SendToServerT : MonoBehaviour
{
    public SMAFChatControllerT textUpdater;
    private string serverUrl;
    AudioSource audio;
    public GameObject recordingTimer;
    public bool isSending = false;
    public ChatIconChange iconChanger;

    public MicTestT micController;

    void Start()
    {
        audio = GetComponent<AudioSource>();
        LoadConfig();
    }
    void LoadConfig()
    {
        TextAsset configTextAsset = Resources.Load<TextAsset>("config");
        Config_T config = JsonUtility.FromJson<Config_T>(configTextAsset.text);
        serverUrl = config.serverUrl;
    }

    public void SendingToServer()
    {
        if (audio.clip != null && !isSending)
        {
            var recTime = micController.recordingTimer.GetComponent<Slider>().value;
            if (recTime > 0.5)
            {
                SaveWav.Save(Application.persistentDataPath + "/Datas/audioData", audio.clip);
                isSending = true;
                StartCoroutine(ReadWAVFile());
            }
            else
            {
                isSending = true;
                micController.recordingTimer.GetComponent<Slider>().value = 0;
                micController.recordingTimer.SetActive(false);
                iconChanger.ChangeToChat();
                FlickRec();
            }
        }
    }
    void FlickRec()
    {
        isSending = false;
        // ������ ��� ���� �� ����.
        //textUpdater.UpdateMessage("��?");
    }

    IEnumerator ReadWAVFile()
    {
        string filePath = Application.persistentDataPath + "/Datas/audioData.wav";
        if (!System.IO.File.Exists(filePath))
        {
            Debug.LogError("File not found: " + filePath);
            yield break;
        }
        byte[] wavData;
        try
        {
            wavData = System.IO.File.ReadAllBytes(filePath);
        }
        catch (System.Exception ex)
        {
            Debug.LogError("File reading failed: " + ex.Message);
            yield break;
        }
        SendWAVDataAsJSON(wavData);
        yield return null;
    }

    void SendWAVDataAsJSON(byte[] wavData)
    {
        // JSON ��ȯ �� ���� ���� ȣ��
        string wav = WAVDataToJSON(wavData);
        StartCoroutine(Sending(wav));
    }
    string WAVDataToJSON(byte[] wavData)
    {
        // Base64 ���ڵ��� ���� ����Ʈ �迭�� ���ڿ��� ��ȯ
        string base64WAV = System.Convert.ToBase64String(wavData);
        return base64WAV;
    }

    IEnumerator Sending(string base64Wav)
    {
        AudioData_T formData = new AudioData_T
        {
            audio = base64Wav,
            filename = "audioData.wav"
        };
        string json = JsonUtility.ToJson(formData);
        UnityWebRequest www = new UnityWebRequest(serverUrl+"/saveVoice/t", "POST");
        byte[] jsonToSend = new System.Text.UTF8Encoding().GetBytes(json);

        www.uploadHandler = new UploadHandlerRaw(jsonToSend);
        www.downloadHandler = new DownloadHandlerBuffer();
        www.SetRequestHeader("Content-Type", "application/json");

        yield return www.SendWebRequest();
        isSending = false;
        iconChanger.ChangeToChat();
        if (www.result != UnityWebRequest.Result.Success)
        {
            Debug.LogError("���� ����: " + www.error);
            textUpdater.UpdateMessage("��? �����? �ٽ� �� �� �����ٷ�?");
            recordingTimer.SetActive(false);
        }
        else
        {
            MyResponseData_T responseData = JsonUtility.FromJson<MyResponseData_T>(www.downloadHandler.text);
            textUpdater.UpdateMessage(responseData.result);
            Debug.Log("���� ����: " + responseData.result);
            recordingTimer.SetActive(false);
        }
    }
}

[System.Serializable]
public class AudioData_T
{
    public string audio;
    public string filename;
}

[System.Serializable]
public class MyResponseData_T
{
    public string result;
    public int status_code;
}

[System.Serializable]
public class Config_T
{
    public string serverUrl;
}