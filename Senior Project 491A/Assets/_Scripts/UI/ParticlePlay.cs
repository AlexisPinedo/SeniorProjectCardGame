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

    void CanDestroySummoner()
    {
        canDestroyCircle = true;
    }
}
