using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class PhotonNetworkManager : MonoBehaviourPunCallbacks
{

    [SerializeField]
    GameObject player1Prefab, player2Prefab, shopPrefab, playZonePrefab, gameCanvasPrefab;

    GameObject player1, player2, shop, playZone, gameCanvas;

    // Start is called before the first frame update

    void Start()
    {
        //Online Gameplay
        if (PhotonNetwork.IsConnected)
        {
            if (PhotonNetwork.IsMasterClient)
            {
                player1 = PhotonNetwork.Instantiate(player1Prefab.name, new Vector3(0, 0, 0), Quaternion.identity);
                shop = PhotonNetwork.Instantiate(shopPrefab.name, new Vector3(0, 0, 0), Quaternion.identity);
                playZone = PhotonNetwork.Instantiate(playZonePrefab.name, new Vector3(0, 0, 0), Quaternion.identity);
                gameCanvas = PhotonNetwork.Instantiate(gameCanvasPrefab.name, new Vector3(0, 0, 0), Quaternion.identity);
            }
            else
            {
                //player2 = PhotonNetwork.Instantiate(player2Prefab.name, new Vector3(0, 0, 0), Quaternion.identity);
                //player2.SetActive(false);
            }
        }
        //Offline Gameplay
        else
        {
            Debug.Log("photon network is offline... offline gameplay mode is stashed.");
        }
    }
}
