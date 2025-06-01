using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestAnimation : MonoBehaviour
{
    public GameObject smafPrefab;
    private GameObject instantiatedSmaf = null;
    private SMAFChatController smafChatController = null;
    private SMAFMovement smafMovementController = null;
    public Animator animator;
    bool hovered = false;
    float speed = 1;
    bool open = false;

    public ChestEffectController chestEffectController;
    public GameObject postItPrefab;
    void Start()
    {
        postItPrefab.SetActive(false);
    }
    public void HoverChest()
    {
        hovered = true;
        animator.SetBool("Hovered", hovered);
    }
    public void UnHoveredChest()
    {
        hovered = false;
        animator.SetBool("Hovered", hovered);
    }
    public void OpenChest()
    {
        hovered = false;
        animator.SetBool("Hovered", hovered);
        speed = 1;
        animator.SetFloat("speed", speed);
        open = true;
        animator.SetBool("Open", open);
        if (instantiatedSmaf == null)
        {
            chestEffectController.ShowSmafSpawnEffect();
            instantiatedSmaf = Instantiate(smafPrefab, transform.position, transform.rotation);
            instantiatedSmaf.SetActive(false);
            smafChatController = instantiatedSmaf.GetComponent<SMAFChatController>();
            smafMovementController = instantiatedSmaf.GetComponent<SMAFMovement>();
            StartCoroutine(SpawnSmafAfter3Seconds());
        }
    }
    IEnumerator SpawnSmafAfter3Seconds()
    {
        yield return new WaitForSeconds(1.5f);
        instantiatedSmaf.SetActive(true);
        smafChatController.TmpChat();
        StartCoroutine(SpawnAndWait(2f));
        postItPrefab.SetActive(true);
    }
    IEnumerator SpawnAndWait(float time)
    {
        smafMovementController.enabled = false;
        yield return new WaitForSeconds(time);
        smafMovementController.enabled = true;
    }

    public void CloseChest()
    {
        if (instantiatedSmaf != null)
        {
            Destroy(instantiatedSmaf);
            postItPrefab.SetActive(false);
        }
        speed = -1;
        animator.SetFloat("speed", speed);
        animator.Play("Chest Open", 0, 1f); // 애니메이션의 끝에서 시작하도록 설정
        open = false;
        animator.SetBool("Open", open);
    }
}
