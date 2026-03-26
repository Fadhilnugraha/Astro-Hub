using TimeAndDate.Services;
using UnityEngine;

public class TstDLL : MonoBehaviour
{
    void Start()
    {
        var s = new AstronomyServices();
        Debug.Log(s.GetType().FullName);
    }
}
