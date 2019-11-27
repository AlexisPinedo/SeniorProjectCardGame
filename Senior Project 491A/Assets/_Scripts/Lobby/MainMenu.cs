using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [SerializeField]
    private GameObject mainMenuScreen, singlePlayerScreen, photonManager, nameScreenCanvas;

    [SerializeField]
    private GameObject singlePlayerBtn, multiplayerBtn, optionsBtn;

    #region OnClick Methods
    public void OnClick_SinglePlayer()
    {
        mainMenuScreen.SetActive(false);
        singlePlayerScreen.SetActive(true);
    }

    public void OnClick_Multiplayer()
    {
        mainMenuScreen.SetActive(false);
        nameScreenCanvas.SetActive(true);
    }

    public void OnClick_Options()
    {
        Debug.Log("Go to Options Canvas");
        Debug.LogWarning("OnClick_Options() NOT IMPLEMENTED YET");
    }
    #endregion
}
