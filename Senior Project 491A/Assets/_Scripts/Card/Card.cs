using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Card : MonoBehaviour
{
    public string cardName;
    public string cardDescription;
    public string cardEffect;

    public Sprite cardArtwork;
    public Sprite cardElement;

    //Enemy Card components
    public BossTurnCardPlayer manager;
    public CreateGrid bossZones;
}
