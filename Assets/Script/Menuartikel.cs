using UnityEngine;
using UnityEngine.SceneManagement;
public class Menuartikel : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
   public void Buttonbacktomenu ()
    {
        SceneManager.LoadScene("Main menu");
    }

}
