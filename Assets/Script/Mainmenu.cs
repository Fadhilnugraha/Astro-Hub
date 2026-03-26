using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using TMPro;
using Firebase.Auth;
using Firebase.Firestore;
using Firebase.Extensions;

public class Mainmenu : MonoBehaviour
{
    public RectTransform panel;        // Panel yang mau digerakkan
    public float slideSpeed = 500f;    // Kecepatan animasi
    public Vector2 hiddenPos;          // Posisi tertutup
    public Vector2 shownPos;           // Posisi terbuka

    public TextMeshProUGUI greetingText; //kalimat ucapan


    private bool isOpen = false;

    void Start()
    {
    panel.anchoredPosition = hiddenPos; // supaya awalnya tersembunyi

    ShowUserGreeting();
    }

    void ShowUserGreeting()               // fungsi ucapan
    {
        Debug.Log("user greeting dipanggil");
        var user = FirebaseAuth.DefaultInstance.CurrentUser;
        
        if (user == null)
        {
        greetingText.text = "Halo, Pengunjung!";
        return;
        }
        
        string uid = user.UserId;
        FirebaseFirestore db = FirebaseFirestore.DefaultInstance;
        
        db.Collection("User").Document(uid).GetSnapshotAsync()
        .ContinueWithOnMainThread(task =>
        {
            if (!task.IsCompleted || !task.Result.Exists)
            {
                greetingText.text = "Halo, User!";
                return;
            }

            var data = task.Result.ToDictionary();

            string nama = data.ContainsKey("nama") && data["nama"] != null
                ? data["nama"].ToString()
                : "User";

            greetingText.text = "Halo, " + nama + "!";
        });
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

        public void Tulisartikel()
    {
        SceneManager.LoadScene("Tulisartikel");
    }

    public void ButtonGoWisata()
    {
        SceneManager.LoadScene("Mainmenuwisata");
    }

    public void ButtonGoLogin()
    {
        SceneManager.LoadScene("Login screen");
    }
    public void Buttonuser()
    {
        SceneManager.LoadScene("User data");
    }
    public void ButtonQuit()
    {
        Application.Quit();
    }

}
