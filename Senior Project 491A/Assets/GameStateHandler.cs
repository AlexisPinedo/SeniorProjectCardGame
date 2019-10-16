using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStateHandler : MonoBehaviour
{
    public Queue<IEnumerator> stateQueue = new Queue<IEnumerator>();

    [SerializeField]
    public bool currentlyInaState = false;

    private static GameStateHandler _instance;

    public static GameStateHandler Instance
    {
        get => _instance;
    }
    

    private void Awake()
    {
        if (_instance == null && _instance != this)
            _instance = this;
        else
        {
            Destroy(this.gameObject);
        }
    }

    private void Update()
    {
        if(stateQueue.Count == 0)
            return;
        
        if(currentlyInaState)
            return;
        
        if (!currentlyInaState)
        {
            currentlyInaState = true;
            StartCoroutine(stateQueue.Dequeue());
        }
    }

    public void EndState()
    {
        currentlyInaState = false;
    }
}
