using Newtonsoft.Json;
using OVRSimpleJSON;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Networking;

public class TTSController : MonoBehaviour
{
    public AudioSource ttsAudio;
    // TTS (Text To Speech)
    private string serverUrl;
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

    public IEnumerator TextToSpeech(string mode, string message)
    {
        Message textMessage = new Message{ text = message };
        string inputJson = JsonUtility.ToJson(textMessage);
        byte[] jsonToSend = new System.Text.UTF8Encoding().GetBytes(inputJson);

        UnityWebRequest www = new UnityWebRequest(serverUrl + "/tts/" + mode, "POST");
        www.uploadHandler = new UploadHandlerRaw(jsonToSend);
        www.downloadHandler = new DownloadHandlerBuffer();
        www.SetRequestHeader("Content-Type", "application/json");

        yield return www.SendWebRequest();
        if (www.result != UnityWebRequest.Result.Success)
        {
            Debug.LogError("전송 실패: " + www.error);
        }
        else //성공 
        {
            TTSResponseData responseData = JsonUtility.FromJson<TTSResponseData>(www.downloadHandler.text);
            Debug.Log("TTS 생성 완료: " + responseData.result);
            // tts 저장하기 위한 decode 및 저장, 실행 로직
            byte[] mp3Data = Convert.FromBase64String(responseData.result);
            string path = Application.persistentDataPath+"/Datas/tts.mp3";
            File.WriteAllBytes(path, mp3Data);

            StartCoroutine(PlayTTS(path));
        }
    }

    IEnumerator PlayTTS(string path)
    {
        using (UnityWebRequest www = UnityWebRequestMultimedia.GetAudioClip("file://" + path, AudioType.MPEG))
        {
            yield return www.SendWebRequest();
            if (www.result == UnityWebRequest.Result.ConnectionError || www.result == UnityWebRequest.Result.ProtocolError)
            {
                Debug.LogError("오디오 재생 오류: " + www.error);
            }
            else
            {
                AudioClip clip = DownloadHandlerAudioClip.GetContent(www);
                ttsAudio.clip = clip;
                ttsAudio.Play();
            }
        }
    }
}

[System.Serializable]
class TTSResponseData
{
    public string result;
    public int status_code;
}

[System.Serializable]
class Message
{
    public string text;
}