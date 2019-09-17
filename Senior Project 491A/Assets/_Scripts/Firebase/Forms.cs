using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;
using Firebase.Auth;

public class Forms : MonoBehaviour
{
    void Start()
    {
        AuthManager.sharedInstance.authCallback += HandleAuthCallback;
    }

    IEnumerator HandleAuthCallback(Task<FirebaseUser> task, string operation)
    {
        if (task.IsFaulted || task.IsCanceled)
        {
            Debug.LogError("Issue with authentication. ERROR: " + task.Exception);
        }
        else if (task.IsCompleted)
        {
            if (operation == "email_sign_up" || operation == "facebook_sign_up" || operation == "google_sign_up")
            {
                FirebaseUser newPlayer = task.Result;

                // Push User Initialized values to the Database

                // Send Email Verification
                if (operation == "email_sign_up")
                {
                    newPlayer.SendEmailVerificationAsync().ContinueWith(t =>
                    {
                        // Display message to verify email
                    });
                }
                else
                {
                    // Load the next scene
                }
            }
            else if (operation == "email_login" || operation == "facebook_login" || operation == "google_login")
            {
                FirebaseUser user = task.Result;

                if (!user.IsEmailVerified && operation == "email_login")
                {
                    Debug.LogError("EMAIL IS NOT VERIFIED");
                }
                else
                {
                    yield return new WaitForSeconds(0.5f);

                    // Load the Game's home screen 
                }
            }
        }
    }
}
