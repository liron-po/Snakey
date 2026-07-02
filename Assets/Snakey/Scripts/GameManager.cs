using UnityEngine;
using TMPro; // חובה בשביל לשלוט בטקסט של TextMeshPro

public class GameManager : MonoBehaviour
{
    // Singleton - מאפשר גישה מכל מקום במשחק באמצעות GameManager.Instance
    public static GameManager Instance { get; private set; }

    [Header("UI Elements")]
    public TextMeshProUGUI scoreText; // נגרור לכאן את אובייקט הטקסט מהסצנה
    public int Score => _score;
    private int _score = 0;

    void Awake()
    {
        // הגדרת ה-Singleton
        if (Instance == null) {
            Instance = this;
        } else {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        UpdateScoreUI();
    }

    // פונקציה ציבורית שנקרא לה בכל פעם שהנחש אוכל תפוח
    public void AddScore(int points)
    {
        _score += points;
        UpdateScoreUI();
    }

    // עדכון הטקסט על המסך
    void UpdateScoreUI()
    {
        if (scoreText != null) {
            scoreText.text = "Score: " + _score;
        }
    }
}