using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecallSmafT : MonoBehaviour
{
    GameObject instantiatedChest;
    ChestAnimationT chestController;
    public SMAFEffectController smafEffectController;
    public SMAFChatControllerT smafChatController;
    public GameObject smafMenu;
    public void TurnBackSmafHome()
    {
        StartCoroutine(RecallSequence());
    }
    IEnumerator RecallSequence()
    {
        //메뉴 창 닫기
        smafMenu.SetActive(false);
        //귀환 메세지 출력
        smafChatController.RecallChat();
        //귀환 이펙트 2초 간 보여주기
        StartCoroutine(smafEffectController.RecallSmaf());
        yield return new WaitForSeconds(2f);
        //상자 닫으며 스마프 있으면 없애기
        instantiatedChest = GameObject.Find("SMAF Chest T(Clone)");
        chestController = instantiatedChest.GetComponent<ChestAnimationT>();
        chestController.CloseChest();
    }
}
