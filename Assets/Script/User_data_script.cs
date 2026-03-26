using UnityEngine;
using TMPro;
using Firebase.Auth;
using Firebase.Firestore;
using Firebase.Extensions;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine.SceneManagement;

public class User_data_script : MonoBehaviour
{
     [Header("UI")]
    public TMP_InputField namaInput;
    public TMP_InputField emailInput;
    public TMP_InputField phoneInput;
    public TMP_InputField alamatInput;
    public TMP_Dropdown lokasiDropdown;

    private FirebaseAuth auth;
    private FirebaseFirestore db;
    private string uid;

    void Start()
    {
        auth = FirebaseAuth.DefaultInstance;
        db = FirebaseFirestore.DefaultInstance;

        if (auth.CurrentUser == null)
        {
            Debug.LogError("User belum login!");
            return;
        }

        uid = auth.CurrentUser.UserId;

        LoadUserData();
    }

    void LoadUserData()
    {
        // Email dari Auth (tidak diubah)
        emailInput.text = auth.CurrentUser.Email;
        emailInput.readOnly = true;

        db.Collection("User").Document(uid).GetSnapshotAsync()
        .ContinueWithOnMainThread(task =>
        {
            if (!task.IsCompleted || !task.Result.Exists)
            {
                Debug.LogWarning("Data user belum ada, akan dibuat saat save");
                return;
            }

            var data = task.Result.ToDictionary();

            namaInput.text   = Get(data, "nama");
            phoneInput.text  = Get(data, "phone");
            alamatInput.text = Get(data, "alamat");

            string lokasi = Get(data, "lokasi");
            setDropdownByText(lokasiDropdown,lokasi);
        });
    }

    string Get(Dictionary<string, object> data, string key)
    {
        return data.ContainsKey(key) && data[key] != null
            ? data[key].ToString()
            : "";
    }

    string GetselectedLokasi(){
        return lokasiDropdown.options[lokasiDropdown.value].text;
    }

    void setDropdownByText(TMP_Dropdown dropdown,string value)
    {
        if (string.IsNullOrEmpty(value))return;

        for (int i=0; i < dropdown.options.Count; i++)
        {
            if (dropdown.options[i].text == value)
            {
                dropdown.value=i;
                return;
            }
        }
    }

    // Dipanggil dari Button Save
    public void OnSaveProfile()
    {
        Dictionary<string, object> userData = new Dictionary<string, object>()
        {
            { "nama", namaInput.text },
            { "email", auth.CurrentUser.Email }, // tetap disimpan, tapi tidak dapat diubah
            { "phone", phoneInput.text },
            { "alamat", alamatInput.text },
            { "lokasi",GetselectedLokasi()},
            { "updatedAt", Timestamp.GetCurrentTimestamp() }
        };

        db.Collection("User").Document(uid)
        .SetAsync(userData, SetOptions.MergeAll)
        .ContinueWithOnMainThread(task =>
        {
            if (task.IsCompleted)
            {
                Debug.Log("Profile berhasil disimpan");
            }
            else
            {
                Debug.LogError("Gagal menyimpan profile: " + task.Exception);
            }
        });
    }

    public void buttonkembali()
    {
        SceneManager.LoadScene("Main menu");
    }

    public void onlogout()
    {
        FirebaseAuth.DefaultInstance.SignOut();
        Debug.Log("User telah logout");
        SceneManager.LoadScene("Main menu");
    }
}
