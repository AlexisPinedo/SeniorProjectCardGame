using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ShopAnimationManager : AnimationManagementBase<TransformAnimationObject>
{
    private static ShopAnimationManager _instance;

    public static ShopAnimationManager SharedInstance
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

    public void PlayAnimation(PlayerCardDisplay cardDisplay, Vector3 destination, float lerpTime, bool canScale = false, bool storeOriginalPosition = false, bool shouldDestroy = false, bool stallOtherAnimations = true)
    {
        TransformAnimationObject animObject = new TransformAnimationObject(cardDisplay, destination, lerpTime, canScale, storeOriginalPosition, shouldDestroy);

        if(stallOtherAnimations)
            AddObjectToQueue(animObject);
        else
        {
            StartCoroutine(TransformCardPosition(cardDisplay, destination, lerpTime, 
                canScale, storeOriginalPosition, shouldDestroy, false));
        }

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

    IEnumerator TransformCardPosition(PlayerCardDisplay cardDisplay, Vector3 destination, float lerpTime, bool canScale = false, bool storeOriginalPosition = false, bool shouldDestroy = false, bool isAnimationQueued = true)
    {
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


        BoxCollider2D touch = cardDisplay.GetComponent<BoxCollider2D>();
        touch.enabled = false;

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


        if (isAnimationQueued)
        {
            if (shouldDestroy)
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
        else
        {
            Destroy(cardDisplay.gameObject);
        }
        
    }
}
