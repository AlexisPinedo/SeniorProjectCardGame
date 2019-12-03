using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using System.Text.RegularExpressions;
using Firebase.Auth;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public abstract class Forms : MonoBehaviour
{
    [SerializeField] protected TMP_InputField emailInput;
    [SerializeField] protected TMP_InputField passwordInput;

    protected bool emailAuthorized;
    protected bool isFilled;

    protected void Awake()
    {
        AuthManager.sharedInstance.authCallback += HandleAuthCallback;
    }

    protected IEnumerator HandleAuthCallback(Task<Firebase.Auth.FirebaseUser> task, string operation)
    {
        if (task.IsFaulted || task.IsCanceled)
        {
            Debug.LogError("Issue with Authentication. ERROR: " + task.Exception);
        }
        else if (task.IsCompleted)
        {
            if (operation == "sign_up")
            {
                Firebase.Auth.FirebaseUser newPlayer = task.Result;

                //DateTime createdAt = DateTime.Now;
                AuthPlayer player = new AuthPlayer(newPlayer.Email);

                Debug.Log("Creating new player");
                DatabaseManager.sharedInstance.CreateNewPlayer(player, newPlayer.UserId);

                SceneManager.LoadSceneAsync("Lobby");
            }
            else if (operation == "login")
            {
                FirebaseUser user = task.Result;

                yield return new WaitForSeconds(1.0f);

                SceneManager.LoadSceneAsync("Lobby");
            }
        }
    }

    // TODO: ADD CHECK EMAIL IS VALID
    public void ValidateEmail()
    {
        string email = emailInput.text;
        var regexPattern = @"^(([\w-]+\.)+[\w-]+|([a-zA-Z]{1}|[\w-]{2,}))@"
                           + @"((([0-1]?[0-9]{1,2}|25[0-5]|2[0-4][0-9])\.([0-1]?[0-9]{1,2}|25[0-5]|2[0-4][0-9])\."
                           + @"([0-1]?[0-9]{1,2}|25[0-5]|2[0-4][0-9])\.([0-1]?[0-9]{1,2}|25[0-5]|2[0-4][0-9])){1}|"
                           + @"([a-zA-Z]+[\w-]+\.)+[a-zA-Z]{2,4})$";

        if (email != "" && Regex.IsMatch(email, regexPattern))
        {
            emailAuthorized = true;
        }
        else
        {
            emailAuthorized = false;
        }
    }

    public void IsFilled()
    {
        string email = emailInput.text;
        string password = passwordInput.text;

        if (String.IsNullOrEmpty(email) || String.IsNullOrEmpty(password))
        {
            isFilled = false;
        }
        else
            isFilled = true;
    }

}
