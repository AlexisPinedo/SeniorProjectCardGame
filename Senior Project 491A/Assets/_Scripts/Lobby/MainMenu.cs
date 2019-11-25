using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [SerializeField]
    private GameObject mainMenuScreen, singlePlayerScreen, photonManager;

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
        photonManager.SetActive(true);  // This is the multiplayer screen essentially
        // TODO: delete?
        //multiplayerScreen.SetActive(true);
    }

    public void OnClick_Options()
    {
        Debug.Log("Go to Options Canvas");
        Debug.LogWarning("OnClick_Options() NOT IMPLEMENTED YET");
    }
    #endregion
}
