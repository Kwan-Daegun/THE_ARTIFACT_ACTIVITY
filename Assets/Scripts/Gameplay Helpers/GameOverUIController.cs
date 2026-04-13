using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOverUIController : MonoBehaviour
{
    public static GameOverUIController instance;

    [SerializeField] private Canvas gameOverCanvas;
    [SerializeField] private Text gameOverText;
    [SerializeField] private Canvas winCanvas;
    [SerializeField] private Text winText;
    [SerializeField] private GameObject enemySpawner;

    private void Awake()
    {
        if (instance == null)
            instance = this;

        if (winCanvas != null)
            winCanvas.enabled = false;
    }

    public void GameOver(string gameOverInfo)
    {
        gameOverText.text = gameOverInfo;
        gameOverCanvas.enabled = true;
        Time.timeScale = 0f;
        Destroy(enemySpawner);
    }

    public void Win()
    {
        if (winCanvas != null)
            winCanvas.enabled = true;

        if (winText != null)
            winText.text = "You Win!";

        Time.timeScale = 0f;
        Destroy(enemySpawner);
    }

    public void RestartGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void MainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
    }
}