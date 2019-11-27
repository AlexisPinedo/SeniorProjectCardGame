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

    [SerializeField] private RectTransform playZoneBackground;

    private Vector2 shopBackgroundOriginalPosition;
    
    private Vector2 shopBackgroundDestinationPosition = new Vector2(0, 400);
    
    private Vector2 playZoneBackgroundDestinationPosition = new Vector2(0, 800);

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

    }

    public void MoveShopDown()
    {
        shopBackground.anchoredPosition -= shopBackgroundDestinationPosition;
        shop.transform.position += new Vector3(0, -40, 0);
        MoveTableUp();
    }

    public void MoveTableDown()
    {
        playZoneBackground.anchoredPosition -= playZoneBackgroundDestinationPosition;
    }

    public void ResetShopPosition()
    {
        shopBackground.anchoredPosition = new Vector2(0, 0);
        shop.transform.position = new Vector3(0, 0, 0);
    }

    public void MoveShopUp()
    {
        shopBackground.anchoredPosition += shopBackgroundDestinationPosition;
        shop.transform.position += new Vector3(0, 40, 0);
        MoveTableDown();
    }
    public void MoveTableUp()
    {
        playZoneBackground.anchoredPosition += playZoneBackgroundDestinationPosition;
    }

}
