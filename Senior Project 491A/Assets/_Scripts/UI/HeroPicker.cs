using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Hashtable = ExitGames.Client.Photon.Hashtable;

public class HeroPicker : MonoBehaviour
{
    [SerializeField]
    GameObject heroPickerCanvas, roomLobbyCanvas;

    /// <summary>
    /// Confirm the hero selection for multiplayer.
    /// </summary>
    [SerializeField]
    GameObject confirmButton;

    /// <summary>
    /// Start the match for single player.
    /// </summary>
    [SerializeField]
    GameObject startButton;

    [SerializeField]
    Text heroSelected;

    [SerializeField]
    private Player SinglePlayerHero;

    [SerializeField] private Valor valor;
    [SerializeField] private Vann van;
    [SerializeField] private Vaughn vaughn;
    [SerializeField] private Veda veda;
    [SerializeField] private Vicky vicky;
    [SerializeField] private Vito vito;

    public void OnEnable()
    {
        if (!PhotonNetwork.IsConnected)
        {
            confirmButton.SetActive(false);
            startButton.SetActive(true);
        }
        else
        {
            confirmButton.SetActive(true);
            startButton.SetActive(false);
        }
    }

    public void OnClick_StartMatch()
    {
        if (heroSelected.text != "")
        {
            int heroNum = GetHeroNumber(heroSelected.text);

            // Single Player
            switch ((Heroes)heroNum)
            {
                case Heroes.Valor:
                    SinglePlayerHero.SelectedHero = valor;
                    break;
                case Heroes.Vann:
                    SinglePlayerHero.SelectedHero = van;
                    break;
                case Heroes.Vaughn:
                    SinglePlayerHero.SelectedHero = vaughn;
                    break;
                case Heroes.Veda:
                    SinglePlayerHero.SelectedHero = veda;
                    break;
                case Heroes.Vicky:
                    SinglePlayerHero.SelectedHero = vicky;
                    break;
                case Heroes.Vito:
                    SinglePlayerHero.SelectedHero = vito;
                    break;
                default:
                    SinglePlayerHero.SelectedHero = null;
                    break;
            }

            SceneManager.LoadScene(2);
        }
        else
        {
            heroSelected.text = "Pick a hero";
        }
    }

    public void OnClick_ConfirmHeroSelection()
    {
        if (heroSelected.text != "")
        {
            int heroNum = GetHeroNumber(heroSelected.text);

            Hashtable roomProps = PhotonNetwork.CurrentRoom.CustomProperties;

            if (!PhotonNetwork.LocalPlayer.IsMasterClient)
                if (roomProps.ContainsKey("playerTwoHero"))
                    roomProps.Remove("playerTwoHero");

            if (PhotonNetwork.LocalPlayer.IsMasterClient)
                if (roomProps.ContainsKey("playerOneHero"))
                    roomProps.Remove("playerOneHero");

            if (PhotonNetwork.IsMasterClient)
            {
                PhotonManager.playerOneHero = (Heroes)heroNum;
                roomProps.Add("playerOneHero", PhotonManager.playerOneHero);
                Debug.Log("Player 1 selected " + PhotonManager.playerOneHero);
            }
            else
            {
                PhotonManager.playerTwoHero = (Heroes)heroNum;
                roomProps.Add("playerTwoHero", PhotonManager.playerTwoHero);
                Debug.Log("Player 2 selected " + PhotonManager.playerTwoHero);
            }

            PhotonNetwork.CurrentRoom.SetCustomProperties(roomProps);

            heroPickerCanvas.SetActive(false);
            roomLobbyCanvas.SetActive(true);
        }
        else
        {
            heroSelected.text = "Pick a hero";
        }
    }

    private int GetHeroNumber(string heroName)
    {
        int heroNum = -1;

        switch (heroName)
        {
            case "Valor":
                heroNum = 0;
                break;
            case "Vann":
                heroNum = 1;
                break;
            case "Vaughn":
                heroNum = 2;
                break;
            case "Veda":
                heroNum = 3;
                break;
            case "Vicky":
                heroNum = 4;
                break;
            case "Vito":
                heroNum = 5;
                break;
            default:
                heroNum = -1;
                break;
        }

        return heroNum;
    }
}
