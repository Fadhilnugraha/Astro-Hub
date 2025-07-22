using System.Threading.Tasks;
using UnityEngine;

public class TestnET : MonoBehaviour
{
    async void Start()
    {
        Debug.Log("Mulai test async...");
        await Task.Delay(1000);
        Debug.Log("Selesai!");
    }
}
