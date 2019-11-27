using System;
using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;
using UnityEngine.UI;

public class ButtonInputManager : MonoBehaviour
{
    [SerializeField]
    private List<Button> buttonList = new List<Button>();

    [SerializeField]
    private GameObject startBattleButton, endTurnButton;

    private static ButtonInputManager _instance;

    public static ButtonInputManager Instance
    {
        get { return _instance; }
    }

    private void Awake()
    {
        if (_instance == null && _instance != this)
        {
            _instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    private void OnEnable()
    {
        TurnPhaseManager.PlayerTurnStarted += CurrentTurnButtonSwitch;
    }

    private void OnDisable()
    {
        TurnPhaseManager.PlayerTurnStarted -= CurrentTurnButtonSwitch;
    }

    private void Start()
    {
        CurrentTurnButtonSwitch();
    }

    public void CurrentTurnButtonSwitch()
    {
        if (PhotonNetwork.OfflineMode)
        {
            endTurnButton.GetComponent<Button>().interactable = false;
            startBattleButton.GetComponent<Button>().interactable = false;
            StartCoroutine(ShouldActivateButton(endTurnButton.GetComponent<Button>()));
            StartCoroutine(ShouldActivateButton(startBattleButton.GetComponent<Button>()));
            return;
        }
        

        if (!NetworkOwnershipTransferManger.currentPhotonPlayer.IsLocal)
        {
            startBattleButton.SetActive(false);
            endTurnButton.SetActive(false);
        }
        else
        {
            startBattleButton.SetActive(true);
            startBattleButton.GetComponent<Button>().interactable = false;
            endTurnButton.SetActive(true);
            endTurnButton.GetComponent<Button>().interactable = false;
            StartCoroutine(ShouldActivateButton(endTurnButton.GetComponent<Button>()));
            StartCoroutine(ShouldActivateButton(startBattleButton.GetComponent<Button>()));
        }
    }

    IEnumerator ShouldActivateButton(Button button)
    {
        yield return new WaitForSeconds(0.2f);

        Debug.Log("In Here 1");

        while (AnimationManager.SharedInstance.CardAnimActive || AnimationManager.SharedInstance.ShopAnimActive)
        {
            Debug.Log("In Here");
            yield return null;
        }

        button.interactable = true;

    }


    public void DisableButtonsInList()
    {
        foreach (Button aButton in buttonList)
        {
            aButton.enabled = false;
        }
    }

    public void EnableButtonsInList()
    {
        foreach (Button aButton in buttonList)
        {
            aButton.enabled = true;
        }
    }
}
