using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager instance;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI congratsText;
    public TextMeshProUGUI gameOverText;
    public Button restartButton;
    public Button exitButton;
    public AudioClip congratsSFX;
    private AudioSource audioSource;
    private int score;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        score = 0;
        AtualizarTextoScore();
        audioSource = GetComponent<AudioSource>();
        gameOverText.gameObject.SetActive(false);
        congratsText.gameObject.SetActive(false);
        restartButton.gameObject.SetActive(false);
        exitButton.gameObject.SetActive(false);
    }

    public void AdicionarPonto()
    {
        score++;
        AtualizarTextoScore();
        if (score >= 10)
        {
            PlayCongratsSFX();
            ShowCongratulations();
        }
    }

    void AtualizarTextoScore()
    {
        scoreText.text = "Score: " + score.ToString();
    }

    private void PlayCongratsSFX()
    {
        if (audioSource != null && congratsSFX != null)
        {
            audioSource.Stop();
            audioSource.PlayOneShot(congratsSFX);
        }
    }

    private void ShowCongratulations()
    {
        congratsText.gameObject.SetActive(true);
        restartButton.gameObject.SetActive(true);
        exitButton.gameObject.SetActive(true);
        Time.timeScale = 0f; // Pausa o jogo
    }

    public void GameOver()
    {
        gameOverText.gameObject.SetActive(true);
        restartButton.gameObject.SetActive(true);
        exitButton.gameObject.SetActive(true);
        Time.timeScale = 0f; // Pausa o jogo
    }

    public void Restart()
    {
        Time.timeScale = 1f; // Retoma o tempo
        score = 0; // Reinicia o score
        AtualizarTextoScore();
        congratsText.gameObject.SetActive(false);
        restartButton.gameObject.SetActive(false);
        exitButton.gameObject.SetActive(false);
        gameOverText.gameObject.SetActive(false);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
