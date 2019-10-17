using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticlePlay : MonoBehaviour
{
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
        yield return new WaitForSeconds(1.5f);

        if (!canDestroyCircle)
        {
            StartCoroutine("PlaySummoningCircleHelper"); // Recursive function 
        }
    }

    void PlayPurpleCircle()
    {
        // Instantiate the PurpleCircleHere
    }

    void CanDestroySummoner()
    {
        canDestroyCircle = true;
    }

    void OnDestroy()
    {
        DragCard.CardDragged -= PlaySummoningCircle;
        DragCard.CardReleased -= CanDestroySummoner;
        PlayZone.HasPlayed -= PlayPurpleCircle;
    }
}
