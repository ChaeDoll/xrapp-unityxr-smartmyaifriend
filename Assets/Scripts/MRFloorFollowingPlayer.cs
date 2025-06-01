using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class MRFloorFollowingPlayer : MonoBehaviour
{
    public Transform followTarget;

    void Update()
    {
        if (followTarget != null)
        {
            // 현재 GameObject의 위치를 업데이트
            Vector3 newPosition = transform.position;
            newPosition.x = followTarget.position.x;  // x 값을 대상의 x 값으로 설정
            newPosition.z = followTarget.position.z;  // z 값을 대상의 z 값으로 설정

            transform.position = newPosition;  // 업데이트된 위치를 적용
        }
    }
}
