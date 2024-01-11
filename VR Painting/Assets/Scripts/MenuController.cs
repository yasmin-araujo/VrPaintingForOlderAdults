using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;


public class MenuController : MonoBehaviour
{
    [SerializeField]
    private IntSO selectedLevel;
    [SerializeField] private SettingsSO settingsSO;
    [SerializeField] private GameObject MainMenu;
    [SerializeField] private GameObject SettingsMenu;

    /* Main menu buttons */
    public void PlayButton()
    {
        SceneManager.LoadSceneAsync("LevelMenu");
    }

    public void SettingsButton(bool openSettings)
    {
        MainMenu.SetActive(!openSettings);
        SettingsMenu.SetActive(openSettings);
    }

    public void EnableBrush(GameObject text)
    {
        settingsSO.useBrush = !settingsSO.useBrush;
        text.GetComponent<TextMeshPro>().text = settingsSO.useBrush ? "Deactivate" : "Activate";
    }

    /* Level menu buttons */
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
