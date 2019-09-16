using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Amazon;
using Amazon.CognitoIdentity;
using UnityEngine;

public class AWSConfig : MonoBehaviour
{

    CognitoAWSCredentials credentials = new CognitoAWSCredentials(
        "us-west-2:03e86ab6-282d-4c8d-9f75-77849ce61e96", // Identity pool ID
        RegionEndpoint.USWest2 // Region
    );

    void Awake()
    {
        UnityInitializer.AttachToGameObject(this.gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }


}
