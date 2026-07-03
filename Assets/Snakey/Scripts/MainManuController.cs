using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using System;

public class MainMenuController : MonoBehaviour
{
    [Header("Panels")]
    [SerializeField]
    private GameObject mainPanel;
    [SerializeField]
    private GameObject highScoresPanel;

    [Header("UI Elements")]
    [SerializeField]
    private TextMeshProUGUI scoresText;

    private void Start()
    {
        ShowMainPanel();
    }

    public void PlayGame()
    {
        SceneManager.LoadSceneAsync("Gameplay");
    }

    public void ShowHighScores()
    {
        mainPanel.SetActive(false);
        highScoresPanel.SetActive(true);

        List<ScoreEntry> highScores = SaveSystem.LoadHighScores();
        
        if (highScores == null || highScores.Count == 0)
        {
            scoresText.text = "No high scores yet!";
            return;
        }

        StringBuilder sb = new();

        for (int i = 0; i < highScores.Count; i++)
        {
            if (highScores[i].score <= 0) 
            {
                continue;
            }

            var localDate = DateTimeOffset.FromUnixTimeSeconds(highScores[i].timestamp).LocalDateTime;
            sb.AppendLine($"{i + 1}. {highScores[i].score} Pts ({localDate:yyyy-MM-dd HH:mm:ss})");
        }

        scoresText.text = sb.Length > 0 ? sb.ToString() : "No high scores yet!";
    }

    public void ShowMainPanel()
    {
        mainPanel.SetActive(true);
        highScoresPanel.SetActive(false);
    }

    public void QuitGame()
    {
        Debug.Log("Quitting game...");

        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #else
        Application.Quit();
        #endif
    }
}