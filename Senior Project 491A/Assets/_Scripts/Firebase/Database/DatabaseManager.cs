using System;
using System.Collections;
using System.Collections.Generic;
using Firebase;
using Firebase.Auth;
using Firebase.Database;
using Firebase.Unity.Editor;
using UnityEngine;

public class DatabaseManager : MonoBehaviour
{
    public static DatabaseManager sharedInstance = null;

    public event Action<PlayerRecord> RetrievedPlayerRecord;
    public event Action<bool> PlayerExists;

    private void Awake()
    {
        if (sharedInstance == null)
            sharedInstance = this;
        else if(sharedInstance != this)
            Destroy(gameObject);

        DontDestroyOnLoad(gameObject);

        FirebaseApp.DefaultInstance.SetEditorDatabaseUrl("https://raising-spirits.firebaseio.com/");
    }

    public void CreateNewPlayer(AuthPlayer player, string uid)
    {
        Debug.Log("Pushed new player");
        string jsonString = JsonUtility.ToJson(player);
        Router.PlayerWithUID(uid).SetRawJsonValueAsync(jsonString);
        Debug.Log("New Player Created");
    }

    public void CheckIfPlayerExists(string uid)
    {
        Router.PlayerWithUID(uid).GetValueAsync().ContinueWith(task =>
        {
            if (task.IsFaulted || task.IsCanceled)
            {
                Debug.Log("Error in database retrieval. ERROR: " + task.Exception);
                return;
            }

            DataSnapshot snapshot = task.Result;

            if (snapshot.Value != null)
                PlayerExists(true); // The player exists
            else
                PlayerExists(false); // The player does not exist

        });
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

    public void GetPlayerRecords()
    {
        FirebaseUser user = AuthManager.sharedInstance.GetCurrentUser();

        Router.PlayerRecord(user.UserId).GetValueAsync().ContinueWith(task =>
        {
            if (task.IsFaulted || task.IsCanceled)
            {
                return;
            } else if (task.IsCompleted)
            {
                DataSnapshot snapshot = task.Result;
                var recordDict = (Dictionary<string, object>) snapshot.Value;
                int winsRetrieved = Convert.ToInt32(recordDict["wins"]);
                int lossesRetrieved = Convert.ToInt32(recordDict["losses"]);
                PlayerRecord record = new PlayerRecord(winsRetrieved, lossesRetrieved);
                RetrievedPlayerRecord(record);
            }
        });
    }
}
