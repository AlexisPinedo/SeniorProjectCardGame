using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Deck : MonoBehaviour
{
    //Singleton pattern to ensure only one deck is created globally.
    // private Deck instance = null;

    public delegate void _cardDrawn(Card aCard);
    public static event _cardDrawn CardDrawn;

    [SerializeField]
    private Stack<Card> cardsInDeck = new Stack<Card>();

    // DEBUG: Change later to grab cards from a single-source?
    [SerializeField]
    private Card gameCard;

    [SerializeField]
    private int cardCopies;

    public List<Card> testCards = new List<Card>(10);

    /* Reference to the Player whose Deck this is */
    [SerializeField]
    private GameObject playerObj;
    
    // Reference for the player's graveyard
    private Graveyard playersGraveyard;

    void Awake()
    {
        // Reference player's components
        //SplayersGraveyard = playerObj.GetComponentInChildren<Graveyard>();

        fillDeck();
    }

    /// TODO
    // Temp fix until we get more cards in the system
    private void fillDeck()
    {
        foreach (var card in testCards)
        {
            for (int i = 0; i < cardCopies; i++)
            {
                Card copy = card;
                //copy.transform.parent = this.transform;
                AddCard((Card)copy);
            }
        }
        Shuffle();
    }

    public Stack<Card> getDeck()
    {
        return this.cardsInDeck;
    }

    public Card RevealTopCard()
    {
        return cardsInDeck.Peek();
    }

    public Card DrawCard()
    {
        Card cardPopped = cardsInDeck.Pop();
        if (CardDrawn != null)
        {
            CardDrawn(cardPopped);
        }
        return cardPopped;

    }

    public void AddCard(Card card)
    {
        Debug.Log("Card: " + card.cardName + " has been pushed onto the stack");
        cardsInDeck.Push(card);
    }

    public void AddToGraveYard(Card card)
    {
        playersGraveyard.addToGrave(card);
    }

    public void Shuffle()
    {
        System.Random random = new System.Random();
        var deckList = cardsInDeck.ToArray();
        int n = deckList.Length;
        while (n > 1)
        {
            n--;
            int k = random.Next(n + 1);
            Card value = deckList[k];
            deckList[k] = deckList[n];
            deckList[n] = value;
        }
        cardsInDeck = new Stack<Card>(deckList);
    }

}


//// This class is inside the TestClass so it could access its private fields
//// this custom editor will show up on any object with TestScript attached to it
//// you don't need (and can't) attach this class to a gameobject
//[CustomEditor(typeof(Deck))]
//public class StackPreview : Editor
//{
//    public new void OnInspectorGUI()
//    {

//        // get the target script as TestScript and get the stack from it
//        var ts = (Deck)target;
//        var stack = ts.getDeck();

//        // some styling for the header, this is optional
//        var bold = new GUIStyle();
//        bold.fontStyle = FontStyle.Bold;
//        GUILayout.Label("Items in my stack", bold);

//        // add a label for each item, you can add more properties
//        // you can even access components inside each item and display them
//        // for example if every item had a sprite we could easily show it 
//        foreach (var item in stack)
//        {
//            GUILayout.Label(item.name);
//        }
//    }
//}

