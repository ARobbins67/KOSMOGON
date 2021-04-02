using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms.Impl;
using TMPro;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private GameObject Gameplay;
    [SerializeField] private GameObject GameOverUI;
    [SerializeField] private GameObject MainMenu;

    private FMODController fmod;
    private ScoreManager scoreMan;
    [SerializeField] TextMeshProUGUI menuScoreText;
    
    // Start is called before the first frame update
    void Start()
    {
        // get/set highscore
        if (PlayerPrefs.GetInt("reset") == 1)
        {
            PlayerPrefs.SetFloat("score", 0);
            PlayerPrefs.SetInt("reset", 2);
        }

        scoreMan = GameObject.FindObjectOfType<ScoreManager>();
        if (Gameplay != null)
        {
            Gameplay.SetActive(true);
        }
        if (GameOverUI != null)
        {
            GameOverUI.SetActive(false);
        }

        if (SceneManager.GetActiveScene().name == "MainMenu" && PlayerPrefs.GetFloat("reset") == 2)
        {
            menuScoreText.text = "High Score: " + PlayerPrefs.GetFloat("score");
        }

        if (SceneManager.GetActiveScene().name == "Level 1" || SceneManager.GetActiveScene().name == "Level 2")
        {
            fmod = GameObject.FindObjectOfType<FMODController>();
        }
    }

    public void ToMenu()
    {
        if (fmod != null)
        {
            fmod.StopPlaying();
            Debug.Log("Stop");
        }
        SceneManager.LoadScene("MainMenu",LoadSceneMode.Single);
        
    }

    public void ToLevel1()
    {
        Debug.Log("Level 1");
        if (fmod != null)
        {
            fmod.StopPlaying();
            Debug.Log("Stop");
        }
        SceneManager.LoadScene("Level 1", LoadSceneMode.Single);
    }
    public void ToLevel2()
    {
        if (fmod != null)
        {
            fmod.StopPlaying();
            Debug.Log("Stop");
        }
        SceneManager.LoadScene("Level 2", LoadSceneMode.Single);
    }

    public void ToLeaderboard()
    {
        MainMenu.SetActive(false);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
    public void SaveScore(float finalScore)
    {
        scoreMan.SetFinalScore(finalScore);
        float HighScore = PlayerPrefs.GetFloat("score");
        if (finalScore > HighScore)
        {
            PlayerPrefs.SetFloat("score",finalScore);
        }
    }

    public void GameOver()
    {
        if (Gameplay != null && GameOverUI != null)
        {
            Gameplay.SetActive(false);
            GameOverUI.SetActive(true);
        }
    }

    public void UpdateMenuScoreText()
    {
        if (SceneManager.GetActiveScene().name == "MainMenu")
        {
            menuScoreText.text = "Your score: " + PlayerPrefs.GetFloat("score");
        }
    }
}
