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

    private bool outputMode = true; // true : 말풍선, false : 자막

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
            bubbleText.text = "짜잔!";
        }
        else
        {
            subtitleDialog.SetActive(true);
            subtitleText.text = "짜잔!";
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
            bubbleText.text = "우리 집이다!";
        }
        else
        {
            subtitleDialog.SetActive(true);
            subtitleText.text = "우리 집이다!";
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
            bubbleText.text = "안녕! 난 집에 가볼게";
        }
        else
        {
            subtitleDialog.SetActive(true);
            subtitleText.text = "안녕! 난 집에 가볼게";
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
            bubbleText.text = "음~ 맛있다!!";
        }
        else
        {
            subtitleDialog.SetActive(true);
            subtitleText.text = "음~ 맛있다!!";
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
