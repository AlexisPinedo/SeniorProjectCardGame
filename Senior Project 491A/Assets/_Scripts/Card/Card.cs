using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Defines a Card as a scriptable object and is the base class for all types of Cards to extend. Contains attributes common to all types of Cards.
/// </summary>
public abstract class Card : ScriptableObject
{
    [SerializeField]
    protected string cardName;
    public string CardName
    {
        get { return cardName; }
    }

    [SerializeField]
    protected string cardDescription;
    public string CardDescription
    {
        get { return cardDescription; }
    }

    [SerializeField]
    protected CardEffect cardEffect;
    public CardEffect CardEffect
    {
        get { return cardEffect; }
    }

    [SerializeField]
    protected string cardEffectDisplay;
    public string CardEffectDisplay
    {
        get { return cardEffectDisplay; }
    }

    [SerializeField]
    protected Sprite cardArtwork;
    public Sprite CardArtwork
    {
        get { return cardArtwork; }
    }

    [SerializeField]
    protected Sprite cardTypeArt;
    public Sprite CardTypeArt
    {
        get { return cardTypeArt; }
    }

    [SerializeField]
    protected CardType.CardTypes cardType;
    public CardType.CardTypes CardType
    {
        get { return cardType; }
    }

    [SerializeField]
    protected Sprite nameBoxArt;
    public Sprite NameBoxArt
    {
        get { return nameBoxArt; }
    }

    [SerializeField]
    protected Sprite cardEffectBoxArt;
    public Sprite CardEffectBoxArt
    {
        get { return cardEffectBoxArt; }
    }

    [SerializeField]
    protected Sprite borderArt;
    public Sprite BorderArt
    {
        get { return borderArt; }
    }
}
