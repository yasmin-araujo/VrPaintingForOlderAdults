using System.Collections;
using System.Collections.Generic;
using Oculus.Interaction;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;


public class MenuController : MonoBehaviour
{
    [SerializeField] private IntSO selectedLevel;
    [SerializeField] private IntSO drawingIndex;
    [SerializeField] private GameObject levelMenu;
    [SerializeField] private GameObject drawingMenu;

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

    public void BackToLevelMenu()
    {
        levelMenu.SetActive(true);
        drawingMenu.SetActive(false);
    }

    public void LoadLevels(GameObject button)
    {
        // Avoids triggering the button when isOn is reset
        if (!button.GetComponent<ToggleDeselect>().isOn)
            return;
        button.GetComponent<ToggleDeselect>().isOn = false;
        int difficulty = button.GetComponent<LevelButtonController>().levelDifficulty;
        selectedLevel.Value = difficulty;
        drawingMenu.SetActive(true);
        levelMenu.SetActive(false);

        GetComponent<LevelListingController>().ListLevels(difficulty);
    }

    public void LoadDrawing(int index)
    {
        drawingIndex.Value = index;
        SceneManager.LoadSceneAsync(2);
    }
}
