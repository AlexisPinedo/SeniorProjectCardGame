using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public abstract class CardHolder : MonoBehaviour
{
    [SerializeField]
    protected SpriteRenderer cardArtDisplay;
    [SerializeField]
    protected SpriteRenderer typeIcon;
    [SerializeField]
    protected SpriteRenderer cardBorder;
    [SerializeField]
    protected SpriteRenderer cardEffectTextBox;
    [SerializeField]
    protected SpriteRenderer cardNameTextBox;
    [SerializeField]
    protected TextMeshPro nameText;
    [SerializeField]
    protected TextMeshPro cardEffectText;

    protected virtual void Awake()
    {
        LoadCardIntoContainer();
    }
    
    protected virtual void OnDestroy()
    {
        ClearCardFromContainer();
    }

    protected virtual void OnEnable()
    {
        //Debug.Log("card had been enabled ");
        LoadCardIntoContainer();
    }

    protected virtual void OnDisable()
    {
        //Debug.Log("card had been enabled ");
        ClearCardFromContainer();
    }
    
    protected virtual void LoadCardIntoContainer()
    {

    }
    
    
    protected virtual void ClearCardFromContainer()
    {

    }
    

}
