using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StartWindow : GenericWindow {
   
    public void NewGame()
    {
        SceneManager.LoadSceneAsync(1);
    }

    public void OnMultiplayer()
    {
        OnNextWindow();
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void Options()
    {
        Debug.Log("Options Pressed");
    }
}
