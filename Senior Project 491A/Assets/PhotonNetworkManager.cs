using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class PhotonNetworkManager : MonoBehaviourPunCallbacks
{

    [SerializeField]
    private HandContainer p1Handcontainer;

    [SerializeField]
    private HandContainer p2HandContainer;

    [SerializeField]
    private ShopContainer shopContainer;

    [SerializeField]
    private PlayZone playZone;

    [SerializeField]
    private GameObject cavnas;

    PhotonView p1HandcontainerPV, p2HandContainerPV, shopContainerPV, playZonePV, canvasPV;

    public static Photon.Realtime.Player photonPlayer1, photonPlayer2;

    public static Photon.Realtime.Player currentPhotonPlayer;

    private static bool _offline = true;

    public static bool IsOffline { get { return _offline; } }

    void Awake()
    {
        Debug.Log("Hello from photon network manager");
        p1HandcontainerPV = p1Handcontainer.GetComponent<PhotonView>();
        p2HandContainerPV = p2HandContainer.GetComponent<PhotonView>();
        shopContainerPV = shopContainer.GetComponent<PhotonView>();
        playZonePV = playZone.GetComponent<PhotonView>();
        canvasPV = cavnas.GetComponent<PhotonView>();

        if (PhotonNetwork.IsConnected)
        {
            Debug.Log("Photon is online...");
            _offline = false;

            foreach (Photon.Realtime.Player player in PhotonNetwork.PlayerList)
            {
                if (player.IsMasterClient)
                    photonPlayer1 = player;
                else
                    photonPlayer2 = player;
            }

            currentPhotonPlayer = photonPlayer1;

            Debug.Log("Photon Player 1: " + photonPlayer1.NickName);
            Debug.Log("Photon Player 2: " + photonPlayer2.NickName);

            p1HandcontainerPV.TransferOwnership(photonPlayer1);
            p2HandContainerPV.TransferOwnership(photonPlayer2);
            shopContainerPV.TransferOwnership(photonPlayer1);
            playZonePV.TransferOwnership(photonPlayer1);
            canvasPV.TransferOwnership(photonPlayer1);

            Debug.Log("Transfered ownership...");
        }
        else
        {
            PhotonNetwork.OfflineMode = true;
            Debug.Log("Photon is offline...");
        }
    }

	private void OnEnable()
	{
		UIHandler.EndTurnClicked += TransferObjects;
	}

	private void OnDisable()
	{
		UIHandler.EndTurnClicked -= TransferObjects;
	}

	public void TransferObjects()
    {
        Debug.Log("HELLO I AM DELEGRATE FOR END TURN,,,");
        if (TurnManager.Instance.turnPlayer == TurnManager.Instance.player1)
        {
            currentPhotonPlayer = photonPlayer1;
			shopContainerPV.TransferOwnership(photonPlayer1);
			playZonePV.TransferOwnership(photonPlayer1);
			canvasPV.TransferOwnership(photonPlayer1);

            PhotonView[] shopCards = shopContainerPV.GetComponentsInChildren<PhotonView>();
            foreach (PhotonView shopCard in shopCards)
                shopCard.TransferOwnership(photonPlayer1);

		}
        else if (TurnManager.Instance.turnPlayer == TurnManager.Instance.player2)
        {
            currentPhotonPlayer = photonPlayer2;
			shopContainerPV.TransferOwnership(photonPlayer2);
			playZonePV.TransferOwnership(photonPlayer2);
			canvasPV.TransferOwnership(photonPlayer2);
            PhotonView[] shopCards = shopContainerPV.GetComponentsInChildren<PhotonView>();
            foreach (PhotonView shopCard in shopCards)
                shopCard.TransferOwnership(photonPlayer2);
        }
		Debug.Log("Transfered ownership to: " + currentPhotonPlayer.NickName);
	}
}
