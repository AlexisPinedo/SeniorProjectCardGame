using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using ExitGames.Client.Photon;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System;

/// <summary>
/// Holds visual information specific to PlayerCards. Extends CardDisplay.
/// Class will load in text, sprites, card art, and values into the card display
/// this loads depending on the card attached to it. 
/// </summary>
//[ExecuteInEditMode]
[Serializable]
public class PlayerCardDisplay : CardDisplay
{
    public PlayerCard card;
    [SerializeField] private TextMeshPro attackText;
    [SerializeField] private TextMeshPro costText;
    [SerializeField] private TextMeshPro currencyText;
    [SerializeField] private SpriteRenderer cardEffectCostsIcons;
    [SerializeField] private SpriteRenderer costIcon;
    [SerializeField] private SpriteRenderer powerIcon;
    [SerializeField] private SpriteRenderer currencyIcon;
    [SerializeField] private List<GameObject> cardIcons = new List<GameObject>();

    public delegate void _CardPurchased(PlayerCardDisplay cardBought);

    public static event _CardPurchased CardPurchased;

    //private static List<int> photonViewIDs = new List<int>();
    
    public void OnEnable()
    {
        base.OnEnable();
        LoadCardIntoDisplay();
        DragCard.CardDragged += DisableNonSelectedCollider;
        DragCard.CardReleased += EnableBoxCollider;
    }

    public void OnDisable()
    {
        base.OnEnable();
        ClearCardFromDisplay();
        DragCard.CardDragged -= DisableNonSelectedCollider;
        DragCard.CardReleased -= EnableBoxCollider;
    }

    //THis method will load the display based on the information stored within the card
    protected override void LoadCardIntoDisplay()
    {
        cardArtDisplay.sprite = card.CardArtwork;
        typeIcon.sprite = card.CardTypeArt;
        cardBorder.sprite = card.BorderArt;
        cardEffectTextBox.sprite = card.CardEffectBoxArt;
        cardNameTextBox.sprite = card.NameBoxArt;
        nameText.text = card.CardName;
        cardEffectText.text = card.CardEffectDisplay;
        attackText.text = card.CardAttack.ToString();
        
        currencyText.text = card.CardCurrency.ToString();

        if (gameObject.GetComponentInParent<HandContainer>() == null)
        {
            costIcon.enabled = true;
            costText.text = card.CardCost.ToString();
        }
        
        LoadCostEffectIcons();
        
    }

    /// <summary>
    /// Removes all references to the Card's information from this display.
    /// </summary>
    protected override void ClearCardFromDisplay()
    {
        card = null;
        cardArtDisplay.sprite = null;
        typeIcon.sprite = null;
        cardBorder.sprite = null;
        cardEffectTextBox.sprite = null;
        cardNameTextBox.sprite = null;
        nameText.text = null;
        cardEffectText.text = null;
        attackText.text = null;
        costText.text = null;
        currencyText.text = null;

        RemoveCardEffectCostIcons();


        costIcon.enabled = false;
        costText.enabled = false;

    }

    private void OnMouseEnter()
    {
        SetSelectedCardLayer();

        if (!PhotonNetwork.OfflineMode)
        {
            if (photonView.IsMine)
            {
                photonView.RPC("ChangeCardLayer", RpcTarget.Others, "enter");
            }
        }
    }

    private void OnMouseExit()
    {
        SetCardLayerBack();

        if (!PhotonNetwork.OfflineMode)
        {
            if (photonView.IsMine)
            {
                photonView.RPC("ChangeCardLayer", RpcTarget.Others, "exit");
            }
        }
    }

    /// <summary>
    /// RPC call for OnMouseEnter() and OnMouseExit().
    /// </summary>
    /// <param name="changeType">Parameter to enter or exit the layer</param>
    [PunRPC]
    private void ChangeCardLayer(string changeType)
    {
        if (changeType.Equals("enter"))
        {
            SetSelectedCardLayer();
            return;
        }

        if (changeType.Equals("exit"))
        {
            SetCardLayerBack();
            return;
        }
    }

    /// <summary>
    /// this method will look at sprite list of required costs icons and place them appropriately into the scene
    /// 
    /// </summary>
    private void LoadCostEffectIcons()
    {
        //Debug.Log("Loading cost effect icons for " + card.CardName);
        // Initial spawn point that changes with each card added
        Vector3 spawnPoint = new Vector3(-.55f, .23f, 0f);

        foreach (var cardIconSprite in card.cardCostsIcons)
        {
            SpriteRenderer cardIcon = Instantiate(cardEffectCostsIcons, cardEffectTextBox.transform.position, Quaternion.identity, cardEffectTextBox.transform);
            cardIcon.sortingLayerName = "Player Card";
            cardIcons.Add(cardIcon.gameObject);
            cardIcon.transform.position += spawnPoint;
            spawnPoint += new Vector3(.10f, 0f, 0f);
            cardIcon.sprite = cardIconSprite;
        }
    }

    /// <summary>
    /// This method will remove the icons from the display
    /// </summary>
    private void RemoveCardEffectCostIcons()
    {
        // Debug.Log(cardIcons.Count);
        for (int i = 0; i < cardIcons.Count; i++)
        {
            DestroyImmediate(cardIcons[i].gameObject);
            // Debug.Log("Object Destroyed " + i);
        }

        cardIcons.Clear();
    }

    public void TriggerCardPurchasedEvent()
    {
        CardPurchased?.Invoke(this);
    }

    private void DisableNonSelectedCollider(PlayerCardDisplay selectedCard)
    {
        if (selectedCard != this)
        {
            DisableBoxCollider();
        }
    }

