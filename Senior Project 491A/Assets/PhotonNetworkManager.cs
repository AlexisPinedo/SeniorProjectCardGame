using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class PhotonNetworkManager : MonoBehaviourPunCallbacks
{

    [SerializeField]
    GameObject player1Prefab, player2Prefab, shopPrefab, playZonePrefab, gameCanvasPrefab;

    // Start is called before the first frame update

    void Start()
    {
        //Online Gameplay
        if (PhotonNetwork.IsConnected)
        {
            Debug.Log("Network is connected... instantiating objects");
            if (PhotonNetwork.IsMasterClient)
            {
                PhotonNetwork.Instantiate(player1Prefab.name, new Vector3(0, 0, 0), Quaternion.identity);
                PhotonNetwork.Instantiate(shopPrefab.name, new Vector3(0, 0, 0), Quaternion.identity);
                PhotonNetwork.Instantiate(playZonePrefab.name, new Vector3(0, 0, 0), Quaternion.identity);
                PhotonNetwork.Instantiate(gameCanvasPrefab.name, new Vector3(0, 0, 0), Quaternion.identity);

            }
            else
            {
                /**
                 *  TODO: Fix on photon instantiation to manipulate instantiated objects from different clients.
                 *  PhotonNetwork.Instantiate(player2Prefab.name, new Vector3(0, 0, 0), Quaternion.identity);
                 */

            }

            //player2 = GameObject.FindWithTag("Player 2");
            //player2 = GameObject.Find("Player2(Clone)");
            //if (player2 != null)
            //    player2.SetActive(false);
            //else
            //    Debug.Log("could not find...");
        }
        //Offline Gameplay
        else
        {
            Debug.Log("Photon network is offline... offline gameplay mode is stashed.");
        }
    }

    void OnPhotonInstantiate(PhotonMessageInfo info)
    {
        Debug.Log("tagging object...");
        info.Sender.TagObject = this.gameObject;
    }

}
