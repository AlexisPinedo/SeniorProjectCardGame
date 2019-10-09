using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using Firebase.Auth;
using UnityEngine;
using UnityEngine.UI;

public abstract class Forms : MonoBehaviour
{

    private void Awake()
    {
        AuthManager.sharedInstance.authCallback += HandleAuthCallback;
    }

    IEnumerator HandleAuthCallback(Task<Firebase.Auth.FirebaseUser> task, string operation)
    {
        if (task.IsFaulted || task.IsCanceled)
        {
            Debug.LogError("Issue with Authentication. ERROR: " + task.Exception);
        }
        else if (task.IsCompleted)
        {
            if (operation == "email_sign_up" || operation == "fb_sign_up" || operation == "google_sign_up" || operation == "anon_login")
            {
                Firebase.Auth.FirebaseUser newPlayer = task.Result;

                if (operation == "email_sign_up")
                {

                }
            }
            else if (operation == "email_login" || operation == "fb_login" || operation == "google_login")
            {
                FirebaseUser user = task.Result;

                yield return new WaitForSeconds(1.0f);
            }
        }
    }

    // TODO: ADD CHECK EMAIL IS VALID

    
}
