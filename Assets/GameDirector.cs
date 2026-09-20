using UnityEngine;
using TMPro;

public class GameDirector : MonoBehaviour
{
    [Header("UI 設定")]
    public TMP_Text timeText;
    public TMP_Text scoreText;
    public TMP_Text gameOverText;

    [Header("遊戲設定")]
    public float gameTime = 60.0f;

    private float timer;
    private int score = 0;
    private bool isGameOver = false;

    public bool IsGameOver
    {
        get { return isGameOver; }
    }

    void Start()
    {
        Time.timeScale = 1f;

        timer = gameTime;
        score = 0;
        isGameOver = false;

        if (gameOverText != null)
        {
            gameOverText.gameObject.SetActive(false);
        }

        UpdateUI();
    }

    void Update()
    {
        if (isGameOver) return;

        timer -= Time.deltaTime;

        if (timer <= 0)
        {
            timer = 0;
            GameOver();
        }

        UpdateUI();
    }

    public void CatchApple()
    {
        if (isGameOver) return;

        score += 100;
        Debug.Log("接到蘋果，加 100 分");
        UpdateUI();
    }

    public void CatchBomb()
    {
        if (isGameOver) return;

        score /= 2;
        Debug.Log("接到炸彈，分數減半");
        UpdateUI();
    }

    public void GameOver()
    {
        if (isGameOver) return;

        isGameOver = true;

        if (gameOverText != null)
        {
            gameOverText.gameObject.SetActive(true);
            gameOverText.text = "GAME OVER";
        }

        Debug.Log("Game Over，最後分數：" + score);
    }

    void UpdateUI()
    {
        if (timeText != null)
        {
            timeText.text = timer.ToString("F1");
        }

        if (scoreText != null)
        {
            scoreText.text = "Score:\n" + score.ToString();
        }
    }
}