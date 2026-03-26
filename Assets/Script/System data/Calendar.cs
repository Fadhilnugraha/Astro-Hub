using UnityEngine;
using System;
using UnityEngine.UI;
using TMPro;

public class Calendar : MonoBehaviour
{
    [Serializable]
    public class DayColumn
    {
        public Transform column; // Senin, Selasa, dst
    }

    public DayColumn[] daysOfWeek; // WAJIB size = 7 (Senin–Minggu)
    public TextMeshProUGUI monthAndYear;

    private Image[,] cells = new Image[7, 6];
    private TextMeshProUGUI[,] texts = new TextMeshProUGUI[7, 6];

    private DateTime currDate;

    void Start()
    {
        CacheCells();
        UpdateCalendar(DateTime.Now.Year, DateTime.Now.Month);
    }

    // =============================
    // CACHE CELL (AMAN)
    // =============================
    void CacheCells()
    {
        for (int d = 0; d < 7; d++)
        {
            Transform column = daysOfWeek[d].column;

            if (column.childCount < 6)
            {
                Debug.LogError($"Kolom {column.name} harus punya 6 child!");
                return;
            }

            for (int w = 0; w < 6; w++)
            {
                Transform cell = column.GetChild(w);
                cells[d, w] = cell.GetComponent<Image>();
                texts[d, w] = cell.GetComponentInChildren<TextMeshProUGUI>();
            }
        }
    }

    // =============================
    // UPDATE CALENDAR
    // =============================
    void UpdateCalendar(int year, int month)
    {
        currDate = new DateTime(year, month, 1);
        monthAndYear.text = currDate.ToString("MMMM yyyy");

        int startDay = ((int)currDate.DayOfWeek + 6) % 7; // Senin = 0
        int totalDays = DateTime.DaysInMonth(year, month);

        // reset semua cell
        for (int d = 0; d < 7; d++)
        {
            for (int w = 0; w < 6; w++)
            {
                texts[d, w].text = "";
                cells[d, w].color = Color.grey;
            }
        }

        // isi tanggal
        for (int day = 1; day <= totalDays; day++)
        {
            int index = startDay + (day - 1);
            int col = index % 7;
            int row = index / 7;

            texts[col, row].text = day.ToString();
            cells[col, row].color = Color.white;

            CalendarCell cell = cells[col, row].GetComponent<CalendarCell>();
                if (cell != null)
                {
                    DateTime date = new DateTime(year,month, day);
                    cell.Setup(date,this);
                }

            // highlight hari ini
            if (DateTime.Now.Year == year &&
                DateTime.Now.Month == month &&
                DateTime.Now.Day == day)
            {
                cells[col, row].color = Color.green;
            }
        }
    }

    // =============================
    // SWITCH MONTH
    // =============================
    public void SwitchMonth(int dir)
    {
        currDate = currDate.AddMonths(dir > 0 ? 1 : -1);
        UpdateCalendar(currDate.Year, currDate.Month);
    }
}
