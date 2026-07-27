using UnityEngine;
using UnityEngine.SceneManagement;

public class Wisatamainmenu : MonoBehaviour
{
    public void Buttonbacktomenu ()
    {
        SceneManager.LoadScene("Main menu");
    }
    public void gowisata()
    {
        SceneManager.LoadScene("Artikel1");
    }
    public void gowisata2()
    {
        SceneManager.LoadScene("Artikel2");
    }

        public void gowisata3()
    {
        SceneManager.LoadScene("Artikel3");
    }

        public void gowisata4()
    {
        SceneManager.LoadScene("Artikel4");
    }
}
