using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Hashtable = ExitGames.Client.Photon.Hashtable;

/// <summary>
/// 
/// </summary>
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
    Text heroSelected, playerNickName;

    /// <summary>
    /// Hero icon.
    /// </summary>
    [SerializeField]
    private GameObject valorIconP1, vannIconP1, vaughnIconP1, vedaIconP1, vickyIconP1, vitoIconP1, unknownIconP1;

    public static int offlineSelectedHero;

    public void OnEnable()
    {
        if (!PhotonNetwork.IsConnected)
        {
            playerNickName.text = heroSelected.text;
            heroSelected.gameObject.SetActive(false);
            confirmButton.SetActive(false);
            startButton.SetActive(true);
        }
        else
        {
            heroSelected.gameObject.SetActive(true);
            confirmButton.SetActive(true);
            startButton.SetActive(false);
            playerNickName.text = PhotonNetwork.LocalPlayer.NickName;
        }
    }

    #region OnClick methods
    public void OnClick_StartMatch()
    {
        offlineSelectedHero = GetHeroNumber(heroSelected.text);
        SceneManager.LoadScene(2);
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
            AssignPlayerOne(heroSelected.text, false);
            heroSelected.text = "?";
            heroPickerCanvas.SetActive(false);
            roomLobbyCanvas.SetActive(true);
        }
    }

    /// <summary>
    /// Sets the text for the hero based upon which icon was clicked.
    /// </summary>
    /// <param name="heroName"></param>
    public void OnClick_HeroClicked(string heroName)
    {
        AssignPlayerOne(heroSelected.text, false);
        playerNickName.text = heroSelected.text = heroName;
        AssignPlayerOne(heroName, true);

        if (!PhotonNetwork.IsConnected)
            startButton.GetComponent<Button>().interactable = true;
        else
            confirmButton.GetComponent<Button>().interactable = true;
    }
    #endregion

    /// <summary>
    /// Sets the Hero icon active if the player has selected to play as said Hero.
    /// </summary>
    /// <param name="heroName">Name of the Hero selected</param>
    /// <param name="Switch">Boolean for determining whether to activate or deactivate the Hero icon.</param>
    private void AssignPlayerOne(string heroName, bool Switch)
    {
        unknownIconP1.gameObject.SetActive(!Switch);
        if (heroName.Equals("Valor"))
            valorIconP1.gameObject.SetActive(Switch);
        else if (heroName.Equals("Vann"))
            vannIconP1.gameObject.SetActive(Switch);
        else if (heroName.Equals("Vaughn"))
            vaughnIconP1.gameObject.SetActive(Switch);
        else if (heroName.Equals("Veda"))
            vedaIconP1.gameObject.SetActive(Switch);
        else if (heroName.Equals("Vicky"))
            vickyIconP1.gameObject.SetActive(Switch);
        else if (heroName.Equals("Vito"))
            vitoIconP1.gameObject.SetActive(Switch);
    }

    /// <summary>
    /// Given a Hero name, gets the Heroes enum associated number.
    /// </summary>
    /// <param name="heroName">Name of the Hero.</param>
    /// <returns>Hero number as defined in Heroes enum.</returns>
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
