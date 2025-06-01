using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MinigameGunScript : MonoBehaviour
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
    private SMAFChatController chatController;

    // 게임 초기 설정
    public void InitGameState()
    {
        smafMovement = GameObject.Find("SMAF_VrRoom(Clone)").GetComponent<SMAFMovement>();
        chatController = GameObject.Find("SMAF_VrRoom(Clone)").GetComponent<SMAFChatController>();
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
        gunLazerController.GetComponent<GunLazer>().ClearBulletHole();
        finishCanvas.SetActive(true);
        finishSound.Play();
    }

    // 게임 나가기
    public void ExitGame()
    {
        gunLazerController.GetComponent<GunLazer>().ClearBulletHole();
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
            gunLazerController.GetComponent<GunLazer>().CreateBulletHole();
        }
    }

    // 타겟 명중
    public void HitTarget(int targetScore)
    {
        System.Random rand = new System.Random();
        string[] smafHitMessage = { "아야!", "나한테 화난거 있어?", "말로 해 말로" };
        //점수에 따른 F 스마프 리액션
        switch (targetScore)
        {
            case 10:chatController.UpdateMessage("헐 대박! 너 진짜 대단하구나?"); break;
            case 9: chatController.UpdateMessage("오오! 너 재능있는데?"); break;
            case 8: 
            case 7: chatController.UpdateMessage("잘 쏘는데~!"); break;
            case 6:
            case 5: chatController.UpdateMessage("생각보다 잘 한다!"); break;
            case 4: 
            case 3: chatController.UpdateMessage("아쉽다..."); break;
            case 0: chatController.UpdateMessage("나.. 난 못 봤어"); break;
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
