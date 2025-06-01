using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VRMiniGameStart : MonoBehaviour
{
    MinigameGunScript minigameGunScript;
    private void Start()
    {
        minigameGunScript = GameObject.Find("Minigame Manager").GetComponent<MinigameGunScript>();
    }
    public void StartMinigame()
    {
        if (minigameGunScript != null)
        {
            minigameGunScript.ExitGame();
            minigameGunScript.InitGameState();
        } else
        {
        }
    }
}
