using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [SerializeField]
    private GameObject mainMenuScreen, singlePlayerScreen, multiplayerScreen;

    [SerializeField]
    private GameObject singlePlayerBtn, multiplayerBtn, optionsBtn;

    #region OnClick Methods
    public void OnClick_SinglePlayer()
    {
        Debug.Log("Go to Single Player Canvas");
        mainMenuScreen.SetActive(false);
        singlePlayerScreen.SetActive(true);
    }

    public void OnClick_Multiplayer()
    {
        Debug.Log("Go to Multiplayer Canvas");
        mainMenuScreen.SetActive(false);
        multiplayerScreen.SetActive(true);
    }

    public void OnClick_Options()
    {
        Debug.Log("Go to Options Canvas");
        Debug.LogWarning("OnClick_Options() NOT IMPLEMENTED YET");
    }
    #endregion
}
