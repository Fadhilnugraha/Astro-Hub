using UnityEngine;

public class Calendartogle : MonoBehaviour
{
    public GameObject calendar;
    public GameObject Panel_event;

    public void togglecal()
    {
        if (calendar != null)
        {
            calendar.SetActive(!calendar.activeSelf);
        }
        if (Panel_event != null)
        {
            Panel_event.SetActive(!Panel_event.activeSelf);
        }
        
    }

    public void tutupkalender()
    {
        if (calendar != null)
        {
            calendar.SetActive(!calendar.activeSelf);
            
        }
         if (Panel_event != null)
        {
            Panel_event.SetActive(!Panel_event.activeSelf);
        }
    }
}
