using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeftHandUIScript : MonoBehaviour
{
    public Transform eyePosition;
    public CanvasGroup uiElement;
    public float activationAngle = 60f;  // 활성화 각도 (디폴트 60도)
    public GameObject leftHandUIObject;
    void Update()
    {
        Vector3 directionToPlayer = eyePosition.position- transform.position;
        float angle = Vector3.Angle(transform.up, directionToPlayer);
        if (angle < activationAngle)
        {
            leftHandUIObject.SetActive(true);
        }
        else
        {
            leftHandUIObject.SetActive(false);
        }
    }
}
