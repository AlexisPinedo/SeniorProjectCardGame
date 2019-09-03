using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Amazon;
using Amazon.CognitoIdentity;
using Amazon.CognitoSync;
using Amazon.CognitoSync.SyncManager;
using Amazon.IdentityManagement.Model.Internal.MarshallTransformations;
using UnityEngine;
using UnityEngine.Scripting;

public class AWSConfig : MonoBehaviour
{
    private Dataset playerInfo;
    private string alias, playerName, password;
    //TODO: Add Input Fields 

    private string IdentityPoolId = "us-west-2:03e86ab6-282d-4c8d-9f75-77849ce61e96";

    public string Region = RegionEndpoint.USWest2.SystemName;

    private RegionEndpoint _Region
    {
        get { return RegionEndpoint.GetBySystemName(Region); }
    }

    private CognitoAWSCredentials _credentials;
    // Create an instance of the AWS Identity Pool to store the credentials
    private CognitoAWSCredentials Credentials
    {
        get
        {
            if (_credentials == null)
                _credentials = new CognitoAWSCredentials(IdentityPoolId, _Region);
            return _credentials;
        }
    }


    private CognitoSyncManager _syncManager;
    // Create an instance of Cognito Sync Manager to start synchronizing data
    private CognitoSyncManager SyncManager
    {
        get
        {
            if (_syncManager == null)
            {
                _syncManager =
                    new CognitoSyncManager(Credentials, new AmazonCognitoSyncConfig {RegionEndpoint = _Region});
            }

            return _syncManager;
        }
    }


    void Awake()
    {
        UnityInitializer.AttachToGameObject(this.gameObject);

        // Open your datasets
        playerInfo = SyncManager.OpenOrCreateDataset("PlayerInfo");

        // Fetch locally stored data from a previous run and add it to UI input line
        alias = string.IsNullOrEmpty(playerInfo.Get("alias")) ? "Enter your alias" : playerInfo.Get("alias");
        playerName = string.IsNullOrEmpty(playerInfo.Get("playerName"))
            ? "Enter your full name"
            : playerInfo.Get("playerName");
        password = string.IsNullOrEmpty(playerInfo.Get("password"))
            ? "Enter your password"
            : playerInfo.Get("password");

        // Define the synchronize callbacks
        playerInfo.OnSyncSuccess += this.HandleSyncSuccess; //utilizes delegates/events pattern
        playerInfo.OnSyncFailure += this.HandleSyncFailure; //utilizes delegates/events pattern
        playerInfo.OnSyncConflict = this.HandleSyncConflict;
        playerInfo.OnDatasetMerged = this.HandleDatasetMerged;
        playerInfo.OnDatasetDeleted = this.HandleDatasetDeleted;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    #region Synchronize Callbacks

    void HandleSyncSuccess(object sender, SyncSuccessEventArgs e)
    {

    }

    void HandleSyncFailure(object sender, SyncFailureEventArgs e)
    {

    }

    private bool HandleSyncConflict(Dataset dataset, List<SyncConflict> conflicts)
    {

        return true;
    }

    private bool HandleDatasetDeleted(Dataset dataset)
    {
        return true;
    }

    private bool HandleDatasetMerged(Dataset dataset, List<string> datasetNames)
    {
        return true;
    }

    #endregion


}
