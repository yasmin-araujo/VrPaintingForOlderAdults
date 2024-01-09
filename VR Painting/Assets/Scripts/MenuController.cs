using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class MenuController : MonoBehaviour
{
    [SerializeField]
    private IntSO selectedLevel;
    [SerializeField] private SettingsSO settingsSO;
    public void PlayButton(bool isDesktop)
    {
        SceneManager.LoadSceneAsync(!isDesktop ? "LevelMenu" : "LevelMenuOld");
    }
    
    public void SettingsButton()
    {
        settingsSO.useBrush = !settingsSO.useBrush;
        print(settingsSO.useBrush);
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
