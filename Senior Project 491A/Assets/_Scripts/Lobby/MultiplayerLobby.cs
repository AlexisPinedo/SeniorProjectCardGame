using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MultiplayerLobby : MonoBehaviour
{
    [SerializeField]
    private GameObject multiplayerLobby, mainMenuScreen, photonManager;

    [SerializeField]
    private GameObject backBtn;

    #region OnClick Methods
    public void OnClick_Back()
    {
        Debug.Log("Going back to main screen");
        multiplayerLobby.SetActive(false);
        photonManager.SetActive(false);
        mainMenuScreen.SetActive(true);
    }
    #endregion

}
