using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SinglePlayerMenu : MonoBehaviour
{
    [SerializeField]
    private GameObject mainMenuScreen, singlePlayerScreen;

    [SerializeField]
    private GameObject backBtn;

    #region OnClick Methods
    public void OnClick_Back()
    {
        Debug.Log("Going back to main menu");
        singlePlayerScreen.SetActive(false);
        mainMenuScreen.SetActive(true);
    }
    #endregion

}
