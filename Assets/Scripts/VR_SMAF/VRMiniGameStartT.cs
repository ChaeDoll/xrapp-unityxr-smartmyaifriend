using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VRMiniGameStartT : MonoBehaviour
{
    MinigameGunScriptT minigameGunScript;
    private void Start()
    {
        minigameGunScript = GameObject.Find("Minigame Manager").GetComponent<MinigameGunScriptT>();
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
