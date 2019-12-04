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
    Text heroSelected, playerNickName;

    [SerializeField]
    private GameObject valorIconP1, vannIconP1, vaughnIconP1, vedaIconP1, vickyIconP1, vitoIconP1, unknownIconP1;

    public static int offlineSelectedHero;

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
            playerNickName.text = PhotonNetwork.LocalPlayer.NickName;
        }
    }

    public void OnClick_StartMatch()
    {
        if (heroSelected.text != "")
        {
            offlineSelectedHero = GetHeroNumber(heroSelected.text);
            SceneManager.LoadScene(1);
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
            AssignPlayerOne(heroSelected.text, false);
            heroSelected.text = "?";
            heroPickerCanvas.SetActive(false);
            roomLobbyCanvas.SetActive(true);
        }
        else
        {
            heroSelected.text = "Pick a hero";
        }
    }

    /// <summary>
    /// Sets the text for the hero based upon which icon was clicked.
    /// </summary>
    /// <param name="heroName"></param>
    public void OnClick_HeroClicked(string heroName)
    {
        AssignPlayerOne(heroSelected.text, false);
        heroSelected.text = heroName;
        AssignPlayerOne(heroName, true);
    }

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
