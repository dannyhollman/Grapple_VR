using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerPrefsManager : MonoBehaviour
{
    public TMP_Text username;

    public void SaveUsername()
    {
        PlayerPrefs.SetString("Username", username.text);
    }
}
