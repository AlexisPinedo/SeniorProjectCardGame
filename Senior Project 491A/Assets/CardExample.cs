using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardExample : MonoBehaviour
{
    public CreateGrid handGrid;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void BoughtCard(GameObject turnPlayer)
    {
        handGrid = turnPlayer.GetComponent<CreateGrid>();

        Vector2 spawnPoint = handGrid.GetNearestPointOnGrid(new Vector2());
        this.gameObject.transform.position = spawnPoint;
        this.gameObject.SetActive(true);
    }
}
