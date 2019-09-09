using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Card : ScriptableObject
{
    [SerializeField]
    private string cardName;
    public string CardName
    {
        get { return cardName; }
    }

    [SerializeField]
    private string cardDescription;
    public string CardDescription
    {
        get { return cardDescription; }
    }

    [SerializeField]
    private CardEffect cardEffect;
    public CardEffect CardEffect
    {
        get { return cardEffect; }
    }

    [SerializeField]
    private string cardEffectDisplay;
    public string CardEffectDisplay
    {
        get { return cardEffectDisplay; }
    }

    [SerializeField]
    private Sprite cardArtwork;
    public Sprite CardArtwork
    {
        get { return cardArtwork; }
    }


    [SerializeField]
    private Sprite cardTypeArt;
    public Sprite CardTypeArt
    {
        get { return cardTypeArt; }
    }

    [SerializeField]
    private CardType.CardTypes cardType;
    public CardType.CardTypes CardType
    {
        get { return cardType; }
    }

    [SerializeField]
    private Sprite nameBoxArt;
    public Sprite NameBoxArt
    {
        get { return nameBoxArt; }
    }

    [SerializeField]
    private Sprite cardEffectBoxArt;
    public Sprite CardEffectBoxArt
    {
        get { return cardEffectBoxArt; }
    }

    [SerializeField]
    private Sprite borderArt;
    public Sprite BorderArt
    {
        get { return borderArt; }
    }
}
