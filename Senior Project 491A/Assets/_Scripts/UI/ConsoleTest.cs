using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConsoleTest : MonoBehaviour
{
    public void OnEnable()
    {
        ButtonInputEventHandler.SettingsClicked += test;
        ButtonInputEventHandler.StartClicked += test;
        ButtonInputEventHandler.GraveyardClicked += test;
    }

    public void OnDisable()
    {
        ButtonInputEventHandler.SettingsClicked -= test;
        ButtonInputEventHandler.StartClicked -= test;
        ButtonInputEventHandler.GraveyardClicked -= test;
    }

    void test()
    {
        Debug.Log("clicked!");
    }
}
