using Firebase.Firestore;

[FirestoreData]
public class Article
{
    [FirestoreProperty]public string Judul{get;set;}
    [FirestoreProperty]public string IsiArtikel{get;set;}
    [FirestoreProperty]public string JenisArtikel{get;set;}
    [FirestoreProperty]public string AuthorId{get;set;}
    [FirestoreProperty]public string StatusArtikel{get;set;}
    [FirestoreProperty]public Timestamp createdAt{get;set;}

    public Article() {} // Required for Firebase

    public Article(string judul, string isi, string jenis, string authorId, string status)
    {
        Judul = judul;
        IsiArtikel = isi;
        JenisArtikel = jenis;
        AuthorId = authorId;
        StatusArtikel = status;
        createdAt = Timestamp.GetCurrentTimestamp();
    }
}