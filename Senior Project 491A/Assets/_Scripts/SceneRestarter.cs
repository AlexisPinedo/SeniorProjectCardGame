using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SceneRestarter : MonoBehaviour
{
    public void RestartScene()
    {
        SceneManager.LoadSceneAsync("RainScene (Duplicate)");

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadSceneAsync("RainScene (Duplicate)");
        }
    }
}
