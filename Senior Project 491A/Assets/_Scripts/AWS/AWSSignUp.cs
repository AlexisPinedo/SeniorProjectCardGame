using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AWSSignUp : AWSForms
{
    public InputField userAlias, passwordRetype;

    private bool passAuthorized = false;

    public void ValidatePassword()
    {
        if (password_input.text != passwordRetype.text)
        {
            passAuthorized = false;
        } else if (password_input.text == passwordRetype.text)
            passAuthorized = true;
    }

    // Add email validation here (regex function)

    // Check if all fields are valid (function)
    // If valid allow user to sign up
}
