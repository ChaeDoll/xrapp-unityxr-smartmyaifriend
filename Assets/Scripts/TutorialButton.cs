using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using Unity.VisualScripting;
using System.Net.NetworkInformation;

public class TutorialButton : MonoBehaviour
{
    public Transform eyePosition;
    public GameObject tutorial;
    public TMP_Text pageText;
    public TMP_Text content;
    public GameObject previousButton;
    public GameObject nextButton;

    public GameObject[] menuHighLights;

    public GameObject leftHand;
    public GameObject rightHand;
    public Animator leftAnimator;
    public Animator rightAnimator;
    //public GameObject rightPinchHand;
    //public GameObject rightPokeHand;

    public TMP_Text skipText;
    
    private string[] explanations={
        "화면을 '누르기(Poke)'하여 조작해보세요",
        "멀리 있는 화면은 \n'집기(Pinch)'하여 조작할 수 있어요", 
        "메뉴를 보려면 왼손을 뒤집으세요",
        "소환할 스마프를\n선택하는 버튼이에요",
        "소환된 스마프를\n호출하는 버튼이에요",
        "스마프에게 줄 간식을 소환한 뒤,\n간식을 집어 스마프에게 건네보세요",
        "스마프를 상자로 돌려 보내는 버튼이에요",
        "다양한 설정을 할 수 있는\n옵션 버튼이에요",
        "현재 튜토리얼을 다시 보는 버튼이에요",
        "이제 손목 메뉴의 상자호출 버튼을\n눌러 스마프 상자를 소환해보세요",
        "스마프의 상자가 나타났다면\n집기(Pinch)하여 열 수 있어요",
        "스마프가 소환되었나요?\n스마프는 똑똑한 AI 친구에요",
        "스마프를 집기(Pinch)해서\n함께 대화해보세요!"
    };
    private int page = 1;
    private int totalpage;
    
    Color activeColor = new Color(0 / 255f, 111 / 255f, 192 / 255f);
    Color inactiveColor = new Color(72 / 255f, 72 / 255f, 72 / 255f);

