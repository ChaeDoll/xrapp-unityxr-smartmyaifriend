using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class SMAFMovement : MonoBehaviour
{
    public UnityEngine.AI.NavMeshAgent agent;
    public float wanderRadius;
    public float wanderTimer;
    private float timer;
    private bool isCalled;  // 사용자가 호출했는지 여부를 추적하는 변수
    private bool isFixed;
    private Vector3 fixedPosition;
    public Transform userPosition; // 사용자의 Transform을 저장하는 변수
    public float smafDistance;
    public GameObject smafMenu;
    private GameObject instantiatedSnack;

    private bool isGameStarted;
    private Vector3 smafGamePosition;

    void Start()
    {
        smafMenu.SetActive(false);
        timer = wanderTimer;
        isCalled = false;
        isFixed = false;
        GameObject userObject = GameObject.Find("Interaction Camera Rig/TrackingSpace/CenterEyeAnchor");
        if (userObject != null)
        {
            userPosition = userObject.transform;
        }

        isGameStarted = false;
        smafGamePosition = GameObject.Find("Minigame Smaf Position").transform.position;
    }
    void Update()
    {
        instantiatedSnack = GameObject.Find("SMAF_Snack(Clone)");
        if (instantiatedSnack == null)
        {
            instantiatedSnack = GameObject.Find("SMAF_Snack_VR(Clone)");
        }
        if (instantiatedSnack != null)
        {
            agent.SetDestination(instantiatedSnack.transform.position);
        } else
        {
            if (isGameStarted)
            { 
                if (smafGamePosition != null)
                {
                    agent.SetDestination(smafGamePosition);
                    FaceUser();
                }
            }
            else if (isCalled)
            {
                if (isFixed)
                {
                    agent.SetDestination(fixedPosition);
                } else
                {
                    // 사용자가 호출했을 때의 행동
                    Vector3 targetPosition = userPosition.position + userPosition.forward * smafDistance;
                    agent.SetDestination(targetPosition);
                }
                FaceUser();
            }
            else
            {
                timer += Time.deltaTime;

                if (timer >= wanderTimer)
                {
                    Vector3 newPos = RandomNavSphere(transform.position, wanderRadius, -1);
                    agent.SetDestination(newPos);
                    timer = 0;
                }
            }
        }
    }
    public static Vector3 RandomNavSphere(Vector3 origin, float dist, int layermask)
    {
        Vector3 randDirection = Random.insideUnitSphere * dist;
        randDirection += origin;
        UnityEngine.AI.NavMeshHit navHit;
        UnityEngine.AI.NavMesh.SamplePosition(randDirection, out navHit, dist, layermask);
        return navHit.position;
    }

    // 사용자가 호출할 때 실행되는 메서드
    public AudioSource smafAudio;
    public void CallSMAF()
    {
        if (userPosition != null)
        {
            isCalled = true;
            if (!isFixed)
            {
                smafAudio.Play();
                Vector3 targetPosition = userPosition.position + userPosition.forward * smafDistance;
                agent.SetDestination(targetPosition);
            }
            smafMenu.SetActive(true);
        }
    }

    // 사용자가 호출을 취소할 때 실행되는 메서드
    public void CancelCallSMAF()
    {
        isCalled = false;
        isFixed = false;
        timer = wanderTimer;  // 즉시 랜덤 움직임을 시작하도록 타이머 초기화
        smafMenu.SetActive(false);
    }
    public void ToggleFixSMAFPosition()
    {
        if (isCalled)
        {
            isFixed = !isFixed;
            if (isFixed)
            {
                fixedPosition = transform.position; // 현재 위치를 고정된 위치로 설정
            }
        }
    }

    void FaceUser()
    {
        if (userPosition != null)
        {
            Vector3 direction = (userPosition.position - transform.position).normalized;
            Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f); // 부드럽게 회전하도록 설정
        }
    }
    
    // 미니게임 스마프
    public void MinigameStart()
    {
        isGameStarted = true;
        smafMenu.SetActive(false);
    }
    public void MinigameEnd()
    {
        isGameStarted = false;
    }
}
