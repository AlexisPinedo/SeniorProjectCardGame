using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEventManager : MonoBehaviour
{
    private Queue<Event_Base> stateQueue = new Queue<Event_Base>();

    private bool eventActive = false;

    private static GameEventManager _instance;
    public static GameEventManager Instance
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

    public void AddStateToQueue(Event_Base eventState)
    {
        if (stateQueue.Count == 0)
        {
            stateQueue.Enqueue(eventState);
            StartCoroutine(HandleState());
        }
        else
        {
            stateQueue.Enqueue(eventState);
        }
    }

    public void EndEvent()
    {
        eventActive = false;
    }

    IEnumerator HandleState()
    {
        Event_Base nextEventToRun = stateQueue.Peek();

        eventActive = true;

        nextEventToRun.EventState();

        while (eventActive)
        {
            yield return null;
        }

        stateQueue.Dequeue();

        if (stateQueue.Count > 0)
        {
            StartCoroutine(HandleState());
        }
    }
}
