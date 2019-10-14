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
    [SerializeField] private List<GameObject> cardIcons = new List<GameObject>();

    public static PhotonView thisPhotonView;

    //When the PlayerCardDisplay is loaded we want to load in the components into the display
    //    protected override void Awake()
    //    {
    //        LoadCardIntoDisplay();
    //    }

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
        PhotonView photonView = GetComponent<PhotonView>();
        if (PhotonNetwork.IsMasterClient)
        {
            if (PhotonNetwork.AllocateViewID(photonView))
            {
                object[] data = { photonView.ViewID };

                RaiseEventOptions raiseEventOptions = new RaiseEventOptions
                {
                    Receivers = ReceiverGroup.Others,
                    CachingOption = EventCaching.AddToRoomCache
                };

                SendOptions sendOptions = new SendOptions
                {
                    Reliability = true
                };

                PhotonNetwork.RaiseEvent((byte)1, data, raiseEventOptions, sendOptions);
            }
            else
            {
                Debug.Log("Unable to allocate ID");
            }

            Debug.Log("ViewID: " + photonView.ViewID);
        }
        //thisPhotonView = GetComponent(this.gameObject.GetComponentInChildren<PhotonView>);
        //PhotonPeer.RegisterType(typeof(PlayerCardDisplay), (byte)'C', PlayerCardDisplay.SerializeCard, PlayerCardDisplay.DeserializeCard);
    }

    public static explicit operator PlayerCardDisplay(GameObject v)
    {
        throw new NotImplementedException();
    }

    public void OnEvent(EventData photonEvent)
    {
        if (photonEvent.Code == (byte) 1)
        {
            object[] data = (object[])photonEvent.CustomData;

            PhotonView photonView = GetComponent<PhotonView>();
            photonView.ViewID = (int)data[0];
        }
        else
        {
            Debug.Log("Event code not found..");
        }
    }

    //private static short SerializeCard(StreamBuffer outStream, object customobject)
    //{
    //    PlayerCardDisplay vo = (PlayerCardDisplay)customobject;
    //    //lock (memVector2)
    //    //{
    //    //    byte[] bytes = memVector2;
    //    //    int index = 0;
    //    //    Protocol.Serialize(vo.attackText, bytes, ref index);
    //    //    Protocol.Serialize(vo.y, bytes, ref index);
    //    //    outStream.Write(bytes, 0, 2 * 4);
    //    //}

    //    return 2 * 4;
    //}

    //private static object DeserializeCard(StreamBuffer inStream, short length)
    //{
    //    PlayerCardDisplay vo = new PlayerCardDisplay();
    //    //lock (memVector2)
    //    //{
    //    //    inStream.Read(memVector2, 0, 2 * 4);
    //    //    int index = 0;
    //    //    Protocol.Deserialize(out vo.x, memVector2, ref index);
    //    //    Protocol.Deserialize(out vo.y, memVector2, ref index);
    //    //}

    //    return vo;
    //}

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


}
