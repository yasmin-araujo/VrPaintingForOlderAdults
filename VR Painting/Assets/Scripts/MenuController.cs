using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;


public class MenuController : MonoBehaviour
{
    [SerializeField]
    private IntSO selectedLevel;

    /* Main menu buttons */
    public void PlayButton()
    {
        SceneManager.LoadSceneAsync("LevelMenu");
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
