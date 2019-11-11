using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Card Effect/Minion Card Effect/Destroy Player Card")]
public class OnDeathPlayPlayerCardEffect : OnDeathCardEffects
{
    public override void LaunchCardEffect()
    {
        Deck playerDeck = TurnPlayerManager.Instance.TurnPlayer.deck;
        Graveyard playerGrave = TurnPlayerManager.Instance.TurnPlayer.graveyard;

        if (playerDeck.cardsInDeck.Count != 0)
        {
            PlayerCard playerCard = (PlayerCard)playerDeck.cardsInDeck.Pop();
            TurnPlayerManager.Instance.TurnPlayer.Power += playerCard.CardAttack;
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

                PlayerCard playerCard = (PlayerCard)playerDeck.cardsInDeck.Pop();
                TurnPlayerManager.Instance.TurnPlayer.Power += playerCard.CardAttack;
            }
            else
                NotificationWindowEvent.Instance.EnableNotificationWindow(
                    "deck & grave is empty nothing to destroy");
        }    
    }
}
