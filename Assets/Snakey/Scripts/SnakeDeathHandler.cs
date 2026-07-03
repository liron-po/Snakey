using UnityEngine;
using UnityEngine.SceneManagement;

public class SnakeDeathHandler : MonoBehaviour
{
    public void TriggerGameOver()
    {
        Debug.Log("Game Over!");

        if (GameManager.Instance != null)
        {
            int finalScore = GameManager.Instance.Score;
            SaveSystem.SaveScore(finalScore);
        }

        SceneManager.LoadSceneAsync("Mainmenu");
    }
}
