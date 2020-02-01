using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public class NetworkIDAssigner : MonoBehaviour
{
    public static int photonIDCounter = 1001;

    private static NetworkIDAssigner _instance;

    public static NetworkIDAssigner Instance
    {
        get => _instance;
    }
    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }
    }

    public static void AssignID(PhotonView currentObject)
    {
        if (currentObject.ViewID == 0)
            currentObject.ViewID = photonIDCounter++;
        currentObject.TransferOwnership(NetworkOwnershipTransferManger.currentPhotonPlayer);
    }
}
