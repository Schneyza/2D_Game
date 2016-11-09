using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;

public class ScoreManager : MonoBehaviour
{
    public Text survivedText;
    public Text killsText;

    private float timeSurvived;
    private int kills;
    private float score;

    void OnEnable()
    {
        GameController.ResetCallback += ResetStats;
        GameController.GameEndCallback += CalculateScore;
        DestroyOnProjectile.EnemyKillCallback += IncrementKills;
    }

    // Update is called once per frame
    void Update()
    {
        {
            timeSurvived += Time.deltaTime;
            survivedText.text = "SURVIVED: " + formatTime(timeSurvived);
        }
    }

    string formatTime(float value)
    {
        TimeSpan t = TimeSpan.FromSeconds(value);

        return string.Format("{0:D2}:{1:D2}", t.Minutes, t.Seconds);
    }

    void ResetStats()
    {
        survivedText.gameObject.SetActive(true);
        killsText.gameObject.SetActive(true);
        timeSurvived = 0f;
        kills = 0;
        killsText.text = "KILLS: " + kills.ToString();
    }

    void CalculateScore()
    {
        survivedText.gameObject.SetActive(false);
        killsText.gameObject.SetActive(false);
        score = (timeSurvived * kills);
    }

    void IncrementKills()
    {
        kills++;
        killsText.text = "KILLS: " + kills.ToString();
    }

    void OnDisable()
    {
        GameController.ResetCallback -= ResetStats;
        GameController.GameEndCallback -= CalculateScore;
        DestroyOnProjectile.EnemyKillCallback -= IncrementKills;
    }

    //String Getters for private Properties
    public string getTime() { return formatTime(timeSurvived); }
    public string getKills() { return kills.ToString(); }
    public string getScore() { return ((int)score).ToString(); }
}
