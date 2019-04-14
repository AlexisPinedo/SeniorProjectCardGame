using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using UnityEngine;

public class Deck : ScriptableObject
{
    //Singleton pattern to ensure only one deck is created globally.
    private static Deck instance = null;  
    void Awake()
    {
    if (instance == null)
      instance = this;
    }
    
    static Stack<Card> deck = new Stack<Card>();
    static Graveyard gradeyard = ScriptableObject.CreateInstance<Graveyard>();

    public Card RevealTopCard()
    {
        return deck.Peek();
    }

    public Card DrawCard()
    {
        return deck.Pop();
    }

    public void AddCard(Card card)
    {
        deck.Push(card);
    }

    public void AddToGraveYard(Card card)
    {
        gradeyard.AddToGrave(card);
    }
    
    public void Shuffle(Stack<Card> deckStack)
    {
        System.Random random = new System.Random();  
        var deckList = new List<Card>(deckStack);
        int n = deckList.Count;  
        while (n > 1) {  
            n--;  
            int k = random.Next(n + 1);  
            Card value = deckList[k];  
            deckList[k] = deckList[n];  
            deckList[n] = value;  
        }  
        deck = new Stack<Card>(deckList);
    }
}
