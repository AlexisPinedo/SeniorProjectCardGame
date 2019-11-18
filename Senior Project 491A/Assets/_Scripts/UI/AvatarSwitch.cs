using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AvatarSwitch : MonoBehaviour
{
    [SerializeField]
    private GameObject p1Avatar;

    [SerializeField]
    private GameObject p2Avatar;

    [SerializeField]
    private TurnPlayerManager turnPlayerManager;

    void Start()
    {
        p2Avatar.SetActive(false);
    }

    public void ShowHideAvatar()
    {
        if (p1Avatar != null && p2Avatar != null)
        {
            // Switch to Player Two
            if (p1Avatar.activeSelf)
            {
                p1Avatar.SetActive(false);
                p2Avatar.SetActive(true);
            }
            // Switch to Player One
            else if (p2Avatar.activeSelf)
            {
                p2Avatar.SetActive(false);
                p1Avatar.SetActive(true);
            }
        }
    }
}
