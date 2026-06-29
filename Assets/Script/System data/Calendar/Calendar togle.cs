using UnityEngine;

public class Calendartogle : MonoBehaviour
{
    public GameObject calendar;

    public void togglecal()
    {
        if (calendar != null)
        {
            calendar.SetActive(!calendar.activeSelf);
        }
        
    }

    public void tutupkalender()
    {
        if (calendar != null)
        {
            calendar.SetActive(!calendar.activeSelf);
        }
    }
}
