using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SceneRestarter : MonoBehaviour
{
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadSceneAsync("RainScene (Duplicate)");
        }
    }

    public void RestartScene()
    {
        SceneManager.LoadSceneAsync("RainScene (Duplicate)");
    }
}
