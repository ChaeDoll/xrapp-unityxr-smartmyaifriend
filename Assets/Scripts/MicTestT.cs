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
        // ����̽� Ȯ�ο� �ڵ��, �����ص� �ȴ�
        foreach (var device in Microphone.devices)
        {
            Debug.Log("Name: " + device);
        }
        if (!sendController.isSending)
        {
            if (selectedMic != null)
            {
                record = Microphone.Start(selectedMic, false, 7, 44100); // ���õ� ����ũ�� 7�� ����
            }
            else
            {
                record = Microphone.Start(Microphone.devices[0].ToString(), false, 7, 44100); // 7�� ����
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
        while (currentTime < 7 && !sendController.isSending) //SendToServer ��ũ��Ʈ�� isSending�� �����ͼ� Sending�Ǵ¼��� Slider�� ���ߵ���
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
