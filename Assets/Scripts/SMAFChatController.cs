using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class SMAFChatController : MonoBehaviour
{
    public GameObject bubbleDialog;
    public TMP_Text bubbleText;
    public TTSController ttsController;

    private GameObject subtitleDialog;
    private TMP_Text subtitleText;
    private Coroutine hideCoroutine;

    private bool outputMode = true; // true : ��ǳ��, false : �ڸ�

    void Start()
    {
        SubtitleInit subtitleInit = GameObject.Find("Interaction Camera Rig/TrackingSpace/CenterEyeAnchor/HUD UI")?.GetComponent<SubtitleInit>();
        subtitleDialog = subtitleInit.subtitleCanvas;
        subtitleText = subtitleInit.subtitleText;
        subtitleDialog.SetActive(false);
    }

    public void UpdateMessage(string message)
    {
        if (outputMode)
        {
            bubbleText.text = message;
            bubbleDialog.SetActive(true);
        }
        else
        {
            subtitleText.text = message;
            subtitleDialog.SetActive(true);
        }
        if (hideCoroutine != null)
        {
            StopCoroutine(hideCoroutine);
        }
        hideCoroutine = StartCoroutine(HideSubtitlesAfterDelay(5f));

        OptionProvider optionProvider = GameObject.Find("Dialogs")?.GetComponent<OptionProvider>();
        Toggle ttsToggle = optionProvider.TTSToggle;
        if (ttsToggle.isOn)
        {
            StartCoroutine(ttsController.TextToSpeech("f", message));
        }
    }

    IEnumerator HideSubtitlesAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        bubbleDialog.SetActive(false);
        subtitleDialog.SetActive(false);
    }


    public void TmpChat()
    {
        if (outputMode)
        {
            bubbleDialog.SetActive(true);
            bubbleText.text = "¥��!";
        }
        else
        {
            subtitleDialog.SetActive(true);
            subtitleText.text = "¥��!";
        }
        if (hideCoroutine != null)
        {
            StopCoroutine(hideCoroutine);
        }
        hideCoroutine = StartCoroutine(HideSubtitlesAfterDelay(5f));
    }
    public void HomeChat()
    {
        if (outputMode)
        {
            bubbleDialog.SetActive(true);
            bubbleText.text = "�츮 ���̴�!";
        }
        else
        {
            subtitleDialog.SetActive(true);
            subtitleText.text = "�츮 ���̴�!";
        }
        if (hideCoroutine != null)
        {
            StopCoroutine(hideCoroutine);
        }
        hideCoroutine = StartCoroutine(HideSubtitlesAfterDelay(3f));
    }
    public void RecallChat()
    {
        if (outputMode)
        {
            bubbleDialog.SetActive(true);
            bubbleText.text = "�ȳ�! �� ���� ������";
        }
        else
        {
            subtitleDialog.SetActive(true);
            subtitleText.text = "�ȳ�! �� ���� ������";
        }
        if (hideCoroutine != null)
        {
            StopCoroutine(hideCoroutine);
        }
        hideCoroutine = StartCoroutine(HideSubtitlesAfterDelay(1.5f));
    }
    public void EatSnackChat()
    {
        if (outputMode)
        {
            bubbleDialog.SetActive(true);
            bubbleText.text = "��~ ���ִ�!!";
        }
        else
        {
            subtitleDialog.SetActive(true);
            subtitleText.text = "��~ ���ִ�!!";
        }
        if (hideCoroutine != null)
        {
            StopCoroutine(hideCoroutine);
        }
        hideCoroutine = StartCoroutine(HideSubtitlesAfterDelay(1.5f));
    }
    public void ToggleOutputMode(bool output)
    {
        outputMode = output;
    }
}
