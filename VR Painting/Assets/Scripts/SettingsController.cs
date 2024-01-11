using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SettingsController : MonoBehaviour
{
    [SerializeField] private SettingsSO settingsSO;
    [SerializeField] private GameObject MainMenu;
    [SerializeField] private GameObject SettingsMenu;
    private TextMeshProUGUI brushText;

    void Start()
    {
        Transform[] children = SettingsMenu.GetComponentsInChildren<Transform>();
        foreach (Transform child in children)
        {
            if (child.name == "BrushStatusText")
            {
                brushText = child.gameObject.GetComponent<TextMeshProUGUI>();
                break;
            }
        }
        settingsSO.UseBrush = false;
        brushText.text = "No";
    }

    public void SettingsButton(bool openSettings)
    {
        MainMenu.SetActive(!openSettings);
        SettingsMenu.SetActive(openSettings);
    }

    public void EnableBrush()
    {
        settingsSO.UseBrush = !settingsSO.UseBrush;
        brushText.text = settingsSO.UseBrush ? "Yes" : "No";
    }
}
