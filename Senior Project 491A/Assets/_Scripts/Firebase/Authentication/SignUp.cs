using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SignUp : Forms
{
    [SerializeField] private InputField emailInput;
    [SerializeField] private InputField passwordInput;
    [SerializeField] private InputField passwordRetype;

    [SerializeField] private GameObject emailPopUp;

    public void EmailRegistrationBtn(bool shouldOpen)
    {
        if(shouldOpen)
            emailPopUp.SetActive(true);
        else 
            emailPopUp.SetActive(false);
    }

    public void EmailSignUp()
    {
        AuthManager.sharedInstance.SignUpNewUserWithEmail(emailInput.text, passwordInput.text);
    }

    public void FacebookSignUp()
    {
        AuthManager.sharedInstance.FBLogin("facebook_sign_up");
    }

    public void GoogleSignUp()
    {
        AuthManager.sharedInstance.GoogleLogin("google_sign_up");
    }

    public void AlreadyHaveAccount()
    {
        SceneManager.LoadScene("SignIn");
    }
}
