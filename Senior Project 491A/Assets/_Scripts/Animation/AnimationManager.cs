using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AnimationManager : MonoBehaviour
{
    private static AnimationManager _instance;

    public static AnimationManager SharedInstance
    {
        get => _instance;
    }
    
    Queue<AnimationObject> animQueue = new Queue<AnimationObject>();

    private bool animationActive;

    public bool AnimationActive
    {
        get => animationActive;
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

    #region QEUEING SYSTEM
    //TODO: Add a queue that allows only one animation to occur at a time 
    public void PlayAnimation(PlayerCardDisplay cardDisplay, Vector3 destination, float lerpTime, bool canScale = false, bool storeOriginalPosition = false, bool shouldDestroy = false)
    {
        AnimationObject animObject = new AnimationObject(cardDisplay, destination, lerpTime, canScale, storeOriginalPosition, shouldDestroy);

        AddObjectToQueue(animObject);
    }

    void AddObjectToQueue(AnimationObject animObject)
    {
        if (animQueue.Count == 0)
        {
            animQueue.Enqueue(animObject);
            StartCoroutine(HandleAnim());
        }
        else
        {
            animQueue.Enqueue(animObject);
        }
    }

    void EndEvent()
    {
        animationActive = false;
    }


    IEnumerator HandleAnim()
    {
        AnimationObject nextAnim = animQueue.Peek();

        animationActive = true;

        StartCoroutine(TransformCardPosition(
            nextAnim.cardDisplay, nextAnim.destination, nextAnim.lerpTime,
            nextAnim.canScale, nextAnim.storeOriginalPosition, 
            nextAnim.shouldDestroy
            ));

        while (animationActive)
        {
            yield return null;
        }

        animQueue.Dequeue();

        if (animQueue.Count > 0)
        {
            StartCoroutine(HandleAnim());
        }

    }

    #endregion

    public IEnumerator TransformCardPosition(PlayerCardDisplay cardDisplay, Vector3 destination, float lerpTime, bool canScale, bool storeOriginalPosition, bool shouldDestroy)
    {
        if (canScale)
        {
            cardDisplay.transform.localScale -= new Vector3(cardDisplay.transform.localScale.x, cardDisplay.transform.localScale.y, cardDisplay.transform.localScale.z);
            Debug.Log("Card Display Shrank: " + cardDisplay.transform.localScale);
        }
            

        BoxCollider2D touch = cardDisplay.GetComponent<BoxCollider2D>();
        touch.enabled = false;

        //Vector3 destination = GameObject.Find(destinationName).transform.position;

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
            cardDisplay.GetComponent<CardZoomer>().OriginalPosition = destination;
            cardDisplay.GetComponent<DragCard>().OriginalPosition = destination;
        }

        if (canScale && shouldDestroy)
            StartCoroutine(ScaleCardSize(cardDisplay, true));
        else if (canScale && !shouldDestroy)
            StartCoroutine(ScaleCardSize(cardDisplay, cardTouch: touch));
        else if (!canScale && shouldDestroy)
        {
            EndEvent();
            Destroy(cardDisplay.gameObject);
        }
        else
        {
            touch.enabled = true;
            EndEvent();
        }


    }

    // This is called through a boolean 
    IEnumerator ScaleCardSize(PlayerCardDisplay cardDisplay, bool shouldDestroy = false, Collider2D cardTouch = null)
    {
       

        float currentLerpTime = 0.0f;
        float lerpTime = 0.2f;

        Vector3 startSize = cardDisplay.transform.localScale;
        Vector3 targetSize = cardDisplay.transform.localScale + new Vector3(.5f, .5f, .5f);

        while (cardDisplay.transform.localScale != targetSize)
        {
            currentLerpTime += Time.deltaTime;
            if (currentLerpTime >= lerpTime)
            {
                currentLerpTime = lerpTime;
            }

            float Perc = currentLerpTime / lerpTime;

            cardDisplay.transform.localScale = Vector3.Lerp(startSize, targetSize, Perc);

            yield return new WaitForEndOfFrame();
        }

        yield return new WaitForSeconds(1.5f);

        if (!shouldDestroy)
        {
            cardTouch.enabled = true;
        }
        else
        {
            Destroy(cardDisplay.gameObject);
        }

        EndEvent();
    }

}

class AnimationObject
{
    public PlayerCardDisplay cardDisplay;
    public Vector3 destination;
    public float lerpTime;
    public bool canScale;
    public bool storeOriginalPosition;
    public bool shouldDestroy;

    public AnimationObject(PlayerCardDisplay cardDisplay, Vector3 destination, float lerpTime, bool canScale = false, bool storeOriginalPosition = false, bool shouldDestroy = false)
    {
        this.cardDisplay = cardDisplay;
        this.destination = destination;
        this.lerpTime = lerpTime;
        this.canScale = canScale;
        this.storeOriginalPosition = storeOriginalPosition;
        this.shouldDestroy = shouldDestroy;
    }
}
