using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinionCardEffectHandler : MonoBehaviour
{
    private MinionCardDisplay minionCardDisplay;
    private EnemyCard minionCard;

    private void Awake()
    {
        minionCardDisplay = GetComponent<MinionCardDisplay>();
        minionCard = minionCardDisplay.card;
    }

    private void OnEnable()
    {
//        Debug.Log("minion created validating onplay effect");
        if (minionCard.CardEffect is OnPlayEffects || minionCard.CardEffect is PassiveEffect)
        {
            //Debug.Log("is an immediate effect to launch");

            minionCard.CardEffect.LaunchCardEffect();
        }
    }

    private void OnDisable()
    {
        if (minionCard.CardEffect is OnDeathCardEffects)
        {
            minionCard.CardEffect.LaunchCardEffect();
        }
    }
}
