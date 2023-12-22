using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class MenuController : MonoBehaviour
{
    [SerializeField]
    private IntSO selectedLevel;
    public void PlayButton(bool isDesktop)
    {
        SceneManager.LoadSceneAsync(!isDesktop ? "LevelMenu" : "LevelMenuOld");
    }
    
    public void SettingsButton()
    {
        // SceneManager.LoadSceneAsync(1);
    }

    public void BackToMainMenuButton()
    {
        SceneManager.LoadSceneAsync(0);
    }

    public void LoadLevels(int difficulty)
    {
        print("Loading level " + difficulty);
        selectedLevel.Value = difficulty;
        SceneManager.LoadSceneAsync(2);
    }
}
