using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestEffectController : MonoBehaviour
{
    public GameObject smafSpawnEffect;
    void Awake()
    {
        smafSpawnEffect.SetActive(false);
    }
    public void ShowSmafSpawnEffect()
    {
        StartCoroutine(SmafSpawn());
    }
    IEnumerator SmafSpawn()
    {
        smafSpawnEffect.SetActive(true);
        yield return new WaitForSeconds(1.5f);
        smafSpawnEffect.SetActive(false);
    }
}
