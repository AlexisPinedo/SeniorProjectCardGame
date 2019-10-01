using System.Collections;
using System.Collections.Generic;
using Firebase;
using Firebase.Auth;
using Firebase.Unity.Editor;
using UnityEngine;

public class DatabaseManager : MonoBehaviour
{
    public static DatabaseManager sharedInstance = null;

    private void Awake()
    {
        if (sharedInstance == null)
            sharedInstance = this;
        else if(sharedInstance != this)
            Destroy(gameObject);

        DontDestroyOnLoad(gameObject);

        FirebaseApp.DefaultInstance.SetEditorDatabaseUrl("https://raising-spirits.firebaseio.com/");
    }

    public void CreateNewPlayer()
    {

    }

    public void CheckIfPlayerExists()
    {

    }

    public void CreatePlayerRecords(PlayerRecord playerRecord, string uid)
    {
        string jsonString = JsonUtility.ToJson(playerRecord);
        Router.PlayerRecord(uid).SetRawJsonValueAsync(jsonString);
    }

    public void UpdatePlayerRecords(int wins, int losses)
    {
        FirebaseUser user = AuthManager.sharedInstance.GetCurrentUser();

        PlayerRecord playerRecord = new PlayerRecord(wins, losses);

        Dictionary<string, object> dict = playerRecord.ToDictionary();

        Dictionary<string, object> childUpdates = new Dictionary<string, object>();
        childUpdates["/player_record/" + user.UserId + "/"] = dict;

        Router.Base().UpdateChildrenAsync(childUpdates);
    }
}
