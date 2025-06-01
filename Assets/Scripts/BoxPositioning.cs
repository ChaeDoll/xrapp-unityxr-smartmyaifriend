using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxPositioning : MonoBehaviour
{
    public Transform eyePosition;
    public GameObject boxShadowPrefab;
    public GameObject boxPrefab;
    public GameObject boxPrefabT;
    public GameObject dialog;
    private GameObject instantiatedShadowBox = null;
    private GameObject instantiatedBox = null;
    Vector3 boxposition;
    Vector3 boxRotation;
    Quaternion lookRotation;
    private bool dialogCheck = false;
    public Transform rightHandPoint;

    public GameObject smafSelectCanvas;

    private string spawnCharacter;

    private void Start()
    {
        dialog.SetActive(false);
        smafSelectCanvas.SetActive(false);
    }

    public void SpawnSelectCanvas()
    {
        if (instantiatedBox != null)
        {
            GameObject smafPrefab = GameObject.Find("SMAF_MyRoom(Clone)");
            if (smafPrefab == null) smafPrefab = GameObject.Find("SMAT_MyRoom(Clone)");
            if (smafPrefab != null) Destroy(smafPrefab);
            Destroy(instantiatedBox);
            dialog.SetActive(false);
            smafSelectCanvas.SetActive(false);
            dialogCheck = false;
        }
        else if (instantiatedShadowBox != null)
        {
            Destroy(instantiatedShadowBox);
            dialog.SetActive(false);
            smafSelectCanvas.SetActive(false);
            dialogCheck = false;
        }
        else
        {
            smafSelectCanvas.SetActive(true);
            smafSelectCanvas.transform.position = eyePosition.position + eyePosition.forward * 0.4f;
            smafSelectCanvas.transform.rotation = Quaternion.LookRotation(eyePosition.forward);
        }
    }
    public void CloseSelectCanvas()
    {
        smafSelectCanvas.SetActive(false);
    }

    public void CreateChestDialog(string characterName)
    {
        spawnCharacter = characterName;
        boxposition = eyePosition.position + eyePosition.forward * 0.8f;
        boxRotation = eyePosition.position - boxposition;
        boxRotation.y = 0;
        lookRotation = Quaternion.LookRotation(boxRotation);

        if (instantiatedShadowBox == null)
        {
            // 스마프 선택 창 꺼짐.
            smafSelectCanvas.SetActive(false);

            instantiatedShadowBox = Instantiate(boxShadowPrefab, boxposition, lookRotation);
            dialog.transform.position = eyePosition.position + eyePosition.forward * 0.5f;
            dialog.transform.rotation = Quaternion.LookRotation(eyePosition.forward);
            dialog.SetActive(true);
        }
    }
    void Update()
    {
        if (instantiatedShadowBox != null && dialogCheck)
        {
            boxposition = rightHandPoint.position;

            boxRotation = eyePosition.position - boxposition;
            boxRotation.y = 0;
            lookRotation = Quaternion.LookRotation(boxRotation);

            instantiatedShadowBox.transform.position = boxposition;
            instantiatedShadowBox.transform.rotation = lookRotation;
        }
    }

    public void SelectPosition()
    {
        if (instantiatedShadowBox != null && dialogCheck)
        {
            if (rightHandPoint != null)
            {
                boxposition = rightHandPoint.position; //여기를 손 끝 포인터 위치로
                boxRotation = eyePosition.position - boxposition;
                boxRotation.y = 0;
                lookRotation = Quaternion.LookRotation(boxRotation);

                if (spawnCharacter == "SMAF") instantiatedBox = Instantiate(boxPrefab, boxposition, lookRotation);
                else if (spawnCharacter == "SMAT") instantiatedBox = Instantiate(boxPrefabT, boxposition, lookRotation);

                Destroy(instantiatedShadowBox);
                dialogCheck = false;
            }
        }
    }
    public void OnPressDialog()
    {
        // 확인 버튼 누르면 Update가 실행되게
        dialog.SetActive(false);
        dialogCheck = true;
    }
}