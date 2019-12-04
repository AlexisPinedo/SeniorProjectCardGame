using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Single player menu handler with references to other relevant canvases.
/// </summary>
public class SinglePlayerMenu : MonoBehaviour
{
    [SerializeField]
    private GameObject mainMenuScreen, singlePlayerScreen, heroPickerCanvas;

    [SerializeField]
    private GameObject backBtn;

    #region OnClick Methods
    public void OnClick_Back()
    {
        singlePlayerScreen.SetActive(false);
        heroPickerCanvas.SetActive(false);
        mainMenuScreen.SetActive(true);
    }
    #endregion
}
