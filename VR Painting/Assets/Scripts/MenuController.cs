using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class MenuController : MonoBehaviour
{
    [SerializeField]
    private IntSO selectedLevel;
    public void PlayButton()
    {
        SceneManager.LoadSceneAsync(1);
    }
    
    public void SettingsButton()
    {
        // SceneManager.LoadSceneAsync(1);
    }

    public void LoadLevels(int difficulty)
    {
        print("Loading level " + difficulty);
        selectedLevel.Value = difficulty;
        SceneManager.LoadSceneAsync(2);
    }
}
