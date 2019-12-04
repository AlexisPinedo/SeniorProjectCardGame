using UnityEngine;

/// <summary>
/// Main Menu handler with references to other relevant canvases and the Photon manager.
/// </summary>
public class MainMenu : MonoBehaviour
{
    [SerializeField]
    private GameObject mainMenuScreen, singlePlayerScreen, photonManager, nameScreenCanvas, heroPickerCanvas;

    [SerializeField]
    private GameObject singlePlayerBtn, multiplayerBtn, optionsBtn;

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
