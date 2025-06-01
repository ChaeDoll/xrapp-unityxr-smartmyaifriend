using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MinigameGunScriptT : MonoBehaviour
{
    public GameObject GameObjects;
    public AudioSource shootSound;
    public GameObject gunLazerController;
    // 시작 시 Disable
    void Start()
    {
        tutorialCanvas.SetActive(false);
        GameObjects.SetActive(false);
    }

    public Sprite bulletOn;
    public Sprite bulletOff;
    public Image[] bullets;
    public TMP_Text scoreText;
    public Transform gunPosition;
    public GameObject finishCanvas;
    public AudioSource finishSound;
    public GameObject tutorialCanvas;
    public ParticleSystem gunEffect;
    public AudioSource gameStartSound;

    public GameObject goMyRoomObject;

    private Vector3 gunPositionMemory = Vector3.zero;
    private int bulletCount;
    private int score;
    private SMAFMovement smafMovement;
    private SMAFChatControllerT chatController;

    // 게임 초기 설정
    public void InitGameState()
    {
        smafMovement = GameObject.Find("SMAT_VrRoom(Clone)").GetComponent<SMAFMovement>();
        chatController = GameObject.Find("SMAT_VrRoom(Clone)").GetComponent<SMAFChatControllerT>();
        if (smafMovement != null )
        {
            smafMovement.MinigameStart();
        }
        if (gunPositionMemory == Vector3.zero)
        {
            gunPositionMemory = gunPosition.position;
        }
        foreach (var bullet in bullets)
        {
            bullet.sprite = bulletOn;
        }
        bulletCount = 5;
        score = 0;
        scoreText.text = score.ToString();
        finishCanvas.SetActive(false);
        GameObjects.SetActive(true);
        gameStartSound.Play();

        goMyRoomObject.SetActive(false);
    }

    // 다시 하기
    public void RetryGame()
    {
        if (gunPositionMemory != Vector3.zero)
        {
            gunPosition.position = gunPositionMemory;
        }
        InitGameState();
    }

    // 게임 종료 이벤트
    public void FinishGame()
    {
        gunLazerController.GetComponent<GunLazerT>().ClearBulletHole();
        finishCanvas.SetActive(true);
        finishSound.Play();
    }

    // 게임 나가기
    public void ExitGame()
    {
        gunLazerController.GetComponent<GunLazerT>().ClearBulletHole();
        if (smafMovement != null)
        {
            smafMovement.MinigameEnd();
        }
        if (gunPositionMemory != Vector3.zero)
        {
            gunPosition.position = gunPositionMemory;
        }
        finishCanvas.SetActive(false);
        GameObjects.SetActive(false);

        goMyRoomObject.SetActive(true);
    }

    // 총 튜토리얼
    IEnumerator ShowTutorial()
    {
        tutorialCanvas.SetActive(true);
        yield return new WaitForSeconds(5f);
        tutorialCanvas.SetActive(false);
    }
    public void HandleGrabGun()
    {
        StartCoroutine(ShowTutorial());
    }
    public void HandleUnGrabGun()
    {
        StopAllCoroutines();
        tutorialCanvas.SetActive(false);
    }

    // 탄알 소비
    void UseBullet()
    {
        bulletCount--;
        bullets[4-bulletCount].sprite = bulletOff;
        if (bulletCount == 0)
        {
           FinishGame();
        }
    }

    // 총 소리
    void GunSound()
    {
        shootSound.Play();
    }

    // 총 연기
    void GunSmoke()
    {
        gunEffect.Play();
    }

    // 총알 자국
    void GunBulletHole()
    {
        if (bulletCount > 0)
        {
            gunLazerController.GetComponent<GunLazerT>().CreateBulletHole();
        }
    }

    // 타겟 명중
    public void HitTarget(int targetScore)
    {
        System.Random rand = new System.Random();
        string[] smafHitMessage = { "눈 없냐?", "죽을래?", "?" };
        //점수에 따른 T 스마프 리액션
        switch (targetScore)
        {
            case 10: chatController.UpdateMessage("미친 개쩌네?"); break;
            case 9: chatController.UpdateMessage("미친"); break;
            case 8:
            case 7: chatController.UpdateMessage("그 정도는 나도 한다"); break;
            case 6:
            case 5: chatController.UpdateMessage("풉"); break;
            case 4:
            case 3: chatController.UpdateMessage("에반데"); break;
            case 0: chatController.UpdateMessage("와.. 실화냐?"); break;
            default: chatController.UpdateMessage(smafHitMessage[rand.Next(0, 3)]); break;
        }

        score += targetScore;
        scoreText.text = score.ToString();
    }

    // 사격 함수
    public void Shoot()
    {
        GunBulletHole();
        UseBullet();
        GunSound();
        GunSmoke();
    }
}
