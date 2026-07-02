using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class MainMenuController : MonoBehaviour
{
    [Header("Panels")]
    public GameObject mainPanel;
    public GameObject highScoresPanel;

    [Header("UI Elements")]
    public TextMeshProUGUI scoresText;

    void Start()
    {
        // ודא שהתפריט הראשי דולק והשאר כבוי
        ShowMainPanel();
    }

    // נקרא לזה בלחיצה על Start Game
    public void PlayGame()
    {
        // טעינה אסינכרונית של סצנת המשחק (נניח שקוראים לה Gameplay)
        SceneManager.LoadSceneAsync("Gameplay");
    }

    // נקרא לזה בלחיצה על High Scores
    public void ShowHighScores()
    {
        mainPanel.SetActive(false);
        highScoresPanel.SetActive(true);

        // טעינת הנתונים והצגתם בטקסט
        List<ScoreEntry> highScores = SaveSystem.LoadHighScores();
        
        if (highScores.Count == 0)
        {
            scoresText.text = "No high scores yet!";
            return;
        }

        StringBuilder sb = new StringBuilder();
        for (int i = 0; i < highScores.Count; i++)
        {
            sb.AppendLine($"{i + 1}. {highScores[i].score} Pts ({highScores[i].date})");
        }

        scoresText.text = sb.ToString();
    }

    public void ShowMainPanel()
    {
        mainPanel.SetActive(true);
        highScoresPanel.SetActive(false);
    }

    // נקרא לזה בלחיצה על Quit (עובד רק בבנייה לנייד/מחשב, לא בתוך ה-Editor)
    public void QuitGame()
    {
        Debug.Log("Quitting game...");
        Application.Quit();
    }
}