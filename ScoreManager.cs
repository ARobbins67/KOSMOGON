using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    [SerializeField] List<GameObject> Strikes = new List<GameObject>();
    [SerializeField] private TextMeshProUGUI finalScoreText;

    private LevelManager level;
    private TextMeshProUGUI text;
    private int numStrikes = 0;
    private float currentScore = 0;

    private FMODController fmod;
    // Start is called before the first frame update
    void Start()
    {
        text = GetComponent<TextMeshProUGUI>();
        fmod = GameObject.FindObjectOfType<FMODController>();
        level = GameObject.FindObjectOfType<LevelManager>();
        numStrikes = 0;
        currentScore = 0;
        foreach (GameObject strike in Strikes)
        {
            strike.SetActive(false);
        }

        if (text != null)
        {
            text.text = "High Score: " + PlayerPrefs.GetFloat("score");
        }
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    public float GetHighScore()
    {
        return currentScore;
    }

    public void IncrementScore(float value)
    {
        currentScore += value;
        text.text = "Score: " + currentScore;
        fmod.UpdateTransition(value);
    }

    public void AddStrike()
    {
        numStrikes += 1;
        if (numStrikes <= 5)
        {
            Strikes[numStrikes - 1].SetActive(true);
        }
        
        if (numStrikes >= 5)
        {
            level.SaveScore(currentScore);
            level.GameOver();
        }
    }

    public void SetFinalScore(float score)
    {
        finalScoreText.text = "Your Score: " + score;
    }
}