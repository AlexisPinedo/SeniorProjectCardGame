using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HideShopAnimation : MonoBehaviour
{
    [SerializeField] private GameObject shopTable;
    [SerializeField] private GameObject shopBackground;
    [SerializeField] private GameObject shopGrid;
    [SerializeField] private float lerpTime;
    [SerializeField] private Transform shopTableDestination;
    [SerializeField] private Transform shopBackgroundDestination;
    [SerializeField] private Transform shopGridDestination;

    private int animationsRunning = 0;
    private bool firstTime = true;
    private bool inOriginalPosition = true;

    private static bool isCompleted = true;
    public static bool IsCompleted
    {
        get => IsCompleted;
    }

    private readonly Vector3 shopTableOriginalPosition, shopBackgroundOriginalPosition, shopGridOriginalPosition;

    

    HideShopAnimation(Vector3 shopTableOriginalPosition, Vector3 shopBackgroundOriginalPosition,
        Vector3 shopGridOriginalPosition)
    {
        this.shopTableOriginalPosition = shopTableOriginalPosition;
        this.shopBackgroundOriginalPosition = shopBackgroundOriginalPosition;
        this.shopGridOriginalPosition = shopGridOriginalPosition;
    }

    void Awake()
    {
        HideShopAnimation hideShopAnimation = new HideShopAnimation
            (shopTable.transform.position, shopBackground.transform.position, shopGrid.transform.position);

        //UIHandler.EndTurnClicked += PlayAnimation;
        //UIHandler.StartBattleClicked += PlayAnimation;
        //TurnPhaseManager.PrePlayerPhase += MoveToOriginalPosition;
    }

    void MoveToOriginalPosition()
    {
        StartCoroutine(TransformFieldPosition(shopBackground.transform, shopBackgroundOriginalPosition));
        StartCoroutine(TransformFieldPosition(shopTable.transform, shopTableOriginalPosition));
        StartCoroutine(TransformFieldPosition(shopGrid.transform, shopGridOriginalPosition));

        inOriginalPosition = true;
    }

    void PlayAnimation()
    {
        animationsRunning = 3;
        isCompleted = false;

        if (!inOriginalPosition)
        {
            MoveToOriginalPosition();
        }
        else
        {
            StartCoroutine(TransformFieldPosition(shopBackground.transform, shopBackgroundDestination.localPosition));
            StartCoroutine(TransformFieldPosition(shopTable.transform, shopTableDestination.localPosition));
            StartCoroutine(TransformFieldPosition(shopGrid.transform, shopGridDestination.localPosition));
        }

        inOriginalPosition = !inOriginalPosition;
    }

    public void SetReadOnly()
    {
        if (firstTime)
        {
            firstTime = false;
            HideShopAnimation hideShopAnimation = new HideShopAnimation
                (shopTable.transform.position, shopBackground.transform.position, shopGrid.transform.position);
        }
    }

    IEnumerator TransformFieldPosition(Transform movingComponent, Vector3 destination)
    {
        float currentLerpTime = 0.0f;

        Vector3 startPos = movingComponent.localPosition;

        while (movingComponent.position != destination)
        {
            currentLerpTime += Time.deltaTime;
            if (currentLerpTime >= lerpTime)
            {
                currentLerpTime = lerpTime;
            }

            float Perc = currentLerpTime / lerpTime;

            movingComponent.position = Vector3.Lerp(startPos, destination, Perc);

            yield return new WaitForEndOfFrame();
        }

        animationsRunning--;

        if (animationsRunning == 0)
            isCompleted = true;
    }

    void OnDestroy()
    {
        firstTime = true;
        UIHandler.EndTurnClicked -= PlayAnimation;
        UIHandler.StartBattleClicked -= PlayAnimation;
    }
}
