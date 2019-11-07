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
    
    private Vector3 shopBackgroundOriginalPosition;

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

        shopBackgroundOriginalPosition = shopBackground.transform.position;
    }

    public void MoveShopDown()
    {
        shopBackground.transform.position = new Vector2(shopBackgroundOriginalPosition.x, shopBackgroundOriginalPosition.y);
        shop.transform.position = new Vector3(0, 0, 0);

    }

    public void MoveShopUp()
    {
        shopBackground.transform.position = new Vector2(shopBackgroundOriginalPosition.x, shopBackgroundOriginalPosition.y + 20);
        shop.transform.position = new Vector3(0, 40, 0);
    }
}
