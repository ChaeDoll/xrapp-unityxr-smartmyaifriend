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
    // ���� �� Disable
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

    // ���� �ʱ� ����
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

    // �ٽ� �ϱ�
    public void RetryGame()
    {
        if (gunPositionMemory != Vector3.zero)
        {
            gunPosition.position = gunPositionMemory;
        }
        InitGameState();
    }

    // ���� ���� �̺�Ʈ
    public void FinishGame()
    {
        gunLazerController.GetComponent<GunLazer>().ClearBulletHole();
        finishCanvas.SetActive(true);
        finishSound.Play();
    }

    // ���� ������
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

    // �� Ʃ�丮��
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

    // ź�� �Һ�
    void UseBullet()
    {
        bulletCount--;
        bullets[4-bulletCount].sprite = bulletOff;
        if (bulletCount == 0)
        {
           FinishGame();
        }
    }

    // �� �Ҹ�
    void GunSound()
    {
        shootSound.Play();
    }

    // �� ����
    void GunSmoke()
    {
        gunEffect.Play();
    }

    // �Ѿ� �ڱ�
    void GunBulletHole()
    {
        if (bulletCount > 0)
        {
            gunLazerController.GetComponent<GunLazer>().CreateBulletHole();
        }
    }

    // Ÿ�� ����
    public void HitTarget(int targetScore)
    {
        System.Random rand = new System.Random();
        string[] smafHitMessage = { "�ƾ�!", "������ ȭ���� �־�?", "���� �� ����" };
        //������ ���� F ������ ���׼�
        switch (targetScore)
        {
            case 10:chatController.UpdateMessage("�� ���! �� ��¥ ����ϱ���?"); break;
            case 9: chatController.UpdateMessage("����! �� ����ִµ�?"); break;
            case 8: 
            case 7: chatController.UpdateMessage("�� ��µ�~!"); break;
            case 6:
            case 5: chatController.UpdateMessage("�������� �� �Ѵ�!"); break;
            case 4: 
            case 3: chatController.UpdateMessage("�ƽ���..."); break;
            case 0: chatController.UpdateMessage("��.. �� �� �þ�"); break;
            default: chatController.UpdateMessage(smafHitMessage[rand.Next(0, 3)]); break;
        }

        score += targetScore;
        scoreText.text = score.ToString();
    }

    // ��� �Լ�
    public void Shoot()
    {
        GunBulletHole();
        UseBullet();
        GunSound();
        GunSmoke();
    }
}
