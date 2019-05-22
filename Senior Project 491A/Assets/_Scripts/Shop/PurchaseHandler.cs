using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PurchaseHandler : MonoBehaviour
{
    public delegate void _CardBought(Card cardBuying);
    public static event _CardBought CardBought;

    private TurnManager turnManager;
    private Player turnPlayer;

    public void Start()
    {
        turnPlayer = turnManager.turnPlayer;
    }

    public bool isPurchasable(Card cardClicked)
    {
        bool canBePurchased;
        // TODO

        canBePurchased = true;

        return canBePurchased;
    }

    public void PurchaseCard(Card cardBuying)
    {

        CardBought?.Invoke(cardBuying);
    }

}
