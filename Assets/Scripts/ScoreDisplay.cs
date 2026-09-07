using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class ScoreDisplay : MonoBehaviour
{
    public Bird bird;

    private Label scoreLabel;
    private Label gameOverLabel;
    private Button restartButton;
    private bool gameOver;

    void OnEnable()
    {
        VisualElement root = GetComponent<UIDocument>().rootVisualElement;

        scoreLabel = root.Q<Label>("score-label");
        gameOverLabel = root.Q<Label>("game-over-label");
        restartButton = root.Q<Button>("restart-button");

        gameOverLabel.style.display = DisplayStyle.None;
        restartButton.style.display = DisplayStyle.None;
        gameOver = false;

        restartButton.clicked += RestartGame;
    }

    void Update()
    {
        if (!gameOver)
        {
            scoreLabel.text = bird.Points.ToString();
        }
    }

    public void ShowGameOver()
    {
        gameOver = true;
        scoreLabel.text = "Final Score: " + bird.Points;
        gameOverLabel.style.display = DisplayStyle.Flex;
        restartButton.style.display = DisplayStyle.Flex;
    }

    private void RestartGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        Bird.gameStarted = false;
    }

    void OnDisable()
    {
        restartButton.clicked -= RestartGame;
    }
}
