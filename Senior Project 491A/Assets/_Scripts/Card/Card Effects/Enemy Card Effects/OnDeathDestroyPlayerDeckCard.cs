using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class OnDeathDestroyPlayerDeckCard : OnDeathCardEffects
{
    public override void LaunchCardEffect()
    {
        Deck playerDeck = TurnManager.Instance.turnPlayer.deck;

        if (playerDeck.cardsInDeck.Count != 0)
        {
            Destroy(playerDeck.cardsInDeck.Pop());
        }
        else
        {
            NotificationWindowEvent.Instance.EnableNotificationWindow("deck is empty nothing to destroy");
        }
    }
}
