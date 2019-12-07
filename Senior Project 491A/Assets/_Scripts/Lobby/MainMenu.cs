using UnityEngine;
using Photon.Pun;

/// <summary>
/// Main Menu handler with references to other relevant canvases and the Photon manager.
/// </summary>
public class MainMenu : MonoBehaviour
{
    [SerializeField]
    private GameObject mainMenuScreen, singlePlayerScreen, multiPlayerScreen, photonManager, nameScreenCanvas, heroPickerCanvas;

    [SerializeField]
    private GameObject singlePlayerBtn, multiplayerBtn, optionsBtn;

    private void OnEnable()
    {
        /**
         * We are leaving a game.
         * Should be connected.
         * Skips mode selection and attempts to rejoin the lobby.
         */
        if (PhotonNetwork.IsConnected)
        {
            mainMenuScreen.SetActive(false);
            photonManager.SetActive(true);
        }
    }

    #region OnClick Methods
    public void OnClick_SinglePlayer()
    {
        mainMenuScreen.SetActive(false);
        singlePlayerScreen.SetActive(true);
        heroPickerCanvas.SetActive(true);
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
