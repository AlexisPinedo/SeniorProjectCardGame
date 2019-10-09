using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SignIn : MonoBehaviour
{
    [SerializeField] private InputField emailInput;
    [SerializeField] private InputField passwordInput;

    public void EmailSignIn()
    {
        AuthManager.sharedInstance.SignInUserWithEmail(emailInput.text, passwordInput.text);
    }

    public void FacebookSignIn()
    {
        AuthManager.sharedInstance.FBLogin("fb_login");
    }

    public void GoogleSignIn()
    {
        AuthManager.sharedInstance.GoogleLogin("google_login");
    }
}
