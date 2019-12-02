using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndGameHandler : MonoBehaviour
{
    public static Action GameEnded;
    
    
    public static void TriggerEndGame()
    {
        GameEnded?.Invoke();
        NotificationWindowEvent.Instance.EnableNotificationWindow("Game ended please restart scene");
    }
}
