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
        //�޴� â �ݱ�
        smafMenu.SetActive(false);
        //��ȯ �޼��� ���
        smafChatController.RecallChat();
        //��ȯ ����Ʈ 2�� �� �����ֱ�
        StartCoroutine(smafEffectController.RecallSmaf());
        yield return new WaitForSeconds(2f);
        //���� ������ ������ ������ ���ֱ�
        instantiatedChest = GameObject.Find("SMAF Chest T(Clone)");
        chestController = instantiatedChest.GetComponent<ChestAnimationT>();
        chestController.CloseChest();
    }
}
