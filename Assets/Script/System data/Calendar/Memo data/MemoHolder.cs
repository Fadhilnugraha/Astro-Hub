using UnityEngine;

public class MemoHolder : MonoBehaviour
{
    public static MemoHolder Instance;

    public string Title;
    public string Content;
    public string Date;
    public string Author;

    void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}