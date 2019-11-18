 using System.Collections;
using System.Collections.Generic;
 using System.Xml.Schema;
 using UnityEngine;
 
 /// <summary>
 /// Enemy decks are Prefillable decks
 /// Creating a deck from this class will allow you to add cards into the game
 /// before the game starts
 /// </summary>
//This is used to create an instance of the enemy deck in the project folder
[CreateAssetMenu(menuName = "Deck/EnemyDeck")]
 public class EnemyDeck : PrefillableDeck<MinionCard>
 {
  
 }