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
    private GameController gameController;

    private void UpdateStatText(Text label, Text value)
    {
        label.text += STATNAMES[currentStat] + "\n";
        value.text += statValues[currentStat] + "\n";
    }

    private void ShowNextStat()
    {
        if(currentStat > totalStats - 1)
        {
            scoreValue.text = gameController.getScore();
            currentStat = -1;
            return;
        }
        if(currentStat < totalStats)
        {
            UpdateStatText(StatsLabel, StatsValues);
        }

        currentStat++;
    }

    void Update()
    {
        delay += Time.deltaTime;
        if(delay > statsDelay && currentStat != -1)
        {
            ShowNextStat();
            delay = 0;
        }
    }

    public void clearText()
    {
        StatsLabel.text = "";
        StatsValues.text = "";
        scoreValue.text = "";
    }

    public override void Open()
    {
        clearText();
        base.Open();
        gameController = GameObject.FindObjectOfType<GameController>();
        statValues = new string[totalStats];
        statValues[0] = gameController.getTime();
        statValues[1] = gameController.getKills();
    }

    public override void Close()
    {
        base.Close();
        currentStat = 0;
    }

    public void OnMainMenu()
    {
        SceneManager.LoadSceneAsync(0);
    }
}
