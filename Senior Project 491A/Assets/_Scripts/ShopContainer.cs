using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopContainer : Container
{
    public ShopDeck shopDeck;
    [SerializeField]
    //public PlayerCardContainer playerCardContainer;
    
    private int shopCardCount = 6;
    // Start is called before the first frame update
    void Start()
    {
        InitialCardDisplay();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    protected override void InitialCardDisplay()
    {
        if(shopDeck.cardsInDeck.Count <= 0)
        {
            Debug.Log("Shop deck is empty");
            return;
        }

        PlayerCard cardDrawn = null;
        cardDrawn = (PlayerCard)shopDeck.cardsInDeck.Pop();

        container.card = cardDrawn;
        Instantiate(container, this.transform);

        base.InitialCardDisplay();
    }
}
