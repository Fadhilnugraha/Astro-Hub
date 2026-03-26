using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class CalendarCell : MonoBehaviour
{
    public UnityEngine.UI.Button button;


    private DateTime date;
    private Calendar calendar;

    public void Setup(DateTime date, Calendar calendar)
    {
        this.date = date;
        this.calendar = calendar;

        if (button == null)
        {
            button = GetComponent<UnityEngine.UI.Button>();
        }

        button.interactable = true;
        button.onClick.RemoveAllListeners();
        button.onClick.AddListener(OnClick);
    }

    public void Disable()
    {
        button.interactable = false;
        button.onClick.RemoveAllListeners();
    }

    void OnClick()
    {
        MemoUI.Instance.Open(date);
        Debug.Log("memo muncul");
    }
}