    private void SetSelectedCardLayer()
    {
        if (transform.parent.gameObject.GetComponent<HandContainer>() == null)
        {
            cardArtDisplay.sortingLayerName = "Selected Player Card";
            typeIcon.sortingLayerName = "Selected Player Card";
            attackText.GetComponent<MeshRenderer>().sortingLayerName = "Selected Player Card";
            powerIcon.sortingLayerName = "Selected Player Card";
            currencyIcon.sortingLayerName = "Selected Player Card";
            cardEffectCostsIcons.sortingLayerName = "Selected Player Card";
            costIcon.sortingLayerName = "Selected Player Card";
            typeIcon.sortingLayerName = "Selected Player Card";
            cardBorder.sortingLayerName = "Selected Player Card";
            cardEffectTextBox.sortingLayerName = "Selected Player Card";
            foreach (var spriteRenderer in cardEffectTextBox.gameObject.GetComponentsInChildren<SpriteRenderer>())
            {
                spriteRenderer.sortingLayerName = "Selected Player Card";
            }
        
            cardNameTextBox.sortingLayerName = "Selected Player Card";
            cardArtDisplay.sortingLayerName = "Selected Player Card";
            attackText.GetComponent<MeshRenderer>().sortingLayerName = "Selected Player Card";
            costText.GetComponent<MeshRenderer>().sortingLayerName = "Selected Player Card";
            currencyText.GetComponent<MeshRenderer>().sortingLayerName = "Selected Player Card";
            nameText.GetComponent<MeshRenderer>().sortingLayerName = "Selected Player Card";
            cardEffectText.GetComponent<MeshRenderer>().sortingLayerName = "Selected Player Card";
        }
        
        else
        {
            cardArtDisplay.sortingLayerName = "Selected Player Card";
            typeIcon.sortingLayerName = "Selected Player Card";
            attackText.GetComponent<MeshRenderer>().sortingLayerName = "Selected Player Card";
            powerIcon.sortingLayerName = "Selected Player Card";
            currencyIcon.sortingLayerName = "Selected Player Card";
            cardEffectCostsIcons.sortingLayerName = "Selected Player Card";
            typeIcon.sortingLayerName = "Selected Player Card";
            cardBorder.sortingLayerName = "Selected Player Card";
            cardEffectTextBox.sortingLayerName = "Selected Player Card";
            foreach (var spriteRenderer in cardEffectTextBox.gameObject.GetComponentsInChildren<SpriteRenderer>())
            {
                spriteRenderer.sortingLayerName = "Selected Player Card";
            }
        
            cardNameTextBox.sortingLayerName = "Selected Player Card";
            cardArtDisplay.sortingLayerName = "Selected Player Card";
            attackText.GetComponent<MeshRenderer>().sortingLayerName = "Selected Player Card";
            currencyText.GetComponent<MeshRenderer>().sortingLayerName = "Selected Player Card";
            nameText.GetComponent<MeshRenderer>().sortingLayerName = "Selected Player Card";
            cardEffectText.GetComponent<MeshRenderer>().sortingLayerName = "Selected Player Card";
        }
    }

    private void SetCardLayerBack()
    {
        if (transform.parent.gameObject.GetComponent<HandContainer>() == null)
        {
            cardArtDisplay.sortingLayerName = "Player Card";
            typeIcon.sortingLayerName = "Player Card";
            attackText.GetComponent<MeshRenderer>().sortingLayerName = "Selected Player Card";
            powerIcon.sortingLayerName = "Player Card";
            currencyIcon.sortingLayerName = "Player Card";
            cardEffectCostsIcons.sortingLayerName = "Player Card";
            costIcon.sortingLayerName = "Player Card";
            typeIcon.sortingLayerName = "Player Card";
            cardBorder.sortingLayerName = "Player Card";
            cardEffectTextBox.sortingLayerName = "Player Card";
            foreach (var spriteRenderer in cardEffectTextBox.gameObject.GetComponentsInChildren<SpriteRenderer>())
            {
                spriteRenderer.sortingLayerName = "Player Card";
            }
            cardNameTextBox.sortingLayerName = "Player Card";
            cardArtDisplay.sortingLayerName = "Player Card";
            attackText.GetComponent<MeshRenderer>().sortingLayerName = "Player Card";
            costText.GetComponent<MeshRenderer>().sortingLayerName = "Player Card";
            nameText.GetComponent<MeshRenderer>().sortingLayerName = "Player Card";
            cardEffectText.GetComponent<MeshRenderer>().sortingLayerName = "Player Card";
        }

        else
        {
            cardArtDisplay.sortingLayerName = "Player Card";
            typeIcon.sortingLayerName = "Player Card";
            attackText.GetComponent<MeshRenderer>().sortingLayerName = "Selected Player Card";
            powerIcon.sortingLayerName = "Player Card";
            currencyIcon.sortingLayerName = "Player Card";
            cardEffectCostsIcons.sortingLayerName = "Player Card";
            typeIcon.sortingLayerName = "Player Card";
            cardBorder.sortingLayerName = "Player Card";
            cardEffectTextBox.sortingLayerName = "Player Card";
            foreach (var spriteRenderer in cardEffectTextBox.gameObject.GetComponentsInChildren<SpriteRenderer>())
            {
                spriteRenderer.sortingLayerName = "Player Card";
            }
            cardNameTextBox.sortingLayerName = "Player Card";
            cardArtDisplay.sortingLayerName = "Player Card";
            attackText.GetComponent<MeshRenderer>().sortingLayerName = "Player Card";
            nameText.GetComponent<MeshRenderer>().sortingLayerName = "Player Card";
            cardEffectText.GetComponent<MeshRenderer>().sortingLayerName = "Player Card";
        }
        
    }
    
    [PunRPC]
    private void DestroyCard()
    {
        Destroy(gameObject);
    }
}
