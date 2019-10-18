using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEditor.VersionControl;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

public class ParticlePlay : MonoBehaviour
{
    [SerializeField] private AssetReference goldSummoner;
    [SerializeField] private AssetReference purpleSummoner;

    private bool canDestroyCircle;

    // Start is called before the first frame update
    void Start()
    {
        DragCard.CardDragged += PlaySummoningCircle;
        DragCard.CardReleased += CanDestroySummoner;
        PlayZone.HasPlayed += PlayPurpleCircle;
    }

    void PlaySummoningCircle()
    {
        canDestroyCircle = false;
        StartCoroutine("PlaySummoningCircleHelper");
    }

    IEnumerator PlaySummoningCircleHelper()
    {
        Debug.Log("Playing Gold Summoner");

        LoadAndSpawn(goldSummoner);

        yield return new WaitForSeconds(1.5f);

        if (!canDestroyCircle)
        {
            StartCoroutine("PlaySummoningCircleHelper"); // Recursive function 
        }
    }

    void PlayPurpleCircle()
    {
        Debug.Log("Card Played.. Playing Purple Summoner");

        // Instantiate the PurpleCircleHere
        LoadAndSpawn(purpleSummoner);
        
    }

    void LoadAndSpawn(AssetReference assetReference)
    {
        var op = Addressables.LoadAssetAsync<GameObject>(assetReference);

        op.Completed += (operation) =>
        {
            assetReference.InstantiateAsync(transform.position, Quaternion.identity).Completed +=
                (asyncOperationHandle) =>
                {
                    Debug.Log("Spawned summoner");
                   
                };
        };
    }

    void CanDestroySummoner()
    {
        Debug.Log("Mouse Up, Card can be lifted");

        canDestroyCircle = true;
    }

    void OnDestroy()
    {
        DragCard.CardDragged -= PlaySummoningCircle;
        DragCard.CardReleased -= CanDestroySummoner;
        PlayZone.HasPlayed -= PlayPurpleCircle;
    }
}
