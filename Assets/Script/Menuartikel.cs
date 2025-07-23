using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menuartikel : MonoBehaviour
{
    
    public void Buttonbacktomenu()
    {
        SceneManager.LoadScene("Main menu");
    }

    public void Buttontotulis()
    {
        SceneManager.LoadScene("Tulisartikel");
    }

}
