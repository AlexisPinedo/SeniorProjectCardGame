using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawCard : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> cards = new List<GameObject>(10);

    private float tempSpot = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        // Check if the player has pressed the "draw" button
        if (Input.GetKeyDown("space"))
        {
            GameObject cardDrawn;

            // Get a random index and "draw" the card at that index
            int index = (int)Random.Range(0, cards.Count);
            cardDrawn = Instantiate(cards[index]);

            print(cardDrawn);   // DEBUGGING PURPOSES

            // Show card on the field
            cardDrawn.transform.position = new Vector3(-1.0f, tempSpot, 0.0f);

            // Remove card from deck and shift the y-axis for the next card
            cards.RemoveAt(index);
            tempSpot -= 0.5f;
        }
    }
}
