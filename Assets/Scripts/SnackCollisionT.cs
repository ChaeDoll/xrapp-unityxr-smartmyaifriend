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
            //여기에 소리랑 텍스트 출력 놔둬도 좋을 듯 (EatSnack 메소드)
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
