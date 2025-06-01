using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MicTestT : MonoBehaviour
{
    AudioClip record;
    AudioSource audio;
    string selectedMic = null;

    public GameObject recordingTimer;
    public SendToServerT sendController;

    // Start is called before the first frame update
    void Start()
    {
        audio = GetComponent<AudioSource>();
    }
    public void PlaySnd()
    {
        audio.Play();
    }
    public void RecSnd()
    {
        // 디바이스 확인용 코드로, 생략해도 된다
        foreach (var device in Microphone.devices)
        {
            Debug.Log("Name: " + device);
        }
        if (!sendController.isSending)
        {
            if (selectedMic != null)
            {
                record = Microphone.Start(selectedMic, false, 7, 44100); // 선택된 마이크로 7초 녹음
            }
            else
            {
                record = Microphone.Start(Microphone.devices[0].ToString(), false, 7, 44100); // 7초 녹음
            }
            audio.clip = record;
            StartCoroutine(ShowRecording());
        }
    }
    float currentTime;
    IEnumerator ShowRecording()
    {
        recordingTimer.SetActive(true);
        Slider recordingSlider = recordingTimer.GetComponent<Slider>();
        recordingSlider.value = 0;
        currentTime = 0f;
        while (currentTime < 7 && !sendController.isSending) //SendToServer 스크립트의 isSending을 가져와서 Sending되는순간 Slider가 멈추도록
        {
            currentTime += Time.deltaTime;
            recordingSlider.value = currentTime;
            yield return null;
        }
    }

    public void SelectMicDevice(string device)
    {
        selectedMic = device;
    }
}
