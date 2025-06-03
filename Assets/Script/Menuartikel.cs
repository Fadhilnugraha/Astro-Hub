using UnityEngine;
using UnityEngine.SceneManagement;
public class Menuartikel : MonoBehaviour
{
    public void Buttonbacktomenu()
    {
        SceneManager.LoadScene("Main menu");
    }

    public void BToA1()
    {
        SceneManager.LoadScene("Artikel1");
    }

    public void BToA2()
    {
        SceneManager.LoadScene("Artikel2");
    }
}
