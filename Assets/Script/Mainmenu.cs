using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class Mainmenu : MonoBehaviour
{
    public RectTransform panel;        // Panel yang mau digerakkan
    public float slideSpeed = 500f;    // Kecepatan animasi
    public Vector2 hiddenPos;          // Posisi tertutup
    public Vector2 shownPos;           // Posisi terbuka


    private bool isOpen = false;

    void Start()
    {
    panel.anchoredPosition = hiddenPos; // supaya awalnya tersembunyi
    }
    public void TogglePanel()
    {
        isOpen = !isOpen;
        StopAllCoroutines();
        StartCoroutine(Slide(isOpen ? shownPos : hiddenPos));
    }

    private System.Collections.IEnumerator Slide(Vector2 targetPos)
    {
        while (Vector2.Distance(panel.anchoredPosition, targetPos) > 0.1f)
        {
            panel.anchoredPosition = Vector2.Lerp(panel.anchoredPosition, targetPos, Time.deltaTime * 10);
            yield return null;
        }
        panel.anchoredPosition = targetPos;
    }

    public void ButtonGoArtikel()
    {
        SceneManager.LoadScene("Artikel");
    }

    public void ButtonGoWisata()
    {
        SceneManager.LoadScene("Mainmenuwisata");
    }

    public void ButtonQuit()
    {
        Application.Quit();
    }
}
