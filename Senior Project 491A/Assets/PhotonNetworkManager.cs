using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class PhotonNetworkManager : MonoBehaviourPunCallbacks
{

    [SerializeField]
    GameObject player1Prefab, player2Prefab, shopPrefab, playZonePrefab, gameCanvasPrefab;

    private static bool _offline = true;

    public static bool IsOffline {get { return _offline; }}


    void Start()
    {
        if (PhotonNetwork.IsConnectedAndReady)
        {
            _offline = false;
            if (PhotonNetwork.IsMasterClient)
            {
                PhotonNetwork.InstantiateSceneObject(player1Prefab.name, new Vector3(0, 0, 0), Quaternion.identity);
                PhotonNetwork.InstantiateSceneObject(shopPrefab.name, new Vector3(0, 0, 0), Quaternion.identity);
                PhotonNetwork.InstantiateSceneObject(playZonePrefab.name, new Vector3(0, 0, 0), Quaternion.identity);
                PhotonNetwork.InstantiateSceneObject(gameCanvasPrefab.name, new Vector3(0, 0, 0), Quaternion.identity);
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
        else
        {
            PhotonNetwork.OfflineMode = true;
            Instantiate(player1Prefab, new Vector3(0, 0, 0), Quaternion.identity);
            Instantiate(shopPrefab, new Vector3(0, 0, 0), Quaternion.identity);
            Instantiate(playZonePrefab, new Vector3(0, 0, 0), Quaternion.identity);
            Instantiate(gameCanvasPrefab, new Vector3(0, 0, 0), Quaternion.identity);
        }
    }

    void OnPhotonInstantiate(PhotonMessageInfo info)
    {
        Debug.Log("tagging object...");
        info.Sender.TagObject = this.gameObject;
    }

}
