using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class UIViewHandler : MonoBehaviourPunCallbacks
{
    //[SerializeField]
    //public GameObject startBattleButton, endTurnButton;

    //private void Awake()
    //{
    //    DisplayButtons();
    //}

    //private void OnEnable()
    //{
    //    UIHandler.EndTurnClicked += DisplayButtons;
    //}
    //private void OnDisable()
    //{
    //    UIHandler.EndTurnClicked += DisplayButtons;
    //}

    //public void DisplayButtons()
    //{
    //    if (photonView.IsMine)
    //    {
    //        Debug.Log("my turn, show the buttons!");
    //        startBattleButton.SetActive(true);
    //        endTurnButton.SetActive(true);
    //    }
    //    else
    //    {
    //        Debug.Log("not my turn, buttons be gone!");
    //        startBattleButton.SetActive(false);
    //        endTurnButton.SetActive(false);
    //    }
    //}
}