    void Start(){
        totalpage = explanations.Length;
        tutorial.SetActive(false);
        for (int i = 0; i < 6; i++)
        {
            menuHighLights[i].SetActive(false);
        }
    }
    public void CreateTutorial()
    {
        if (!tutorial.activeSelf)
        {
            skipText.text = "SKIP";
            page = 1;
            NotShowPinch();
            NotShowPoke();
            NotShowWrist();
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
        for (int i = 0; i < 6; i++)
        {
            menuHighLights[i].SetActive(false);
        }
        pageText.text = page.ToString()+" / "+totalpage.ToString();
        content.text = explanations[page-1];
        if (page == 1){ // 1st Page : 누르기 가이드
            previousButton.GetComponent<Button>().interactable = false;
            previousButton.transform.GetChild(0).GetComponent<Image>().color = inactiveColor;
            nextButton.GetComponent<Button>().interactable = true;
            nextButton.transform.GetChild(0).GetComponent<Image>().color = activeColor;
            NotShowPinch();
            StartCoroutine(ShowPoke());
            rightAnimator.SetTrigger("PokeTrigger");
        }
        else if(page == 2) // 2nd Page : 집기 가이드
        {
            previousButton.transform.GetChild(0).GetComponent<Image>().color = activeColor;
            previousButton.GetComponent<Button>().interactable = true;
            NotShowPoke();
            NotShowWrist();
            StartCoroutine(ShowPinch());
            rightAnimator.SetTrigger("PinchTrigger");
        }
        else if (page == 3) // 3rd Page : 손목 뒤집기
        {
            NotShowPinch();
            StartCoroutine(ShowWrist());
        }
        else if (page == 4 || page == 5 || page == 7 || page == 8) // 4~9th Page : 메뉴 설명들
        {
            NotShowWrist();
            NotShowPinch();
            menuHighLights[page-4].SetActive(true);
        }
        else if (page == 6) // 6th : 간식 집기 설명
        {
            menuHighLights[page - 4].SetActive(true);
            StartCoroutine(ShowPinch());
            rightAnimator.SetTrigger("GrabTrigger");
        }
        else if (page == 9)
        {
            menuHighLights[page - 4].SetActive(true);
        }
        else if(page == 10){
            menuHighLights[0].SetActive(true); 
        }
        else if (page == 12)
        {
            nextButton.GetComponent<Button>().interactable = true;
            nextButton.transform.GetChild(0).GetComponent<Image>().color = activeColor;
        }
        else if (page == 13)
        {
            skipText.text = "EXIT";
            nextButton.GetComponent<Button>().interactable = false;
            nextButton.transform.GetChild(0).GetComponent<Image>().color = inactiveColor;
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
        for (int i = 0; i < 6; i++)
        {
            menuHighLights[i].SetActive(false);
        }
    }

    private float currentAngleY ;
    public IEnumerator ShowWrist()
    {
        leftHand.SetActive(true);
/*        currentAngleY = -200f;
        leftHand.transform.rotation = Quaternion.Euler(90, currentAngleY, 0);
        while(true){
            while(currentAngleY<0f){
                currentAngleY += 4f;
                leftHand.transform.rotation = Quaternion.Euler(90, currentAngleY, 0);
                yield return new WaitForSeconds(0.03f);
            }
            yield return new WaitForSeconds(2f);
            currentAngleY = -200f;
            leftHand.transform.rotation = Quaternion.Euler(90, currentAngleY, 0);
        }*/
        yield return null;
        // 페이지를 넘어가지 않으면 계속 보이게
    } 

/*    private GameObject index;   
    private float pinchIndexZ;
    private Vector3 startRotation = new Vector3(347.362976f, 4.22357178f, 355.303131f);
    private Vector3 endRotation = new Vector3(305.137238f, 93.0645218f, 264.535217f);
    private float duration = 2.0f;*/
    public IEnumerator ShowPinch()
    {
        rightHand.SetActive(true);
        yield return null;
/*        pinchIndexZ = 60f;
        index = GameObject.Find("Dialogs/Tutorial Canvas/TutorialHands/RightPinchHand/HandRig/Wrist/Hand/IndexFinger/Index2");
        while (true)
        {
            while (pinchIndexZ < 158f)
            {
                pinchIndexZ += 1f;
                index.transform.rotation = Quaternion.Euler(0f, -12f, pinchIndexZ);
                yield return new WaitForSeconds(0.015f);
            }
            yield return new WaitForSeconds(1f);
            pinchIndexZ = 60f;
        }*/
    }
    // 
    // public float pinchCurrentAngleZ = 47f;
    //     public IEnumerator ShowPinch()
    //     {
    //          if (rightHand != null)
    //         {
    //             // HandRig 게임 오브젝트 찾기
    //             thumb = GameObject.Find("RightPinchHand/HandRig/Wrist/Hand/Thumb");
    //             thumb.transform.rotation = Quaternion.Euler(21, -72, pinchCurrentAngleZ);
    //             // pinchCurrentAngleZ = thumb.transform.localRotation.z;
    //             // print(pinchCurrentAngleZ);
    //             if(thumb != null){
    //                 while(pinchCurrentAngleZ>25f){
    //                     pinchCurrentAngleZ -= 2f;
    //                     thumb.transform.rotation = Quaternion.Euler(21, -72, pinchCurrentAngleZ);
    //                     // thumb.transform.localRotation = Quaternion.Euler(thumb.transform.localRotation.x, thumb.transform.localRotation.y, pinchCurrentAngleZ);
    //                     yield return new WaitForSeconds(0.02f); //0.44초?
    //                 }
    //             }

    /*             GameObject indexFinger = GameObject.Find("RightPinchHand/HandRig/Wrist/Hand/IndexFinger");
                public float pinchCurrentAngleX = indexFinger.Transform.rotation.x;
                public float pinchCurrentAngleY = indexFinger.Transform.rotation.y;
                if(indexFinger!=null){
                    while(pinchCurrentAngleX<18f){


                        pinchCurrentAngleX += 11f;
                        indexFinger.transform.rotation = Quaternion.Euler(pinchCurrentAngleX, pinchCurrentAngleY, indexFinger.Transform.rotation.z);
                        yield return new WaitForSeconds(0.01f);
                    }
    /*             GameObject indexFinger = GameObject.Find("RightPinchHand/HandRig/Wrist/Hand/IndexFinger");
                public float pinchCurrentAngleX = indexFinger.Transform.rotation.x;
                public float pinchCurrentAngleY = indexFinger.Transform.rotation.y;
                if(indexFinger!=null){
                    while(pinchCurrentAngleX<18f){
                        pinchCurrentAngleX += 11f;
                        indexFinger.transform.rotation = Quaternion.Euler(pinchCurrentAngleX, pinchCurrentAngleY, indexFinger.Transform.rotation.z);
                        yield return new WaitForSeconds(0.01f);
                    }
    // /*             GameObject indexFinger = GameObject.Find("RightPinchHand/HandRig/Wrist/Hand/IndexFinger");
    //             public float pinchCurrentAngleX = indexFinger.Transform.rotation.x;
    //             public float pinchCurrentAngleY = indexFinger.Transform.rotation.y;
    //             if(indexFinger!=null){
    //                 while(pinchCurrentAngleX<18f){
    //                     pinchCurrentAngleX += 11f;
    //                     indexFinger.transform.rotation = Quaternion.Euler(pinchCurrentAngleX, pinchCurrentAngleY, indexFinger.Transform.rotation.z);
    //                     yield return new WaitForSeconds(0.01f);
    //                 }

    //             } */
    //         }     
    //     } 
    public IEnumerator ShowPoke()
    {
        rightHand.SetActive(true);
        yield return null;
/*        float movement = 0f;
        rightPokeHand.SetActive(true);
        Vector3 currentPosition = rightPokeHand.transform.position;
        for (int i = 0; i< 5; i++)
        {
            while (movement <= 0.1f)
            {
                movement += 0.01f;
                rightPokeHand.transform.position = currentPosition + new Vector3(0f, -1 * movement, 0f);
                yield return new WaitForSeconds(0.05f);
            }
            while (movement >= 0f)
            {
                movement -= 0.01f;
                rightPokeHand.transform.position = currentPosition + new Vector3(0f, -1 * movement, 0f);
                yield return new WaitForSeconds(0.05f);
            }
        }*/
    }

    private void NotShowPoke()
    {
        rightHand.SetActive(false);
    }
    private void NotShowWrist()
    {
        leftHand.SetActive(false);
    }
    private void NotShowPinch()
    {
        rightHand.SetActive(false);   
    }


}


/*public IEnumerator ShowPoke()
{
    rightPokeHand.SetActive(true);
    *//*        Vector3 currentPosition = menuHighLights[0].transform.position;
            float minus = 0f;*//*
    float startX = rightPokeHand.transform.position.x;
    float startY = rightPokeHand.transform.position.y;
    float startZ = rightPokeHand.transform.position.z;
    float movement = 0;
    while (true)
    {
*//*            while (movement)
            {
                currentY -= 0.01f;
                rightPokeHand.transform.position = new Vector3(startX, currentY, startZ);
                yield return new WaitForSeconds(0.05f);
            }
            while (startY - currentY < 0f)
            {
                currentY += 0.01f;
                rightPokeHand.transform.position = new Vector3(startX, currentY, startZ);
                yield return new WaitForSeconds(0.05f);
            }*/
/*            if(minus < 0.09f){
                while(minus < 0.09f){
                    rightPokeHand.transform.position = new Vector3(currentPosition.x+0.03f , currentPosition.y - minus, currentPosition.z +0.3f);
                    minus += 0.01f;
                    yield return new WaitForSeconds(0.06f);
                }
                 yield return new WaitForSeconds(0.1f);
            }
            else{
                minus = 0f;
                while(minus < 0.09f){
                    rightPokeHand.transform.position = new Vector3(currentPosition.x+0.02f, currentPosition.y + minus, currentPosition.z+0.3f);
                    minus += 0.01f;
                    print(minus);
                    yield return new WaitForSeconds(0.06f);
                }
                yield return new WaitForSeconds(0.1f);
                minus = 0f;
            }*//*

}
}*/