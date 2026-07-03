using System;
using System.Collections.Generic;
using UnityEngine;

// מחלקות עזר פשוטות שיוניטי מסוגלת להפוך ל-JSON (חובה Serializable)
[Serializable]
public class ScoreEntry
{
    public int score;
    public string date;
}

[Serializable]
public class HighScoreData
{
    public List<ScoreEntry> scores = new List<ScoreEntry>();
}

public static class SaveSystem
{
    private static readonly string PlayerPrefsKey = "SnakeHighScores";

    public static List<ScoreEntry> LoadHighScores()
    {
        if (!PlayerPrefs.HasKey(PlayerPrefsKey))
        {
            return new List<ScoreEntry>();
        }

        string json = PlayerPrefs.GetString(PlayerPrefsKey);
        HighScoreData data = JsonUtility.FromJson<HighScoreData>(json);
        return data.scores;
    }

    public static void SaveScore(int newScore)
    {
        List<ScoreEntry> currentScores = LoadHighScores();

        ScoreEntry newEntry = new()
        {
            score = newScore,
            date = DateTime.Now.ToString("dd/MM/yyyy")
        };

        currentScores.Add(newEntry);

        currentScores.Sort((x, y) => y.score.CompareTo(x.score));

        if (currentScores.Count > 10)
        {
            currentScores.RemoveRange(10, currentScores.Count - 10);
        }
        HighScoreData data = new () { scores = currentScores };
        string json = JsonUtility.ToJson(data);
        PlayerPrefs.SetString(PlayerPrefsKey, json);
        PlayerPrefs.Save();
    }
}