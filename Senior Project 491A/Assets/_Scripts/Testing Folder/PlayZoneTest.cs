using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;

public class PlayZoneTest : MonoBehaviour
{
    [SerializeField]
    private BoxCollider2D collider;

    void Awake()
    {
        collider = this.gameObject.GetComponent<BoxCollider2D>();
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        PlayerCard card = col.gameObject.GetComponent<PlayerCard>();

        GraveyardListTransfer.instance.Grave.Add(card);
        HandListTransfer.instance.Hand.Remove(card);
        Debug.Log("Collision Detected");

        col.gameObject.transform.position = new Vector3(0, 10, 0);
    }
}
