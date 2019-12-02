using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "Audio Clip Container")]
public class AudioClipContainer : ScriptableObject
{

    
    [SerializeField] private AudioClip _shopBuyingClip;
    [SerializeField] private AudioClip _startGameClip;
    [SerializeField] private AudioClip _endGameClip;

    public AudioClip ShopBuyingClip
    {
        get { return _shopBuyingClip; }
    }

    public AudioClip StartGameClip
    {
        get { return _startGameClip; }
    }
    
    public AudioClip EndGameClip
    {
        get { return _endGameClip; }
    }
}
