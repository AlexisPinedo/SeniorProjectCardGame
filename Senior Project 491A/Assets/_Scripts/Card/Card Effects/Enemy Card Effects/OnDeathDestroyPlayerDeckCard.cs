using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Card Effect/Minion Card Effect/On Death Destroy PlayerGraveyard Deck Card")]
public class OnDeathDestroyPlayerDeckCard : OnDeathCardEffects
{
    public override void LaunchCardEffect()
    {
        PlayerDeck playerDeck = TurnPlayerManager.Instance.TurnPlayer.deck;
        PlayerGraveyard playerGraveyardGrave = TurnPlayerManager.Instance.TurnPlayer.playerGraveyard;

        if (playerDeck.cardsInDeck.Count != 0)
        {
            playerDeck.cardsInDeck.Pop();
        }
        else
        {
            if (playerGraveyardGrave.graveyard.Count != 0)
            {
                for (int j = 0; j < playerGraveyardGrave.graveyard.Count; j++)
                {
                    playerDeck.cardsInDeck.Push(playerGraveyardGrave.graveyard[j]);
                    playerGraveyardGrave.graveyard.Remove(playerGraveyardGrave.graveyard[j]);
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
