using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    [SerializeField] private CanvasGroup mainPanel;
    [SerializeField] private CanvasGroup highScoresPanel;
    [SerializeField] private HighScoreDisplay highScoreDisplay;

    private void Start() => ShowMainPanel();

    public void PlayGame() => SceneManager.LoadSceneAsync("Gameplay");

    public void ShowHighScores()
    {
        SwitchPanel(highScoresPanel);
        highScoreDisplay.RefreshDisplay();
    }

    public void ShowMainPanel() => SwitchPanel(mainPanel);

    private void SwitchPanel(CanvasGroup activePanel)
    {
        mainPanel.alpha = (activePanel == mainPanel) ? 1 : 0;
        mainPanel.blocksRaycasts = (activePanel == mainPanel);
        
        highScoresPanel.alpha = (activePanel == highScoresPanel) ? 1 : 0;
        highScoresPanel.blocksRaycasts = (activePanel == highScoresPanel);
    }

    public void QuitGame() 
    {
        #if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false;
        #else
                Application.Quit();
        #endif
    }
}