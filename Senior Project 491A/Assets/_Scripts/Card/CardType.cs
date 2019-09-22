using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class CardType 
{
    //This Enum has a reference for which attribute the card is. 
    public enum CardTypes
    {
        MythicalCreature = 0,
        Entertainer = 1,
        Magic = 2,
        Nature = 3,
        Warrior = 4,
        All = 5,
        Enemy = 6,
        None = 7
    }
}
