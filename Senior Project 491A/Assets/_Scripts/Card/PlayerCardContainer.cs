using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// An abstract class that defines a Container specific for PlayerCards.
/// containers act as a organized location for all card displays instantiated into the game
/// </summary>
public abstract class PlayerCardContainer : Container
{
    //The player card containers need a reference to the player card display to load in the player card components
    public PlayerCardDisplay display;
    public GameObject spawnPostion;

    //protected IEnumerator TransformCardPosition(PlayerCardDisplay cardDisplay, Vector3 cardDestination)
    //{
    //    float currentLerpTime = 0;
    //    float lerpTime = 0.5f;

    //    Vector3 startPos = cardDisplay.transform.position;

    //    while (cardDisplay.transform.position != cardDestination)
    //    {
    //        currentLerpTime += Time.deltaTime;
    //        if (currentLerpTime >= lerpTime)
    //        {
    //            currentLerpTime = lerpTime;
    //        }

    //        float Perc = currentLerpTime / lerpTime;

    //        cardDisplay.transform.position = Vector3.Lerp(startPos, cardDestination, Perc);
            

    //        yield return new WaitForEndOfFrame();
    //    }

    //    cardDisplay.GetComponent<CardZoomer>().OriginalPosition = cardDestination;
    //    cardDisplay.GetComponent<DragCard>().OriginalPosition = cardDestination;
    //}




}
