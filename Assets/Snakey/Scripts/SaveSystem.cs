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
    private static string PlayerPrefsKey = "SnakeHighScores";

    // טעינת רשימת השיאים מהמכשיר
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

    // הוספת שיא חדש, מיון, חיתוך ל-10 ושמירה
    public static void SaveScore(int newScore)
    {
        List<ScoreEntry> currentScores = LoadHighScores();

        // יצירת רשומה חדשה עם תאריך נוכחי
        ScoreEntry newEntry = new ScoreEntry
        {
            score = newScore,
            date = DateTime.Now.ToString("dd/MM/yyyy")
        };

        currentScores.Add(newEntry);

        // מיון מהגבוה לנמוך באמצעות LINQ
        currentScores.Sort((x, y) => y.score.CompareTo(x.score));

        // חיתוך הרשימה כך שתחזיק מקסימום 10 איברים
        if (currentScores.Count > 10)
        {
            currentScores.RemoveRange(10, currentScores.Count - 10);
        }

        // המרה חזרה ל-JSON ושמירה במכשיר
        HighScoreData data = new HighScoreData { scores = currentScores };
        string json = JsonUtility.ToJson(data);
        PlayerPrefs.SetString(PlayerPrefsKey, json);
        PlayerPrefs.Save();
    }
}