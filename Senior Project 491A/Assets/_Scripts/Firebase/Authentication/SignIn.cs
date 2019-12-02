using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SignIn : Forms
{

    public void EmailSignIn()
    {
        if(isFilled && emailAuthorized)
            AuthManager.sharedInstance.SignInUserWithEmail(emailInput.text, passwordInput.text);
        else if(!isFilled)
            Debug.Log("Please fill out all fields");
        else if(!emailAuthorized)
            Debug.Log("Please fill out a valid email"); 
    }

    public void SignUpButtonClicked()
    {
        SceneManager.LoadScene("SignUp");
    }

    //public void FacebookSignIn()
    //{
    //    AuthManager.sharedInstance.FBLogin("fb_login");
    //}

    //public void GoogleSignIn()
    //{
    //    AuthManager.sharedInstance.GoogleLogin("google_login");
    //}
}
