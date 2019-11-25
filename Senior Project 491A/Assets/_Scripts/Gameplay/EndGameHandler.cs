using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndGameHandler : MonoBehaviour
{
    public static void TriggerEndGame()
    {
        NotificationWindowEvent.Instance.EnableNotificationWindow("Game ended please restart scene");
    }
}
