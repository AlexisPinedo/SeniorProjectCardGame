using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
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

    

    #region Synchronize Callbacks

    void HandleSyncSuccess(object sender, SyncSuccessEventArgs e)
    {
        var dataset = sender as Dataset;

        if (dataset.Metadata != null)
        {
            Debug.Log("Successfully synced for dataset: " + dataset.Metadata);
        }
        else
        {
            Debug.Log("Successfully synced for dataset");
        }

        if (dataset == playerInfo)
        {
            alias = string.IsNullOrEmpty(playerInfo.Get("alias")) ? "Enter your alias" : dataset.Get("alias");
            playerName = string.IsNullOrEmpty(playerInfo.Get("playerName")) ? "Enter your name" : dataset.Get("playerName");
            password = string.IsNullOrEmpty(playerInfo.Get("password")) ? "Enter your password" : dataset.Get("password");
        }
    }

    void HandleSyncFailure(object sender, SyncFailureEventArgs e)
    {
        var dataset = sender as Dataset;
        Debug.Log("Sync failed for dataset : " + dataset.Metadata.DatasetName);
        Debug.LogException(e.Exception);
    }

    // Conflicts may arise if the same key has been modified on the local store and in the sync store. The OnSyncConflict callback handles conflict resolution.
    private bool HandleSyncConflict(Dataset dataset, List<SyncConflict> conflicts)
    {
        if (dataset.Metadata != null)
            Debug.LogWarning("Sync conflict " + dataset.Metadata.DatasetName);
        else
            Debug.LogWarning("Sync conflict");


        List<Record> resolvedRecords = new List<Record>();

        foreach (SyncConflict conflictRecord in conflicts)
        {
            //ResolveWithRemoteRecord - overwrites the local with remote records
            resolvedRecords.Add(conflictRecord.ResolveWithRemoteRecord());
        }

        // resolves the conflicts in the local storage
        dataset.Resolve(resolvedRecords);

        // on return true the synchronize operation continues where it left,
        //      returning false cancels the synchronize operation
        return true;
    }

    private bool HandleDatasetDeleted(Dataset dataset)
    {
        Debug.Log(dataset.Metadata.DatasetName + " Dataset has been deleted");

        // Clean up if necessary 

        // returning true informs the corresponding dataset can be purged in the local storage and return false retains the local dataset

        return true;
    }

    // When two previously unconnected identities are linked together, all of their datasets are merged
    private bool HandleDatasetMerged(Dataset dataset, List<string> datasetNames)
    {
        Debug.Log(dataset + " Dataset needs merge");

        // returning true allows the Synchronize to resume and false cancels it
        return true;
    }

    #endregion


}
