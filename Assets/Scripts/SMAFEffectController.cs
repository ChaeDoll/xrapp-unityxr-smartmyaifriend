using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SMAFEffectController : MonoBehaviour
{
    public GameObject spawnEffect;
    public GameObject recallEffect;
    void Awake()
    {
        spawnEffect.SetActive(false);
        recallEffect.SetActive(false);
    }
    void Start()
    {
        StartCoroutine(PreviousSpawn());
    }
    IEnumerator PreviousSpawn()
    {
        spawnEffect.SetActive(true);
        yield return new WaitForSeconds(5f);
        spawnEffect.SetActive(false);
    }

    public IEnumerator RecallSmaf()
    {
        recallEffect.SetActive(true);
        yield return new WaitForSeconds(2f);
        recallEffect.SetActive(false);
    }
}
