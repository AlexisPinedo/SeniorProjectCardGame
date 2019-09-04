using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class AWSForms : MonoBehaviour
{
    public InputField username_input;
    public InputField password_input;

    protected bool emailAuthorized = false;

    void Awake()
    {
        // make an auth delegate subscription here for user authorization
    }

    
}
