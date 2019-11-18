using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using Photon.Pun;
using UnityEngine;

public class ShopDisplayManager : MonoBehaviour
{
    [SerializeField]
    private PurchaseHandler shop;
    [SerializeField]
    private  RectTransform shopBackground;
    
    private Vector2 shopBackgroundOriginalPosition;
    
    private Vector2 backgroundDestinationPosition = new Vector2(0, 400);

    private static ShopDisplayManager _instance;

    public static ShopDisplayManager Instance
    {
        get => _instance;
    }

    private void Awake()
    {
        if (_instance == null && _instance != this)
        {
            _instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }

        shopBackgroundOriginalPosition = shopBackground.anchoredPosition;
    }

    public void MoveShopDown()
    {
        shopBackground.anchoredPosition -= backgroundDestinationPosition;
        shop.transform.position += new Vector3(0, -40, 0);

    }

    public void ResetShopPosition()
    {
        shopBackground.anchoredPosition = new Vector2(0, 0);
        shop.transform.position = new Vector3(0, 0, 0);
    }

    public void MoveShopUp()
    {
        shopBackground.anchoredPosition += backgroundDestinationPosition;
        shop.transform.position += new Vector3(0, 40, 0);
    }
}
