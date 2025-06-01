using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnackCreator : MonoBehaviour
{
    public GameObject snackPrefab;
    GameObject instantiatedSnack;
    public Transform eyePosition;
    Vector3 snackPosition;
    bool isGrabbed = false;

    void Update()
    {

        if (instantiatedSnack != null)
        {
            if (!isGrabbed)
            {
                snackPosition = eyePosition.position + eyePosition.forward * 0.3f;
                instantiatedSnack.transform.position = snackPosition;
            }
        } 
    }
    public void SpawnSnack()
    {  
        if (instantiatedSnack == null)
        {
            snackPosition = eyePosition.position + eyePosition.forward * 0.3f;
            instantiatedSnack = Instantiate(snackPrefab, snackPosition, Quaternion.identity);
            instantiatedSnack.GetComponent<Rigidbody>().isKinematic = true;
        } 
        else
        {
            Destroy(instantiatedSnack);
            instantiatedSnack = null;
            isGrabbed = false;
        }
    }

    public void GrabSnack()
    {
        isGrabbed = true;
        instantiatedSnack.GetComponent<Rigidbody>().isKinematic = false;
    }
}
