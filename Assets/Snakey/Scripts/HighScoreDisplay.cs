using UnityEngine;
using TMPro;
using System.Text;
using System.Collections.Generic;
using System;

public class HighScoreDisplay : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI scoresText;

    public void RefreshDisplay()
    {
        List<ScoreEntry> highScores = SaveSystem.LoadHighScores();
        
        if (highScores == null || highScores.Count == 0)
        {
            scoresText.text = "No high scores yet!";
            return;
        }

        StringBuilder sb = new StringBuilder(256);
        for (int i = 0; i < highScores.Count; i++)
        {
            if (highScores[i].score <= 0) continue;

            var date = DateTimeOffset.FromUnixTimeSeconds(highScores[i].timestamp).LocalDateTime;
            sb.Append(i + 1).Append(". ").Append(highScores[i].score)
              .Append(" Pts (").Append(date.ToString("yyyy-MM-dd")).AppendLine(")");
        }
        scoresText.text = sb.ToString();
    }
}