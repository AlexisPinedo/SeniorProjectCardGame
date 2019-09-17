using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PurchaseHandler : MonoBehaviour
{
    public delegate void _CardBought(Card cardBuying);
    public static event _CardBought CardBought;

    private Player turnPlayer;

    public void Start()
    {
        turnPlayer = TurnManager.Instance.turnPlayer;
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
