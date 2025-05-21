using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class Mainmenu : MonoBehaviour
{
    
    public void ButtonGoArtikel ()
    {
        SceneManager.LoadScene("Artikel");
    }

   
    public void ButtonGoWisata()
    {
        SceneManager.LoadScene("Astrowisata"); 
    }
}
