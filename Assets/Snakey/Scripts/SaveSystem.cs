using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public struct ScoreEntry
{
    public int score;
    public long timestamp;

    public ScoreEntry(int score)
    {
        this.score = score;
        this.timestamp = DateTimeOffset.UtcNow.ToUnixTimeSeconds();
    }
}

[Serializable]
public class HighScoreData
{
    public List<ScoreEntry> scores = new();
}

public static class SaveSystem
{
    private static readonly string PlayerPrefsKey = "SnakeHighScores_V2";

    public static List<ScoreEntry> LoadHighScores()
    {
        if (!PlayerPrefs.HasKey(PlayerPrefsKey))
        {
            return new List<ScoreEntry>();
        }

        string json = PlayerPrefs.GetString(PlayerPrefsKey);
        
        if (string.IsNullOrEmpty(json) || json == "{}") return new List<ScoreEntry>();

        try
        {
            HighScoreData data = JsonUtility.FromJson<HighScoreData>(json);
            return (data != null && data.scores != null) ? data.scores : new List<ScoreEntry>();
        }
        catch (Exception ex)
        {
            Debug.LogError($"[SaveSystem] Failed to parse JSON: {ex.Message}");
            return new List<ScoreEntry>();
        }
    }

    public static void SaveScore(int newScore)
    {
        List<ScoreEntry> currentScores = LoadHighScores();

        ScoreEntry newEntry = new(newScore);

        currentScores.Add(newEntry);
        currentScores.Sort((x, y) => y.score.CompareTo(x.score));

        if (currentScores.Count > 10)
        {
            currentScores.RemoveRange(10, currentScores.Count - 10);
        }

        HighScoreData data = new() { scores = currentScores };
        string json = JsonUtility.ToJson(data);
        
        PlayerPrefs.SetString(PlayerPrefsKey, json);
        PlayerPrefs.Save();
    }
}