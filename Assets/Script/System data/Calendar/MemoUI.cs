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
        else{
        Destroy(gameObject);
        }
        panel.SetActive(false);
    }

    public void Open(DateTime date)
    {
        currentDate = date;
        panel.SetActive(true);

        //string key = GetKey(date);
        title.text = PlayerPrefs.GetString(GetTitleKey(date),"");
        input.text = PlayerPrefs.GetString(GetContentKey(date), "");
    

        title.interactable=true;
        title.ActivateInputField();

        input.interactable=true;
        input.ActivateInputField();
        Debug.Log(PlayerPrefs.GetString(GetContentKey(date)));
        {
            
        }
    }

    public void Save()
    {
        PlayerPrefs.SetString(GetTitleKey(currentDate), input.text);
        PlayerPrefs.SetString(GetContentKey(currentDate), title.text);
        PlayerPrefs.Save();
        panel.SetActive(false);
        Debug.Log("berhasil melakukan save");
        Debug.Log(input.text);
    }

    public void Close()
    {
        panel.SetActive(false);
    }

    //string GetKey(DateTime date)
    //{
      //  return "MEMO_" + date.ToString("yyyy_MM_dd");
    //}

    string GetTitleKey(DateTime date)
    {
        return "Memo_title"+ date.ToString("yyyy_MM_dd");
    }

    string GetContentKey(DateTime date)
    {
        return "Memo_content"+ date.ToString("yyyy_MM_dd");
    }

    //To Do : tambahin keterhubungan dengan Timeanddate
}
