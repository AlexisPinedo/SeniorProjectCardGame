﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardLog : MonoBehaviour
{
    [SerializeField]
    private List<Card> cardHistory = new List<Card>();

    public Card testCard; 

    private void Start()
    {
        cardHistory.Add(testCard);
    }

}