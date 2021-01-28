using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSelect : MonoBehaviour
{
    public GameObject[] characters;
    public int id;

    private void Start()
    {
        id = PlayerPrefs.GetInt("charID", 0);
        characters[id].SetActive(true);
    }

    public void Next()
    {
        if (id < characters.Length - 1)
        {
            characters[id++].SetActive(false);
            characters[id].SetActive(true);
            PlayerPrefs.SetInt("charID", id);
        }
    }

    public void Prev()
    {
        if (id > 0)
        {
            characters[id--].SetActive(false);
            characters[id].SetActive(true);
            PlayerPrefs.SetInt("charID", id);
        }
    }
}
