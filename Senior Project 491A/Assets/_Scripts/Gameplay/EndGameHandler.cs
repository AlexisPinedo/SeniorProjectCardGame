using System;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndGameHandler : MonoBehaviour
{
    public static Action GameEnded;

    [SerializeField] private GameObject EndGameWindowPanel;

    [SerializeField] private GameObject EndButtonWindowButton;

    [SerializeField] private TextMeshProUGUI EndGameText;

    private void Awake()
    {
        EndGameText = EndGameWindowPanel.GetComponentInChildren<TextMeshProUGUI>();
    }

    private void OnEnable()
    {
        GameEnded += DisplayEndGameWindow;
    }

    private void OnDisable()
    {
        GameEnded -= DisplayEndGameWindow;
    }

    private void SetEndGameText(string text)
    {
        EndGameText.text = text;    
    }

    public static void TriggerEndGame()
    {
        GameEnded?.Invoke();
    }

    private void DisplayEndGameWindow()
    {
        EndGameWindowPanel.SetActive(true);
        EndButtonWindowButton.SetActive(true);
    }

    public void ReturnToMainMenu()
    {
        SceneManager.LoadScene("Lobby", LoadSceneMode.Single);
    }
}
