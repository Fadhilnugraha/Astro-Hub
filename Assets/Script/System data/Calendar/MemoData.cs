using Firebase.Firestore;

[FirestoreData]
public class MemoData
{
    [FirestoreProperty]
    public string Title { get; set; }

    [FirestoreProperty]
    public string Content { get; set; }

    [FirestoreProperty]
    public string Date { get; set; }

    [FirestoreProperty]
    public string AuthorUID { get; set; }

    [FirestoreProperty]
    public string AuthorName { get; set; }

    [FirestoreProperty]
    public Timestamp CreatedAt { get; set; }
}