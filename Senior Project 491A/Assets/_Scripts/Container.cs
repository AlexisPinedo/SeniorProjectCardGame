using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Container : MonoBehaviour
{
    public delegate void _cardDrawn(PlayerCardContainer cardDrawn);

    public static event _cardDrawn CardDrawn;

    public PlayerCardContainer container;


    protected virtual void InitialCardDisplay()
    {
        if (CardDrawn != null)
            CardDrawn.Invoke(container);
    }
}
