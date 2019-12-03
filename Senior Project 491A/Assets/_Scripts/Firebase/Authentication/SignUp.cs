using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class SignUp : Forms
{
    [SerializeField] private TMP_InputField passwordRetype;

    

    private bool passAuthorized;

    //Check Password Retype
    public void ValidatePassword()
    {
        if (passwordInput.text != passwordRetype.text)
        {
            passAuthorized = false;
            Debug.Log("Passwords are not equal");
        }
        else if (passwordInput.text == passwordRetype.text)
        {
            //bool hasUpper = passwordInput.text.Any(char.IsUpper);
            //bool lengthStandard = passwordInput.text.Length >= 6;
            //bool hasLower = passwordInput.text.Any(char.IsLower);
            //bool hasNumber = passwordInput.text.Any(char.IsNumber);
            //if (hasUpper && lengthStandard & hasLower & hasNumber)
            //{
            //    passAuthorized = true;
            //}
            //else
            //{
            //    passAuthorized = false;
            //    Debug.Log("Password doesn't match the required criteria");
            //    Debug.Log("Upper: " + hasUpper + " Lower: " + hasLower + " Length: " + lengthStandard + " Numbers: " + hasNumber);
            //}
            passAuthorized = true;
        }
    }

    public void CanCreateAccount()
    {
        if (emailAuthorized && passAuthorized)
            EmailSignUp();
        else
        {
            if (!emailAuthorized && passAuthorized)
                Debug.LogError("Email not valid");
            else if (emailAuthorized && !passAuthorized)
                Debug.LogError("Password does not meet valid criteria");
            else
                Debug.LogError("Both Email and Password are invalid");
        }
    }



    //public void EmailRegistrationBtn(bool shouldOpen)
    //{
    //    if(shouldOpen)
    //        emailPopUp.SetActive(true);
    //    else 
    //        emailPopUp.SetActive(false);
    //}

    private void EmailSignUp()
    {
        AuthManager.sharedInstance.SignUpNewUserWithEmail(emailInput.text, passwordInput.text);
    }

    //public void FacebookSignUp()
    //{
    //    AuthManager.sharedInstance.FBLogin("facebook_sign_up");
    //}

    //public void GoogleSignUp()
    //{
    //    AuthManager.sharedInstance.GoogleLogin("google_sign_up");
    //}

    public void BackToSignIn()
    {
        SceneManager.LoadScene("SignIn");
    }
}
