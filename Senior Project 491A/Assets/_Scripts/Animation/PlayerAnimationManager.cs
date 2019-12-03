using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerAnimationManager : AnimationManagementBase<TransformAnimationObject>
{
    private static PlayerAnimationManager _instance;

    public static PlayerAnimationManager SharedInstance
    {
        get => _instance;
    }

    

    void Awake()
    {
        if (_instance == null && _instance != this)
            _instance = this;
        else
        {
            Destroy(this.gameObject);
        }
    }


    //TODO: Add a queue that allows only one animation to occur at a time 
    public void PlayAnimation(PlayerCardDisplay cardDisplay, Vector3 destination, float lerpTime, bool canScale = false, bool storeOriginalPosition = false, bool shouldDestroy = false)
    {
        

        TransformAnimationObject animObject = new TransformAnimationObject(cardDisplay, destination, lerpTime, canScale, storeOriginalPosition, shouldDestroy);

        AddObjectToQueue(animObject);

    }

    //void AddObjectToQueue(TransformAnimationObject animObject)
    //{
    //    if (animQueue.Count == 0)
    //    {
    //        animQueue.Enqueue(animObject);
    //        Debug.Log("Starting addition of objects");
    //        StartCoroutine(HandleAnim());
    //    }
    //    else
    //    {
    //        animQueue.Enqueue(animObject);
    //    }
    //}


    public override IEnumerator HandleAnim()
    {
        TransformAnimationObject nextAnim = animQueue.Peek();

        animationsCompleted = false;

        
        //run animation logic
        StartCoroutine(TransformCardPosition(nextAnim.cardDisplay, nextAnim.destination, nextAnim.lerpTime,
            nextAnim.canScale, nextAnim.storeOriginalPosition, nextAnim.shouldDestroy));

        while (!animationsCompleted)
        {
            yield return null;
        }

        animQueue.Dequeue();

        if (animQueue.Count > 0)
        {
            StartCoroutine(HandleAnim());
        }
    }

    IEnumerator TransformCardPosition(PlayerCardDisplay cardDisplay, Vector3 destination, float lerpTime, bool canScale = false, bool storeOriginalPosition = false, bool shouldDestroy = false)
    {
        BoxCollider2D touch = cardDisplay.gameObject.GetComponent<BoxCollider2D>();
        touch.enabled = false;

        if (canScale)
        {
            
            var renderers = cardDisplay.GetComponentsInChildren<Renderer>();
            //for (int i = 0; i < renderers.Length; i++)
            //{
            //    renderers[i].enabled = false;
            //}
            renderers.ToList().ForEach(x => x.enabled = false);
            //cardDisplay.transform.localScale -= new Vector3(cardDisplay.transform.localScale.x, cardDisplay.transform.localScale.y, cardDisplay.transform.localScale.z);
            //Debug.Log("Card Display Shrank: " + cardDisplay.transform.localScale);
        }
        

        float currentLerpTime = 0.0f;

        Vector3 startPos = cardDisplay.transform.position;

        while (cardDisplay.transform.position != destination)
        {
            currentLerpTime += Time.deltaTime;
            if (currentLerpTime >= lerpTime)
            {
                currentLerpTime = lerpTime;
            }

            float Perc = currentLerpTime / lerpTime;

            cardDisplay.transform.position = Vector3.Lerp(startPos, destination, Perc);

            yield return new WaitForEndOfFrame();
        }

        if (storeOriginalPosition)
        {
            cardDisplay.GetComponent<CardZoomer>().originalPosition = destination;
            cardDisplay.GetComponent<DragCard>().OriginalPosition = destination;
        }


        if (canScale && shouldDestroy)
        {
            //StartCoroutine(ScaleCardSize(cardDisplay, true));
            ScaleAnimationObject scaledObject = new ScaleAnimationObject(cardDisplay, shouldDestroy: true);
            PlayZoneAnimationManager.SharedInstance.PlayAnimation(scaledObject);
            EndAnimEvent();
        }
        else if (canScale && !shouldDestroy)
        {
            //StartCoroutine(ScaleCardSize(cardDisplay, cardTouch: touch));
            ScaleAnimationObject scaledObject = new ScaleAnimationObject(cardDisplay, cardTouch: touch);
            //AddScaleObjectToQueue(scaledObject);
            EndAnimEvent();
        }
        else if (!canScale && shouldDestroy)
        {
            EndAnimEvent();
            Destroy(cardDisplay.gameObject);
        }
        else
        {
            touch.enabled = true;
            EndAnimEvent();
        }
    }
}
