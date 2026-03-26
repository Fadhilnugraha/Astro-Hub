 using UnityEngine;
using TMPro;
using System;

public class MemoUI : MonoBehaviour
{
    public static MemoUI Instance;

    public GameObject panel;
    public TMP_InputField title;
    public TMP_InputField input;

    private DateTime currentDate;

    void Awake()
    {
        if (Instance==null){
        Instance = this;
        }
        else
        Destroy(gameObject);
        panel.SetActive(false);
    }

    public void Open(DateTime date)
    {
        currentDate = date;
        panel.SetActive(true);

        string key = GetKey(date);
        title.text = PlayerPrefs.GetString(key,"");
        input.text = PlayerPrefs.GetString(key, "");

        title.interactable=true;
        title.ActivateInputField();

        input.interactable=true;
        input.ActivateInputField();
    }

    public void Save()
    {
        PlayerPrefs.SetString(GetKey(currentDate), input.text);
        PlayerPrefs.SetString(GetKey(currentDate), title.text);
        PlayerPrefs.Save();
        panel.SetActive(false);
    }

    public void Close()
    {
        panel.SetActive(false);
    }

    string GetKey(DateTime date)
    {
        return "MEMO_" + date.ToString("yyyy_MM_dd");
    }
}
