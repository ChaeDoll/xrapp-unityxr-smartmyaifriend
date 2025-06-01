using Oculus.Interaction.HandGrab;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunTrigCalculate : MonoBehaviour
{
    public HandGrabUseInteractable handGrabUseInteractable;
    public MinigameGunScript minigameGunScript;
    private bool isShoot = false;
    private void Update()
    {
        float triggerPoint = handGrabUseInteractable.UseStrengthDeadZone;
        float point = handGrabUseInteractable.UseProgress;
        
        if (point > triggerPoint)
        {
            if (!isShoot)
            {
                minigameGunScript.Shoot();
                isShoot = true;
            }
        }
        else
        {
            if (isShoot)
            {
                isShoot = false;
            }
        }
    }
}
