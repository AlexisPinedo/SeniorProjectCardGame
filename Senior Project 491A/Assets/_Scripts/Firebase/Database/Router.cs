using System.Collections;
using System.Collections.Generic;
using Firebase.Database;
using UnityEngine;

public class Router : MonoBehaviour
{
    private static DatabaseReference baseRef = FirebaseDatabase.DefaultInstance.RootReference;

    public static DatabaseReference Base()
    {
        return baseRef;
    }

    public static DatabaseReference Players()
    {
        return baseRef.Child("players");
    }

    public static DatabaseReference PlayerWithUID(string uid)
    {
        return baseRef.Child("players").Child(uid);
    }

    public static DatabaseReference PlayerRecord(string uid)
    {
        return baseRef.Child("player_record").Child(uid);
    }
}
