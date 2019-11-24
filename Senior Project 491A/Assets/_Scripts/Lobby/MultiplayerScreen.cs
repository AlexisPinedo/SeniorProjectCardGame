using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MultiplayerScreen : MonoBehaviour
{
    [SerializeField]
    private GameObject mainMenuScreen, multiplayerScreen;

    [SerializeField]
    private GameObject backBtn;

    #region OnClick Methods
    public void OnClick_Back()
    {
        Debug.Log("going back to main menu");
        multiplayerScreen.SetActive(false);
        mainMenuScreen.SetActive(true);
    }
    #endregion
}
