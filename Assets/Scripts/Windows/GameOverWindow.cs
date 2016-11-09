using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOverWindow : GenericWindow
{

    public Text StatsLabel;
    public Text StatsValues;
    public Text scoreValue;
    public float statsDelay = 1f;
    public int totalStats = 2;

    private int currentStat = 0;
    private float delay = 0;
    private string[] STATNAMES = { "Time Survived", "Enemies Killed" };
    private string[] statValues;
    private ScoreManager scoreManager;

    public override void Open()
    {
        clearText();
        base.Open();
        statValues = new string[totalStats];

        if ((scoreManager = GameObject.FindObjectOfType<ScoreManager>()) != null)
        {
            statValues[0] = scoreManager.getTime();
            statValues[1] = scoreManager.getKills();
        }
        else
        {
            for (int i = 0; i < statValues.Length; i++)
            {
                statValues[i] = "Not Available";
            }
        }
    }

    void Update()
    {
        delay += Time.deltaTime;
        if (delay > statsDelay && currentStat != -1)
        {
            ShowNextStat();
            delay = 0;
        }
    }

    public void OnMainMenu()
    {
        SceneManager.LoadSceneAsync(0);
    }

    private void UpdateStatText(Text label, Text value)
    {
        label.text += STATNAMES[currentStat] + "\n";
        value.text += statValues[currentStat] + "\n";
    }

    private void ShowNextStat()
    {
        //Display Final Score if all stats have been displayed
        if (currentStat > totalStats - 1)
        {
            if(scoreManager != null)
            {
                scoreValue.text = scoreManager.getScore();
            } else
            {
                scoreValue.text = "Not Available";
            }
            currentStat = -1;
            return;
        }

        //Display next stat if any are still available
        if (currentStat < totalStats)
        {
            UpdateStatText(StatsLabel, StatsValues);
        }

        currentStat++;
    }

    public void clearText()
    {
        StatsLabel.text = "";
        StatsValues.text = "";
        scoreValue.text = "";
    }

    public override void Close()
    {
        base.Close();
        currentStat = 0;
    }

}
