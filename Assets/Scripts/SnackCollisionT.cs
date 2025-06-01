using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnackCollisionT : MonoBehaviour
{
    public SMAFChatControllerT smafChatController;
    public AudioSource snackSound;
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Snack"))
        {
            //���⿡ �Ҹ��� �ؽ�Ʈ ��� ���ֵ� ���� �� (EatSnack �޼ҵ�)
            EatSnack();
            Destroy(collision.gameObject);
        }
    }
    void EatSnack()
    {
        snackSound.Play();
        smafChatController.EatSnackChat();
    }
}
