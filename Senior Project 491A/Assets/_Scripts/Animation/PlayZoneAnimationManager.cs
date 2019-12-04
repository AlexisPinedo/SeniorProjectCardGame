using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayZoneAnimationManager : AnimationManagementBase<ScaleAnimationObject>
{
    private static PlayZoneAnimationManager _instance;

    public static PlayZoneAnimationManager SharedInstance
    {
        get => _instance;
    }

    private List<GameObject> cardsToDestroy = new List<GameObject>();

    void Awake()
    {
        if (_instance == null && _instance != this)
            _instance = this;
        else
        {
            Destroy(this.gameObject);
        }
    }

    public void PlayAnimation(ScaleAnimationObject animObject)
    {
        //Debug.Log("Object Added To Queue");
        AddObjectToQueue(animObject);
        //Debug.Log("ANIM QUEUE COUNT: " + animQueue.Count);
    }

    //void AddObjectToQueue(ScaleAnimationObject animObject)
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
        ScaleAnimationObject nextAnim = animQueue.Peek();

        animationsCompleted = false;
        //run animation logic
        StartCoroutine(ScaleCardSize(nextAnim.cardDisplay, nextAnim.shouldDestroy, nextAnim.cardTouch));

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

    IEnumerator ScaleCardSize(PlayerCardDisplay cardDisplay, bool shouldDestroy, Collider2D cardTouch)
    {
        BoxCollider2D touch = cardDisplay.gameObject.GetComponent<BoxCollider2D>();
        touch.enabled = false;

        var renderers = cardDisplay.GetComponentsInChildren<Renderer>();
        renderers.ToList().ForEach(x => x.enabled = true);

        float currentLerpTime = 0.0f;
        float lerpTime = 0.2f;

        Vector3 startSize = cardDisplay.transform.localScale;
        //Debug.Log("START SIZE: " + startSize);
        Vector3 targetSize = new Vector3(1.5f, 1.5f, 1.5f);

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

        yield return new WaitForSeconds(1.0f);

        if (!shouldDestroy)
        {
            cardTouch.enabled = true;
        }
        else
        {
            //Destroy(cardDisplay.gameObject);
            cardsToDestroy.Add(cardDisplay.gameObject);
            cardDisplay.transform.position = new Vector2(-300, 0);
            //cardDisplay.gameObject.SetActive(false);
        }

        EndAnimEvent();
    }

    public void DestroyCards()
    {
        cardsToDestroy.ForEach(x => Destroy(x));
        cardsToDestroy.Clear();
    }

  

}



