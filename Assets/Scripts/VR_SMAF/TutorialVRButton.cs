using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using Unity.VisualScripting;
using System.Net.NetworkInformation;

public class TutorialVRButton : MonoBehaviour
{
    public Transform eyePosition;
    public GameObject tutorial;
    public TMP_Text pageText;
    public TMP_Text content;
    public GameObject previousButton;
    public GameObject nextButton;
    public GameObject[] menuHighLights;
    public TMP_Text skipText;
    
    private string[] explanations={
        "방 문에 나가기 버튼을\n집기(Pinch)하여 나갈 수 있어요",
        "손목 메뉴에 방 나가기\n버튼을 누르기(Poke) 해도\n나갈 수 있어요"
    };
    private int page = 1;
    private int totalpage;
    
    Color activeColor = new Color(0 / 255f, 111 / 255f, 192 / 255f);
    Color inactiveColor = new Color(72 / 255f, 72 / 255f, 72 / 255f);

    void Start(){
        totalpage = explanations.Length;
        tutorial.SetActive(false);
        menuHighLights[0].SetActive(false);
    }
    public void CreateTutorial()
    {
        if (!tutorial.activeSelf)
        {
            page = 1;
            tutorial.SetActive(true);
            tutorial.transform.position = eyePosition.position + eyePosition.forward * 0.4f;
            tutorial.transform.rotation = Quaternion.LookRotation(eyePosition.forward);
            Pages(page);
        } 
        else
        {
            tutorial.transform.position = eyePosition.position + eyePosition.forward * 0.4f;
            tutorial.transform.rotation = Quaternion.LookRotation(eyePosition.forward);
        }
    }

    private void Pages(int page){
        menuHighLights[0].SetActive(false);
        pageText.text = page.ToString()+" / "+totalpage.ToString();
        content.text = explanations[page-1];
        if(page == 1){ 
            previousButton.GetComponent<Button>().interactable = false;
            previousButton.transform.GetChild(0).GetComponent<Image>().color = inactiveColor;
            nextButton.GetComponent<Button>().interactable = true;
            nextButton.transform.GetChild(0).GetComponent<Image>().color = activeColor;
            skipText.text = "SKIP";
        }
        else if(page == 2)
        {
            previousButton.GetComponent<Button>().interactable = true;
            previousButton.transform.GetChild(0).GetComponent<Image>().color = activeColor;
            nextButton.GetComponent<Button>().interactable = false;
            nextButton.transform.GetChild(0).GetComponent<Image>().color = inactiveColor;
            skipText.text = "EXIT";
        }
    }
    
    public void ToNext()
    {
        if(page < totalpage){
            page = page + 1;
            Pages(page);
        }

    }
    public void ToPrevious()
    {
        if(page > 1)
        {
            page = page - 1;
            Pages(page);
        }

    }
    public void Skip(){
        tutorial.SetActive(false);
        menuHighLights[0].SetActive(false);
    }
}