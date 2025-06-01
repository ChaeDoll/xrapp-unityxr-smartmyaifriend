using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnackGrab : MonoBehaviour
{
    SnackCreator snackController;
    void Start()
    {
        snackController = GameObject.FindWithTag("SpawnSnack").GetComponent<SnackCreator>();
    }

    public void OnGrabSnack()
    {
        if (snackController != null)
        {
            snackController.GrabSnack();
        }
    }
}
