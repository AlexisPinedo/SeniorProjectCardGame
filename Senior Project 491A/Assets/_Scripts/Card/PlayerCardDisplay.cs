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
using UnityEngine.Tilemaps;

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

    private byte currentCardIdenrifier = (byte)'C';

    [SerializeField] private TextMeshPro attackText;
    [SerializeField] private TextMeshPro costText;
    [SerializeField] private TextMeshPro currencyText;
    [SerializeField] private SpriteRenderer cardEffectCostsIcons;
    [SerializeField] private SpriteRenderer costIcon;
    [SerializeField] private List<GameObject> cardIcons = new List<GameObject>();
    
    public delegate void _CardPurchased(PlayerCardDisplay cardBought);

    public static event _CardPurchased CardPurchased;

    public string sortLayerName;
    public int sortOrder;
    
    public GameObject[] cardParts;
//    public GameObject cardImage;
//    public GameObject cardTypeIcon;
//    public GameObject cardAttackIcon;
//    public GameObject cardSoulIcon;
//    public GameObject cardCostIcon;
//    public GameObject cardBorderImage;
//    public GameObject effectsTxt;
//    public GameObject nameTxt;
//    
//    public string imageLayerName;
//    public string typeLayerName;
//    public string attackLayerName;
//    public string soulLayerName;
//    public string costLayerName;
//    public string borderLayerName;
//    public string effectsLayerName;
//    public string nameLayer;
//    
//    public int originalImageOrder;
//    public int originalTypeOrder;
//    public int originalAttackOrder;
//    public int originalSoulOrder;
//    public int originalCostOrder;
//    public int originalBorderOrder;
//    public int originaleffectOrder;
//    public int originalNameOrder;

    //private static List<int> photonViewIDs = new List<int>();

    //When the PlayerCardDisplay is loaded we want to load in the components into the display
    protected override void Awake()
    {
        base.Awake();
        LoadCardIntoDisplay();
    }

    /// <summary>
    /// Called when this object is enabled. Adds EventReceived to the Networking Client.
    /// </summary>
    //[ExecuteInEditMode]
    //    protected override void OnEnable()
    //    {
    //        base.OnEnable();
    //    }

    /// <summary>
    /// Called when this object is disabled. Removes EventReceived from the Networking Client.
    /// </summary>
    //[ExecuteInEditMode]
    //    protected override void OnDisable()
    //    {
    //        ClearCardFromDisplay();
    //    }

    void Start()
    { 
        if(!PhotonNetwork.OfflineMode)
        {
            if (PhotonNetwork.IsMasterClient)
            {
                if (PhotonNetwork.AllocateViewID(photonView))
                {
                    object[] data = {photonView.ViewID};

                    RaiseEventOptions raiseEventOptions = new RaiseEventOptions
                    {
                        Receivers = ReceiverGroup.Others,
                        CachingOption = EventCaching.AddToRoomCache
                    };

                    SendOptions sendOptions = new SendOptions
                    {
                        Reliability = true
                    };

//                    Debug.Log("PlayerCard assigned ViewID: " + photonView.ViewID);

                    PhotonNetwork.RaiseEvent(currentCardIdenrifier, data, raiseEventOptions, sendOptions);
                    if (!PhotonNetworkManager.currentPhotonPlayer.IsMasterClient)
                    {
                        Debug.Log(
                            "Master Client has assigned a PhotonView ID and is transfering ownership to other player...");
                        photonView.TransferOwnership(PhotonNetworkManager.currentPhotonPlayer);
                    }
                }
                else
                {
                    Debug.Log("Unable to allocate ID");
                }
            }

//            cardImage.GetComponent<Renderer>().sortingLayerName = imageLayerName;
//            cardTypeIcon.GetComponent<Renderer>().sortingLayerName = typeLayerName;
//            cardAttackIcon.GetComponent<Renderer>().sortingLayerName = attackLayerName;
//            cardSoulIcon.GetComponent<Renderer>().sortingLayerName = soulLayerName;
//            cardCostIcon.GetComponent<Renderer>().sortingLayerName = costLayerName;
//            cardBorderImage.GetComponent<Renderer>().sortingLayerName = borderLayerName;
//            effectsTxt.GetComponent<Renderer>().sortingLayerName = effectsLayerName;
//            nameTxt.GetComponent<Renderer>().sortingLayerName = nameLayer;
//            
//            cardImage.GetComponent<Renderer>().sortingOrder = originalImageOrder;
//            cardTypeIcon.GetComponent<Renderer>().sortingOrder = originalTypeOrder;
//            cardAttackIcon.GetComponent<Renderer>().sortingOrder = originalAttackOrder;
//            cardSoulIcon.GetComponent<Renderer>().sortingOrder = originalSoulOrder;
//            cardCostIcon.GetComponent<Renderer>().sortingOrder = originalCostOrder;
//            cardBorderImage.GetComponent<Renderer>().sortingOrder = originalBorderOrder;
//            effectsTxt.GetComponent<Renderer>().sortingOrder = originaleffectOrder;
//            nameTxt.GetComponent<Renderer>().sortingOrder = originalNameOrder;
            cardParts[0].GetComponent<Renderer>().sortingOrder = 1;
            cardParts[1].GetComponent<Renderer>().sortingOrder = 2;
            cardParts[2].GetComponent<Renderer>().sortingOrder = 2;
            cardParts[3].GetComponent<Renderer>().sortingOrder = 2;
            cardParts[4].GetComponent<Renderer>().sortingOrder = 2;
            cardParts[5].GetComponent<Renderer>().sortingOrder = 2;
            cardParts[6].GetComponent<Renderer>().sortingOrder = 2;
            cardParts[7].GetComponent<Renderer>().sortingOrder = 2;
            for (int i = 0; i < cardParts.Length; i++)
            {
                cardParts[i].GetComponent<Renderer>().sortingLayerName = sortLayerName;
                //cardParts[i].GetComponent<Renderer>().sortingOrder = sortOrder;
//                cardParts[0].GetComponent<Renderer>().sortingOrder = 1;
//                cardParts[1].GetComponent<Renderer>().sortingOrder = 2;
//                cardParts[2].GetComponent<Renderer>().sortingOrder = 2;
//                cardParts[3].GetComponent<Renderer>().sortingOrder = 2;
//                cardParts[4].GetComponent<Renderer>().sortingOrder = 2;
//                cardParts[5].GetComponent<Renderer>().sortingOrder = 2;
//                cardParts[6].GetComponent<Renderer>().sortingOrder = 2;
//                cardParts[7].GetComponent<Renderer>().sortingOrder = 1;
                cardParts[i].GetComponent<Renderer>().sortingOrder = sortOrder;
            }
        }
    }

    public void OnEnable()
    {
        base.OnEnable();
        PhotonNetwork.NetworkingClient.EventReceived += OnEvent;
    }

    public void OnDisable()
    {
        base.OnEnable();
        PhotonNetwork.NetworkingClient.EventReceived -= OnEvent;
    }

    public void OnEvent(EventData photonEvent)
    {
        byte recievedCode = photonEvent.Code;
        if (recievedCode == currentCardIdenrifier)
        {
            object[] data = (object[])photonEvent.CustomData;
            int recievedPhotonID = (int)data[0];

            if (!PhotonNetworkManager.photonViewIDs.Contains(recievedPhotonID))
            {
                photonView.ViewID = recievedPhotonID;
                PhotonNetworkManager.photonViewIDs.Add(recievedPhotonID);

//                Debug.Log("PlayerCard RPC to assign PhotonView ID: " + photonView.ViewID);
                PhotonNetwork.NetworkingClient.EventReceived -= OnEvent;
            }
        }
        else
        {
            //Debug.Log("Event code not found");
        }
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
        costText.text = card.CardCost.ToString();
        currencyText.text = card.CardCurrency.ToString();
        LoadCostEffectIcons();
    }
    
    //increase the order of the icons inside the card once the card is highlighted
    public void OnMouseEnter()
    {
        //TODO: Implement an incrementor for the sorting order of each sorting layer
        cardParts[0].GetComponent<Renderer>().sortingOrder = 2;
        cardParts[1].GetComponent<Renderer>().sortingOrder = 3;
        cardParts[2].GetComponent<Renderer>().sortingOrder = 3;
        cardParts[3].GetComponent<Renderer>().sortingOrder = 3;
        cardParts[4].GetComponent<Renderer>().sortingOrder = 3;
        cardParts[5].GetComponent<Renderer>().sortingOrder = 3;
        cardParts[6].GetComponent<Renderer>().sortingOrder = 3;
        cardParts[7].GetComponent<Renderer>().sortingOrder = 3;
        //sortOrder += 1;
//        originalImageOrder += 1;
//        originalTypeOrder += 1;
//        originalAttackOrder += 1;
//        originalSoulOrder += 1;
//        originalCostOrder += 1;
//        originalBorderOrder += 1;
//        originaleffectOrder += 1;
//        originalNameOrder += 1;
    }

    public void OnMouseExit()
    {
        //TODO: Return the sorting order back to its original value
        cardParts[0].GetComponent<Renderer>().sortingOrder = 1;
        cardParts[1].GetComponent<Renderer>().sortingOrder = 2;
        cardParts[2].GetComponent<Renderer>().sortingOrder = 2;
        cardParts[3].GetComponent<Renderer>().sortingOrder = 2;
        cardParts[4].GetComponent<Renderer>().sortingOrder = 2;
        cardParts[5].GetComponent<Renderer>().sortingOrder = 2;
        cardParts[6].GetComponent<Renderer>().sortingOrder = 2;
        cardParts[7].GetComponent<Renderer>().sortingOrder = 2;
        
//        for (int j = 0; j < cardParts.Length; j++)
//        {
//            cardParts[j].GetComponent<Renderer>().sortingOrder = sortOrder;
//        }
        //sortOrder -= 1;
//        originalImageOrder -= 1;
//        originalTypeOrder -= 1;
//        originalAttackOrder -= 1;
//        originalSoulOrder -= 1;
//        originalCostOrder -= 1;
//        originalBorderOrder -= 1;
//        originaleffectOrder -= 1;
//        originalNameOrder -= 1;
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

        if (this.transform.parent.gameObject.GetComponent<HandContainer>() != null)
        {
            costIcon.gameObject.SetActive(false);
        }
    }

    /// <summary>
    /// this method will look at sprite list of required costs icons and place them appropriately into the scene
    /// 
    /// </summary>
    private void LoadCostEffectIcons()
    {
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
}