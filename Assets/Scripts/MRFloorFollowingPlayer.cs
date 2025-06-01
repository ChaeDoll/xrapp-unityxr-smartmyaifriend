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
            // ���� GameObject�� ��ġ�� ������Ʈ
            Vector3 newPosition = transform.position;
            newPosition.x = followTarget.position.x;  // x ���� ����� x ������ ����
            newPosition.z = followTarget.position.z;  // z ���� ����� z ������ ����

            transform.position = newPosition;  // ������Ʈ�� ��ġ�� ����
        }
    }
}
