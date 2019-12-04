using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OptionsManager : MonoBehaviour
{
    public void SignOut()
    {
        AuthManager.sharedInstance.Logout();
    }
}
