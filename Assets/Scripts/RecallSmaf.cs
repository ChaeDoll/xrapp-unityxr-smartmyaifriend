using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecallSmaf : MonoBehaviour
{
    GameObject instantiatedChest;
    ChestAnimation chestController;
    public SMAFEffectController smafEffectController;
    public SMAFChatController smafChatController;
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
        instantiatedChest = GameObject.Find("SMAF Chest(Clone)");
        chestController = instantiatedChest.GetComponent<ChestAnimation>();
        chestController.CloseChest();
    }
}
