[System.Serializable]
public class Article
{
    public string title;
    public string content;
    public string author;
    public string timestamp;

    public Article() {} // Required for Firebase

    public Article(string title, string content, string author)
    {
        this.title = title;
        this.content = content;
        this.author = author;
        this.timestamp = System.DateTime.UtcNow.ToString("yyyy-MM-ddTHH:mm:ssZ");
    }
}