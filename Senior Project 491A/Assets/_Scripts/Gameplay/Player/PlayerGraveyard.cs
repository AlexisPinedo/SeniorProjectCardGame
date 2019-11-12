using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Player Component/Grave")]
public class PlayerGraveyard : ScriptableObject 
{
    public List<PlayerCard> graveyard = new List<PlayerCard>();
}