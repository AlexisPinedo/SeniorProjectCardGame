using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

//Valor - if you play 3 cards of the same color. you can add up to 2 cards from the shop to your grave

[CreateAssetMenu]
public class Valor : CostRequirementHero
{
    private int cardSelectionLimit = 2;
    

    //Veda - If you play 3 cards of the same color, both players may add 1 card from the shop to the graveyard

    protected override void HeroPowerEffect()
    {
        UIHandler.Instance.EnableNotificationWindow("Select up to 2 cards of the same color");
        
        ShopSelectionEventListener.Instance.EnableShopSelectionState(cardSelectionLimit, History.Instance.PlayerCardHistory.Last().CardType);
        
    }


}
