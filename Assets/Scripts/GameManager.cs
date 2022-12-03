using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public List<GameObject> targets;
    private float spawnRate = 1.0f;

    public GameObject titleScreen;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI livesText;

    public TextMeshProUGUI gameOverText;
    public Button restartButton;

    public AudioSource backgrondMusic;
    public Slider volumeSlider;

    private bool isGameActive;
    private int score;
    private int lives;

    public Image pausePanel;
    public TextMeshProUGUI pauseText;
    private bool isPaused;

    private void Update()
    {
        backgrondMusic.volume = volumeSlider.value;

        if (Input.GetKeyDown(KeyCode.Space) && isGameActive)
        {
            IsPausedUpdate(!isPaused);
            PauseGame();
        }
    }

    private IEnumerator SpawnTarget()
    {
        while (isGameActive)
        {
            yield return new WaitForSeconds(spawnRate);
            Instantiate(targets[RandomIndex()]);
        }
    }

    public void GameOver()
    {
        IsGameActiveUpdate(false);
        gameOverText.gameObject.SetActive(true);
        restartButton.gameObject.SetActive(true);
    }

    public void PauseGame()
    {
        if (isPaused)
            Time.timeScale = 0;
        else
            Time.timeScale = 1;

        pauseText.gameObject.SetActive(isPaused);
        pausePanel.gameObject.SetActive(isPaused);
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void StartGame(int difficulty)
    {
        score = 0;
        spawnRate /= difficulty;
        SetLives(difficulty);

        IsGameActiveUpdate(true);
        TextScoreUpdate();
        TextLivesUpdate();

        scoreText.gameObject.SetActive(true);
        livesText.gameObject.SetActive(true);
        titleScreen.gameObject.SetActive(false);

        StartCoroutine(SpawnTarget());
    }

    private int RandomIndex()
    {
        return Random.Range(0, targets.Count);
    }

    public void ScoreUpdate(int scoreToAdd)
    {
        score += scoreToAdd;
        TextScoreUpdate();
    }

    private void TextScoreUpdate()
    {
        scoreText.text = "Score: " + score;
    }

    private void SetLives(int difficulty)
    {
        lives = difficulty;
    }

    public void LivesUpdate(int value)
    {
        if (isLivesEnough())
        {
            lives += value;
            TextLivesUpdate();
        }
    }

    public bool isLivesEnough()
    {
        return lives > 0;
    }

    private void TextLivesUpdate()
    {
        livesText.text = "Lives: " + lives;
    }

    public bool IsGameActive()
    {
        return isGameActive;
    }

    private void IsGameActiveUpdate(bool value)
    {
        isGameActive = value;
    }

    private void IsPausedUpdate(bool value)
    {
        isPaused = value;
    }
}
