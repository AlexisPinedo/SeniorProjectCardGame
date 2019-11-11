using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Card Effect/Minion Card Effect/On Death Destroy Player Deck Card")]
public class OnDeathDestroyPlayerDeckCard : OnDeathCardEffects
{
    public override void LaunchCardEffect()
    {
        Deck playerDeck = TurnPlayerManager.Instance.TurnPlayer.deck;
        Graveyard playerGrave = TurnPlayerManager.Instance.TurnPlayer.graveyard;

        if (playerDeck.cardsInDeck.Count != 0)
        {
            playerDeck.cardsInDeck.Pop();
        }
        else
        {
            if (playerGrave.graveyard.Count != 0)
            {
                for (int j = 0; j < playerGrave.graveyard.Count; j++)
                {
                    playerDeck.cardsInDeck.Push(playerGrave.graveyard[j]);
                    playerGrave.graveyard.Remove(playerGrave.graveyard[j]);
                }

                playerDeck.cardsInDeck = ShuffleDeck.Shuffle(playerDeck);

                playerDeck.cardsInDeck.Pop();
            }
            else
                NotificationWindowEvent.Instance.EnableNotificationWindow(
                    "deck & grave is empty nothing to destroy");
        }    
    }
}
