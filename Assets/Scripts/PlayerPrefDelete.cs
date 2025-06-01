using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPrefDelete : MonoBehaviour
{
    public void DeletePref()
    {
        PlayerPrefs.DeleteAll();
    }
}
