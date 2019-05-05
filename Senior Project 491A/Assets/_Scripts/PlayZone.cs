using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayZone : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D col)
    {
        Debug.Log(col.gameObject.name + " has entered the scene");

        PlayerCard card = col.gameObject.GetComponent<PlayerCard>();
        int currency = card.cardCurrency;
        int atk = card.cardAttack;

        Debug.Log("Currency: " + currency);
        Debug.Log("Attack: " + atk);

        GameObject.Destroy(col.gameObject);
    }
}
