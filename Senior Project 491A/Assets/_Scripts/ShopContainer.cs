using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopContainer : PlayerCardContainer
{
    public ShopDeck shopDeck;
    //[SerializeField]
    //public PlayerCardHolder playerCardContainer;
    
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
        for (int i = 0; i < shopCardCount; i++)
        {
            if(shopDeck.cardsInDeck.Count <= 0)
            {
                Debug.Log("Shop deck is " + shopDeck.cardsInDeck.Count);
                return;
            }

            PlayerCard cardDrawn = null;
            cardDrawn = (PlayerCard)shopDeck.cardsInDeck.Pop();

            holder.card = cardDrawn;
            PlayerCardHolder cardHolder = Instantiate(holder, containerGrid.freeLocations.Dequeue(), Quaternion.identity, this.transform);
            containerGrid.cardLocationReference.Add(new Vector2(cardHolder.gameObject.transform.position.x, 
                cardHolder.gameObject.transform.position.y), cardHolder);
        }
    }
}
