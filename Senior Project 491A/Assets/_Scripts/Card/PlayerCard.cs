using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ExitGames.Client.Photon;

/// <summary>
/// Extends the Card scriptable object and contains information specific to Player Cards.
/// </summary>
[CreateAssetMenu(menuName = "Card/Player Card")]
public class PlayerCard : Card
{
    //All this stuff below belongs in Player Card Class
    //========================================================
    //public bool inShop = true;
    [SerializeField] private int _cardCost;

    public int CardCost
    {
        get { return _cardCost; }
    }

    [SerializeField] private int _cardAttack;

    public int CardAttack
    {
        get { return _cardAttack; }
    }

    [SerializeField] private int _cardCurrency;

    public int CardCurrency
    {
        get { return _cardCurrency; }
    }

    [SerializeField]
    private List<CardType.CardTypes> CardEffectRequirement = new List<CardType.CardTypes>();

    public List<CardType.CardTypes> cardEffectRequirments
    {
        get { return CardEffectRequirement; }
    }

    public List<Sprite> cardCostsIcons = new List<Sprite>();
}


//public class PlayerCard : Card
//{
    //Should this be on Player card???
    //public Vector2 spotOnGrid;

    //When purchasing card from shop call this method from an event trigger
    //public bool PurchaseCard()
    //{
    //    Player player = TurnManager.Instance.turnPlayer;

    //    if (cardCost <= player.GetCurrency())
    //    {
    //        player.SubtractCurrency(cardCost);
    //        inShop = false;

    //        return true;
    //    }
    //    else
    //    {
    //        //Debug.Log("Cannot buy too broke");

    //    }
    //    return false;
    //}
//}
